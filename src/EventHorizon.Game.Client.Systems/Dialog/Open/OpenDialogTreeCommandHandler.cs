namespace EventHorizon.Game.Client.Systems.Dialog.Open;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Core.I18n.Api;
using EventHorizon.Game.Client.Engine.Entity.Tag;
using EventHorizon.Game.Client.Engine.Entity.Tracking.Query;
using EventHorizon.Game.Client.Engine.Gui.Activate;
using EventHorizon.Game.Client.Engine.Gui.Api;
using EventHorizon.Game.Client.Engine.Gui.Create;
using EventHorizon.Game.Client.Engine.Gui.Dispose;
using EventHorizon.Game.Client.Engine.Gui.Generate;
using EventHorizon.Game.Client.Engine.Gui.Hide;
using EventHorizon.Game.Client.Engine.Gui.Model;
using EventHorizon.Game.Client.Engine.Gui.Show;
using EventHorizon.Game.Client.Engine.Gui.Update;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
using EventHorizon.Game.Client.Engine.Systems.Scripting.Run;
using EventHorizon.Game.Client.Systems.Dialog.Api;
using EventHorizon.Game.Client.Systems.Dialog.Query;
using EventHorizon.Game.Client.Systems.Player.Api;
using EventHorizon.Game.Client.Systems.Player.Query;

using MediatR;

using Microsoft.Extensions.Logging;

