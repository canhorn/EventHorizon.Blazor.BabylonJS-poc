namespace EventHorizon.Game.Editor.Client.PlayerEditor.Components;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Systems.Combat.Model;
using EventHorizon.Game.Client.Systems.Entity.Properties.Model.Api;
using EventHorizon.Game.Client.Systems.Entity.Properties.Model.Model;
using EventHorizon.Game.Client.Systems.Entity.Properties.Move.Api;
using EventHorizon.Game.Client.Systems.Entity.Properties.Move.Model;
using EventHorizon.Game.Editor.Client.Localization;
using EventHorizon.Game.Editor.Client.Localization.Api;
using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Shared.Components.Containers;
using EventHorizon.Game.Editor.Client.Shared.Components.Select;
using EventHorizon.Game.Editor.Client.Shared.Toast.Model;
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
        // new("forceSet", (Localizer<SharedResource> localizer) => localizer["Force Set"]),
        // new("forceSet1", (Localizer<SharedResource> localizer) => localizer["Force Set 1"]),
        // new("forceSet2", (Localizer<SharedResource> localizer) => localizer["Force Set 2"]),
        // new("forceSet3", (Localizer<SharedResource> localizer) => localizer["Force Set 3"]),
        // new("forceSet4", (Localizer<SharedResource> localizer) => localizer["Force Set 4"]),
    ];

    [CascadingParameter]
    public required ZoneState ZoneState { get; set; }

    protected ComponentState State { get; set; } = ComponentState.Loading;
    protected string ErrorMessage { get; set; } = string.Empty;
    protected bool PendingSave { get; set; }

    protected PlayerObjectEntityDataModel PlayerData { get; set; } = [];
    protected IEnumerable<PropertyType> PlayerDataForceSet { get; set; } = [];
    protected IEnumerable<PropertyType> SelectedForceSetStates { get; set; } = [];
    protected Dictionary<string, object> CustomizableProperties { get; set; } = [];

    protected List<PropertyType> AvailableDataTypes { get; private set; } = [];
    protected List<StandardSelectOption> AvailableDataOptions { get; private set; } = [];
    protected StandardSelectOption SelectedDataOption { get; private set; } = new();

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
        PendingSave = false;
        await LoadPlayerData();
        Setup();
    }

    protected void HandlePropertyChange(PropertyChangeArgs args)
    {
        PlayerData[args.PropertyName] = args.Value;
        CustomizableProperties[args.PropertyName] = args.Value;

        PendingSave = true;
    }

    protected async Task HandleSave()
    {
        var success = await SavePlayerData();
        if (success)
        {
            PendingSave = false;
            await LoadPlayerData();
            Setup();
        }
    }

    private async Task<bool> SavePlayerData()
    {
        var saveResult = await Sender.Send(new SaveZonePlayerDataCommand(PlayerData));
        if (saveResult)
        {
            return true;
        }

        var errorMessage = Localizer[
            "Failed to save Player Data: {0}",
            saveResult.ErrorCode ?? "Unknown Error"
        ];
        await ShowMessage(
            Localizer["Failed to Save Player Data"],
            errorMessage,
            MessageLevel.Error
        );
        return false;
    }

    protected void HandleDataOptionChanged(StandardSelectOption option)
    {
        var propertyName = option.Value;
        if (string.IsNullOrEmpty(propertyName))
        {
            Setup();
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

        PendingSave = true;
        Setup();
    }

    protected void HandleDeleteProperty(string propertyName)
    {
        PlayerData.Remove(propertyName);
        PendingSave = true;
        Setup();
    }

    protected void HandleSelectedForceSetStatesChanged(IEnumerable<PropertyType> options)
    {
        PlayerData["forceSet"] = options.Select(a => a.PropertyName).ToList();
        SelectedForceSetStates = options.OrderBy(a => a.Label);
        PendingSave = true;
        Setup();
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

    private void Setup()
    {
        AvailableDataTypes = DefaultDataTypes
            .Where(type => !PlayerData.ContainsKey(type.PropertyName))
            .Select(a => new PropertyType(a.PropertyName, a.GetLabel(Localizer)))
            .ToList();

        CustomizableProperties = DefaultDataTypes
            .Where(property => PlayerData.ContainsKey(property.PropertyName))
            .OrderBy(property => property.PropertyName)
            .ToDictionary(property => property.PropertyName, a => PlayerData[a.PropertyName]);

        AvailableDataOptions = AvailableDataTypes
            .Select(type => new StandardSelectOption
            {
                Value = type.PropertyName,
                Text = type.Label,
            })
            .OrderBy(option => option.Text)
            .InsertItem(
                0,
                new StandardSelectOption
                {
                    Value = string.Empty,
                    Text = Localizer["Add Player State..."],
                }
            )
            .ToList();
        SelectedDataOption = AvailableDataOptions.First();

        PlayerDataForceSet = DefaultDataTypes
            .Select(type => new PropertyType(type.PropertyName, type.GetLabel(Localizer)))
            .OrderBy(a => a.Label)
            .ToList();
        SelectedForceSetStates = PlayerData
            .Where(property => property.Key.StartsWith("forceSet"))
            .Select(property => property.Value.To<List<string>>() ?? [])
            .SelectMany(list => list)
            .Select(propertyName =>
                PlayerDataForceSet.FirstOrDefault(a => a.PropertyName == propertyName)
                ?? new PropertyType(propertyName, Localizer["Unknown Property({0})", propertyName])
            )
            .ToList();
    }
}

public record PropertyChangeArgs(string PropertyName, object Value);
