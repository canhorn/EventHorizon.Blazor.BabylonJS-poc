namespace EventHorizon.Game.Editor.Client.Shared.Properties;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Input.Model;
using EventHorizon.Game.Editor.Client.Shared.Components.Select;
using EventHorizon.Game.Editor.Client.Zone.Api;
using EventHorizon.Game.Editor.Zone.Services.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

public class InputKeyMapControlModel : PropertyControlBase
{
    [CascadingParameter]
    public ZoneState ZoneState { get; set; } = null!;

    protected ZoneInfo ZoneInfo => ZoneState.ZoneInfo;

    protected string AssetServerFullName = string.Empty;

    protected StandardSelectOption? SelectedAssetOption { get; private set; }
    protected List<StandardSelectOption> AssetOptions { get; private set; } = [];

    protected EditContext? KeyInputContext { get; private set; }
    protected Dictionary<string, ControlKeyInput> ControlInputConfig { get; private set; } = [];

    public string NewKeyInput { get; set; } = string.Empty;
    protected List<StandardSelectOption> InputActionTypeOptions { get; private set; } = [];

    protected override void OnInitialized()
    {
        base.OnInitialized();
        InputActionTypeOptions =
        [
            new StandardSelectOption
            {
                Text = Localizer["Player Move"],
                Value = KeyInputType.PlayerMove,
            },
            new StandardSelectOption
            {
                Text = Localizer["Set Active Camera"],
                Value = KeyInputType.SetActiveCamera,
            },
            new StandardSelectOption
            {
                Text = Localizer["Run Interaction"],
                Value = KeyInputType.RunInteraction,
            },
        ];

        ControlInputConfig =
            JsonSerializer.Deserialize<Dictionary<string, ControlKeyInput>>(
                Property?.ToString() ?? "{}",
                JsonExtensions.DEFAULT_OPTIONS
            ) ?? [];

        foreach (var (key, keyInput) in ControlInputConfig)
        {
            keyInput.OptionType =
                InputActionTypeOptions.FirstOrDefault(a => a.Value == keyInput.Type) ?? new();
        }

        KeyInputContext = new EditContext(ControlInputConfig);
        KeyInputContext.OnFieldChanged += HandleFieldChanged;
    }

    private void HandleFieldChanged(object? sender, FieldChangedEventArgs e)
    {
        OnChange.InvokeAsync(
            new PropertyChangedArgs
            {
                PropertyName = PropertyName,
                Property = JsonSerializer.Serialize(
                    ControlInputConfig,
                    JsonExtensions.DEFAULT_OPTIONS
                ),
            }
        );
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
    }

    protected override object Parse(object value)
    {
        ControlInputConfig =
            JsonSerializer.Deserialize<Dictionary<string, ControlKeyInput>>(
                value?.ToString() ?? "{}",
                JsonExtensions.DEFAULT_OPTIONS
            ) ?? [];
        return value?.ToString() ?? "{}";
    }

    protected string ErrorMessage { get; set; } = string.Empty;

    protected async Task HandleAddNewInput()
    {
        ErrorMessage = string.Empty;
        if (string.IsNullOrEmpty(NewKeyInput))
        {
            ErrorMessage = Localizer["Key Input is required."];
            return;
        }
        else if (ControlInputConfig.ContainsKey(NewKeyInput))
        {
            ErrorMessage = Localizer["Key Input already exists."];
            return;
        }

        ControlInputConfig[NewKeyInput] = new ControlKeyInput
        {
            Key = NewKeyInput,
            Type = KeyInputType.PlayerMove,
            OptionType = InputActionTypeOptions.First(a => a.Value == KeyInputType.PlayerMove),
        };
        NewKeyInput = string.Empty;

        await HandleChange(
            new ChangeEventArgs
            {
                Value = JsonSerializer.Serialize(
                    ControlInputConfig,
                    JsonExtensions.DEFAULT_OPTIONS
                ),
            }
        );
    }

    protected async Task HandleKeyInputTypeChanged(
        ControlKeyInput keyInput,
        StandardSelectOption option
    )
    {
        keyInput.Type = option.Value;

        if (option.Value == KeyInputType.PlayerMove)
        {
            keyInput.Pressed = null;
            keyInput.Released = null;
        }
        else if (option.Value == KeyInputType.SetActiveCamera)
        {
            keyInput.Camera = null;
        }

        keyInput.OptionType = option;

        await HandleChange(
            new ChangeEventArgs
            {
                Value = JsonSerializer.Serialize(
                    ControlInputConfig,
                    JsonExtensions.DEFAULT_OPTIONS
                ),
            }
        );
    }

    protected async Task HandleRemoveKeyInput(string key)
    {
        ControlInputConfig.Remove(key);

        await HandleChange(
            new ChangeEventArgs
            {
                Value = JsonSerializer.Serialize(
                    ControlInputConfig,
                    JsonExtensions.DEFAULT_OPTIONS
                ),
            }
        );
    }

    protected void HandleKeyInputSubmit() { }

    public class ControlKeyInput : KeyInputBase
    {
        public string Key { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public StandardSelectOption OptionType { get; set; } = new();

        #region  PlayerMove Type
        public MoveDirection? Pressed { get; set; }
        public MoveDirection? Released { get; set; }

        #endregion

        #region SetActiveCamera Type
        public string? Camera { get; set; }
        #endregion
    }
}
