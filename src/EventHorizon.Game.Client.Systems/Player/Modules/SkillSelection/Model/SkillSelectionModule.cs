namespace EventHorizon.Game.Client.Systems.Player.Modules.SkillSelection.Model
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.I18n.Api;
    using EventHorizon.Game.Client.Engine.Gui.Show;
    using EventHorizon.Game.Client.Engine.Scripting.Data;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Publish;
    using EventHorizon.Game.Client.Engine.Systems.Module.Model;
    using EventHorizon.Game.Client.Engine.Systems.Scripting.Run;
    using EventHorizon.Game.Client.Systems.ClientScripts.Set;
    using EventHorizon.Game.Client.Systems.EntityModule;
    using EventHorizon.Game.Client.Systems.EntityModule.Api;
    using EventHorizon.Game.Client.Systems.EntityModule.Create;
    using EventHorizon.Game.Client.Systems.Player.Api;
    using EventHorizon.Game.Client.Systems.Player.Modules.SkillSelection.Api;
    using EventHorizon.Game.Server.ServerModule.SystemLog.Message;
    using EventHorizon.Game.Server.SkillSelection.Model;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class StandardSkillSelectionModule
        : ModuleEntityBase,
        SkillSelectionModule,
        ClientScriptsAssemblySetEventObserver
    {
        private readonly ILogger _logger = GameServiceProvider.GetService<ILogger<StandardSkillSelectionModule>>();
        private readonly IMediator _mediator = GameServiceProvider.GetService<IMediator>();
        private readonly IPlayerEntity _entity;

        private IEntityModule? _entityModule;
        private ScriptData _scriptData = new ScriptData(new Dictionary<string, object>());

        public override int Priority => 0;

        public StandardSkillSelectionModule(
            IPlayerEntity entity
        )
        {
            _entity = entity;
            GamePlatfrom.RegisterObserver(this);
        }

        public override async Task Initialize()
        {
            var entityModuleScripts = new EntityModuleScriptsModel
            {
                Name = "Player_SkillSelection",
                InitializeScript = "SkillSelection_Initialize",
                DisposeScript = "SkillSelection_Dispose",
                UpdateScript = "SkillSelection_Update",
            };
            _scriptData = new ScriptData(
                new Dictionary<string, object>
                {
                    { "skillList", CreatePlayerSkillList() }
                }
            );
            var entityModuleResult = await _mediator.Send(
                new CreateEntityModuleCommand(
                    entityModuleScripts,
                    _scriptData
                )
            );
            _entityModule = entityModuleResult.Result;

            await _entityModule.Initialize();
            await _entityModule.PostInitialize();
        }

        public override async Task Dispose()
        {
            if (_entityModule.IsNotNull()
                && _entityModule.IsDisposable)
            {
                await _entityModule.Dispose();
            }

            GamePlatfrom.UnRegisterObserver(this);
        }

        public override Task Update()
        {
            if (_entityModule.IsNotNull()
                && _entityModule.IsUpdatable)
            {
                _entityModule.Update();
            }
            return Task.CompletedTask;
        }

        public async Task Handle(
            ClientScriptsAssemblySetEvent args
        )
        {
            await Dispose();
            await Initialize();
        }

        private IEnumerable<SkillDetails> CreatePlayerSkillList()
        {
            return new List<SkillDetails>
            {
                new SkillDetails
                {
                    SkillName = "Clear Selection",
                    OnClick = async () =>
                    {
                        // TODO: [Interaction System] - Allow for Selection using SelectionModule
                        _logger.LogDebug("RUN Clear Selection");
                        await _mediator.Send(
                            new RunClientScriptCommand(
                                "Skill_Player_ClearSelection",
                                "skill.client_selection",
                                new Dictionary<string, object>
                                {
                                    { "entity", _entity },
                                }
                            )
                        );
                    }
                },
                new SkillDetails
                {
                    SkillName = "Open Debugging",
                    OnClick = () =>
                    {
                        // TODO: [DEBUGGING] - RUN Open Debugging
                        _logger.LogDebug("TODO: RUN Open Debugging");
                        return Task.CompletedTask;
                    }
                },
                new SkillDetails
                {
                    SkillName = "Close Debugging",
                    OnClick = () =>
                    {
                        // TODO: [DEBUGGING] - RUN Close Debugging
                        _logger.LogDebug("RUN Close Debugging");
                        return Task.CompletedTask;
                    }
                },
                new SkillDetails
                {
                    SkillName = "Test Debugging Message",
                    OnClick = () =>
                    {
                        // TODO: [DEBUGGING] - RUN Test Debugging Message
                        _logger.LogDebug("RUN Test Debugging Message");
                        return Task.CompletedTask;
                    }
                },
                new SkillDetails
                {
                    SkillName = "Open Dialog",
                    OnClick = async () =>
                    {
                        _logger.LogDebug("RUN Open Dialog");
                        await _mediator.Send(
                            new ShowGuiCommand(
                                "gui_dialog"
                            )
                        );
                    }
                },
                new SkillDetails
                {
                    SkillName = "Show System Log",
                    OnClick = async () =>
                    {
                        _logger.LogDebug("RUN Show System Log");
                        await _mediator.Send(
                            new RunClientScriptCommand(
                                "Log_ShowSystemLog",
                                "skill.show_system_log",
                                new Dictionary<string, object>
                                {
                                    { "entity", _entity },
                                }
                            )
                        );
                    }
                },
                new SkillDetails
                {
                    SkillName = "Hide System Log",
                    OnClick = async () =>
                    {
                        _logger.LogDebug("RUN Hide System Log");
                        await _mediator.Send(
                            new RunClientScriptCommand(
                                "Log_HideSystemLog",
                                "skill.hide_system_log",
                                new Dictionary<string, object>
                                {
                                    { "entity", _entity },
                                }
                            )
                        );
                    }
                },
                new SkillDetails
                {
                    SkillName = "Test System Message",
                    OnClick = async () =>
                    {
                        _logger.LogDebug("RUN Test System Message");
                        await _mediator.Publish(
                            new ClientActionMessageFromSystemEvent(
                                new ClientActionDataResolver(
                                    new Dictionary<string, object>
                                    {
                                        { "message", "Hello from Skill Selection" }
                                    }
                                )
                            )
                        );
                    }
                },
                new SkillDetails
                {
                    SkillName = "Fire Ball",
                    // KeyboardShortcut = "k",
                    OnClick = async () =>
                    {
                        _logger.LogDebug("RUN Fire Ball");
                        await _mediator.Send(
                            new RunClientScriptCommand(
                                "Skill_Player_FireBall",
                                "skill.fireball",
                                new Dictionary<string, object>
                                {
                                    { "entity", _entity },
                                }
                            )
                        );
                    }
                },
                new SkillDetails
                {
                    SkillName = "Capture Target",
                    // KeyboardShortcut = "c",
                    OnClick = async () =>
                    {
                        _logger.LogDebug("RUN Capture Target");
                        await _mediator.Send(
                            new RunClientScriptCommand(
                                "Skill_Player_CaptureTarget",
                                "skill.capture_target",
                                new Dictionary<string, object>
                                {
                                    { "entity", _entity },
                                }
                            )
                        );
                    }
                },
                new SkillDetails
                {
                    SkillName = "Companion Fire Ball",
                    // KeyboardShortcut = "l",
                    OnClick = async () =>
                    {
                        _logger.LogDebug("RUN Companion Fire Ball");
                        await _mediator.Send(
                            new RunClientScriptCommand(
                                "Skill_Runners_RunSelectedCompanionTargetedSkill",
                                "skill.companion_targeted_skill",
                                new Dictionary<string, object>
                                {
                                    { "entity", _entity },
                                    { "skillId", "Skills_FireBall.json" },
                                    { "noSelectionsMessage", GameServiceProvider.GetService<ILocalizer>()["noSelectionsMessage"] },
                                }
                            )
                        );
                    }
                },
            };
        }
    }
}
