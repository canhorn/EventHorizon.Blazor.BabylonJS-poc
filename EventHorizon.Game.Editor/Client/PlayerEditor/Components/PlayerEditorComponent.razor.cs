namespace EventHorizon.Game.Editor.Client.PlayerEditor.Components;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Systems.Combat.Model;
using EventHorizon.Game.Client.Systems.Entity.Properties.Model.Api;
using EventHorizon.Game.Client.Systems.Entity.Properties.Model.Model;
using EventHorizon.Game.Client.Systems.Entity.Properties.Move.Api;
using EventHorizon.Game.Client.Systems.Entity.Properties.Move.Model;
using EventHorizon.Game.Client.Systems.Player.Modules.SkillSelection.Api;
using EventHorizon.Game.Editor.Client.Localization;
using EventHorizon.Game.Editor.Client.Localization.Api;
using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Shared.Components.Containers;
using EventHorizon.Game.Editor.Client.Shared.Toast.Model;
using EventHorizon.Game.Editor.Client.Shared.Toast.Show;
using EventHorizon.Game.Editor.Client.Zone.Api;
using EventHorizon.Game.Editor.Client.Zone.Change;
using EventHorizon.Zone.Systems.Player.Model;
using EventHorizon.Zone.Systems.Player.Query;
using EventHorizon.Zone.Systems.Player.Save;
using Microsoft.AspNetCore.Components;

public class PlayerEditorComponentBase
    : ObservableComponentBase,
        ActiveZoneStateChangedEventObserver
{
    private static readonly List<PropertyTypeBuilder> DefaultDataTypes =
    [
        new(
            IMovementState.NAME,
            (Localizer<SharedResource> localizer) => localizer["Movement State"]
        ),
        new(ISkillState.NAME, (Localizer<SharedResource> localizer) => localizer["Skill State"]),
        new(IModelState.NAME, (Localizer<SharedResource> localizer) => localizer["Model State"]),
    ];

    [CascadingParameter]
    public required ZoneState ZoneState { get; set; }

    protected ComponentState State { get; set; } = ComponentState.Loading;
    protected string ErrorMessage { get; set; } = string.Empty;
    protected PlayerObjectEntityDataModel PlayerData { get; set; } = [];
    protected Dictionary<string, object> CustomizableProperties { get; set; } = [];

    protected List<PropertyType> AvailableDataTypes { get; private set; } = [];

    public record PropertyType(string PropertyName, string Label);

    public record PropertyTypeBuilder(
        string PropertyName,
        Func<Localizer<SharedResource>, string> GetLabel
    );

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        await LoadPlayerData();
        Setup();
    }

    public async Task Handle(ActiveZoneStateChangedEvent args)
    {
        await LoadPlayerData();
        Setup();
    }

    protected async Task HandlePropertyChange(PropertyChangeArgs args)
    {
        Console.WriteLine(args);
        CustomizableProperties[args.PropertyName] = args.Value;
        PlayerData[args.PropertyName] = args.Value;

        await SavePlayerData();
    }

    private async Task SavePlayerData()
    {
        var saveResult = await Sender.Send(new SaveZonePlayerDataCommand(PlayerData));
        if (!saveResult)
        {
            var errorMessage = Localizer[
                "Failed to save Player Data: {0}",
                saveResult.ErrorCode ?? "Unknown Error"
            ];
            await ShowMessage(
                Localizer["Failed to Save Player Data"],
                errorMessage,
                MessageLevel.Error
            );
            return;
        }
    }

    private void Setup(bool force = false)
    {
        if (!force && State != ComponentState.Content)
        {
            return;
        }

        AvailableDataTypes = DefaultDataTypes
            .Where(type => !PlayerData.ContainsKey(type.PropertyName))
            .Select(a => new PropertyType(a.PropertyName, a.GetLabel(Localizer)))
            .ToList();

        CustomizableProperties = DefaultDataTypes
            .Where(property => PlayerData.ContainsKey(property.PropertyName))
            .OrderBy(property => property.PropertyName)
            .ToDictionary(property => property.PropertyName, a => PlayerData[a.PropertyName]);
    }

    private async Task LoadPlayerData()
    {
        var playerDataResult = await Sender.Send(new QueryForZonePlayerData());
        if (!playerDataResult)
        {
            ErrorMessage = Localizer[
                "Failed to load Player Data: {0}",
                playerDataResult.ErrorCode ?? "Unknown Error"
            ];
            return;
        }
        PlayerData = playerDataResult.Result;
        State = ComponentState.Content;
    }

    protected void HandleDataTypeChanged(ChangeEventArgs args)
    {
        Console.WriteLine(args.Value);
        if (args.Value is not string propertyName)
        {
            return;
        }
        switch (propertyName)
        {
            case IMovementState.NAME:
                PlayerData[IMovementState.NAME] = new MovementStateModel();
                break;
            case ISkillState.NAME:
                PlayerData[ISkillState.NAME] = new SkillStateModel();
                break;
            case IModelState.NAME:
                PlayerData[IModelState.NAME] = new ModelStateModel();
                break;
        }

        Setup(force: true);
    }

    protected void HandleDeleteProperty(string propertyName)
    {
        Console.WriteLine(propertyName);
        PlayerData.Remove(propertyName);
        Setup(force: true);
    }
}

public record PropertyChangeArgs(string PropertyName, object Value);