public class OpenDialogTreeCommandHandler
    : IRequestHandler<OpenDialogTreeCommand, StandardCommandResult>
{
    private readonly ILogger _logger;
    private readonly IMediator _mediator;
    private readonly ILocalizer _localizer;

    public OpenDialogTreeCommandHandler(
        ILogger<OpenDialogTreeCommandHandler> logger,
        IMediator mediator,
        ILocalizer localizer
    )
    {
        _logger = logger;
        _mediator = mediator;
        _localizer = localizer;
    }

    public async Task<StandardCommandResult> Handle(
        OpenDialogTreeCommand request,
        CancellationToken cancellationToken
    )
    {
        var guiId = "gui_dialog";
        var (success, dialogTreeOption, playerOption, npcOption) =
            await ValidateParameters(
                request.PlayerId,
                request.NpcId,
                request.DialogTreeId,
                cancellationToken
            );

        if (
            !success
            || !dialogTreeOption.HasValue
            || !playerOption.HasValue
            || !npcOption.HasValue
        )
        {
            return new StandardCommandResult("failed_to_open_dialog_tree");
        }
        var dialogTree = dialogTreeOption.Value;
        var player = playerOption.Value;
        var npc = npcOption.Value;

        var templateData = new Dictionary<string, object>
        {
            { "player.name", player.Name },
            { "npc.name", npc.Name },
        };

        await _mediator.Send(
            new UpdateGuiControlCommand(
                guiId,
                new GuiControlDataModel
                {
                    ControlId = "gui_dialog-title_text",
                    Options = new GuiControlOptionsModel
                    {
                        {
                            "text",
                            _localizer.Template(
                                dialogTree.Root.TitleKey,
                                templateData.Merge(dialogTree.Root.TitleData)
                            )
                        }
                    }
                }
            ),
            cancellationToken
        );
        await _mediator.Send(
            new UpdateGuiControlCommand(
                guiId,
                new GuiControlDataModel
                {
                    ControlId = "gui_editor-body_text",
                    Options = new GuiControlOptionsModel
                    {
                        {
                            "text",
                            _localizer.Template(
                                dialogTree.Root.TextKey,
                                templateData.Merge(dialogTree.Root.TextData)
                            )
                        }
                    }
                }
            ),
            cancellationToken
        );

        await setupDialogActionArea(
            guiId,
            dialogTree,
            player.EntityId,
            npc.EntityId,
            templateData,
            cancellationToken
        );

        await _mediator.Send(new ShowGuiCommand(guiId), cancellationToken);

        return new StandardCommandResult();
    }

    private async Task setupDialogActionArea(
        string guiId,
        DialogTree dialogTree,
        long playerId,
        long npcId,
        IDictionary<string, object> templateData,
        CancellationToken cancellationToken
    )
    {
        var actionButtonGuiId = "gui_editor-action_button_stack";
        var generatedGuiId = await _mediator.Send(
            new GetGeneratedGuiControlId(guiId, actionButtonGuiId),
            cancellationToken
        );
        await _mediator.Send(
            new DisposeOfGuiControlChildrenCommand(generatedGuiId),
            cancellationToken
        );

        var index = 0;
        foreach (var action in dialogTree.Root.Actions)
        {
            var newActionGuiId = $"gui_dialog-action_button-{index}";

            Func<Task> ActionClickedHandler = async () =>
            {
                switch (action.ActionType)
                {
                    case "next":
                        if (!string.IsNullOrEmpty(action.NextNodeKey))
                        {
                            await _mediator.Send(
                                new OpenDialogTreeCommand(
                                    action.NextNodeKey,
                                    playerId,
                                    npcId
                                )
                            );
                        }
                        break;
                    case "script":
                        if (action.Script.IsNotNull())
                        {
                            await _mediator.Send(
                                new RunClientScriptCommand(
                                    action.Script.Id,
                                    action.Script.Id,
                                    action.Script.Data.Merge(templateData)
                                )
                            );
                        }
                        break;
                    case "done":
                    default:
                        await _mediator.Send(new HideGuiCommand(guiId));
                        break;
                }
            };

            await _mediator.Send(
                new CreateGuiCommand(
                    newActionGuiId,
                    "gui_dialog_action_button",
                    parentControlId: generatedGuiId!,
                    controlDataList: new List<IGuiControlData>
                    {
                        new GuiControlDataModel
                        {
                            ControlId = "gui_editor-action_button",
                            Options = new GuiControlOptionsModel
                            {
                                {
                                    "textBlockOptions",
                                    new GuiControlOptionsModel
                                    {
                                        {
                                            "text",
                                            _localizer.Template(
                                                action.TextKey,
                                                action.TextData.Merge(
                                                    templateData
                                                )
                                            )
                                        }
                                    }
                                },
                                { "onClick", ActionClickedHandler }
                            }
                        }
                    }
                ),
                cancellationToken
            );

            await _mediator.Send(
                new ActivateGuiCommand(newActionGuiId),
                cancellationToken
            );

            index++;
        }
    }

    private async ValueTask<(
        bool success,
        Option<DialogTree> dialogTree,
        Option<IPlayerEntity> player,
        Option<IObjectEntity> npc
    )> ValidateParameters(
        long playerId,
        long npcId,
        string dialogTreeId,
        CancellationToken cancellationToken
    )
    {
        // Validate Player
        var playerResult = await _mediator.Send(
            new QueryForCurrentPlayer(),
            cancellationToken
        );
        if (!playerResult.Success || playerResult.Result.EntityId != playerId)
        {
            _logger.LogWarning(
                "Failed to Validate Current Player: {ErrorCode}",
                playerResult.ErrorCode
            );
            return (
                false,
                new Option<DialogTree>(null),
                new Option<IPlayerEntity>(null),
                new Option<IObjectEntity>(null)
            );
        }

        // Validate NPC
        var npcResult = await _mediator.Send(
            new QueryForEntity(TagBuilder.CreateEntityIdTag(npcId.ToString())),
            cancellationToken
        );
        if (
            !npcResult.Success
            || npcResult.Result.IsNull()
            || !npcResult.Result.Any()
        )
        {
            _logger.LogWarning(
                "Failed to Validate NPC: {NpcId} | {ErrorCode}",
                npcId,
                npcResult.ErrorCode
            );
            return (
                false,
                new Option<DialogTree>(null),
                new Option<IPlayerEntity>(null),
                new Option<IObjectEntity>(null)
            );
        }
        var npc = npcResult.Result.First();

        // Query/Validate Dialog Tree
        var dialogTreeResult = await _mediator.Send(
            new QueryForDialogTree(dialogTreeId),
            cancellationToken
        );
        if (!dialogTreeResult.Success)
        {
            _logger.LogWarning(
                "Failed to Validate DialogTree: {DialogTreeId} | {ErrorCode}",
                dialogTreeId,
                dialogTreeResult.ErrorCode
            );
            return (
                false,
                new Option<DialogTree>(null),
                new Option<IPlayerEntity>(null),
                new Option<IObjectEntity>(null)
            );
        }

        return (
            true,
            dialogTreeResult.Result.ToOption(),
            playerResult.Result.ToOption(),
            npc.ToOption<IObjectEntity>()
        );
    }
}
