namespace EventHorizon.Game.Editor.Client.Shared.Properties;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Input.Model;
using EventHorizon.Game.Client.Systems.Player.Modules.Camera.Api;
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

    protected Dictionary<string, ControlKeyInput> ControlInputConfig { get; private set; } = [];

    public string NewKeyInput { get; set; } = string.Empty;
    protected List<StandardSelectOption> InputActionTypeOptions { get; private set; } = [];
    protected List<StandardSelectOption> CameraTypeOptions { get; private set; } = [];

    protected List<StandardSelectOption> DirectionOptions { get; private set; } = [];

    protected override void OnInitialized()
    {
        base.OnInitialized();
        CameraTypeOptions =
        [
            new StandardSelectOption
            {
                Text = Localizer["Select Camera..."],
                Value = string.Empty,
            },
            new StandardSelectOption
            {
                Text = Localizer["Player Universal Camera"],
                Value = SystemCameraTypes.PLAYER_UNIVERSAL_CAMERA_NAME,
            },
            new StandardSelectOption
            {
                Text = Localizer["Player Follow Camera"],
                Value = SystemCameraTypes.PLAYER_FOLLOW_CAMERA_NAME,
            },
        ];
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
        DirectionOptions =
        [
            new StandardSelectOption
            {
                Text = Localizer["Stationary"],
                Value = MoveDirection.Stationary.ToString(),
            },
            new StandardSelectOption
            {
                Text = Localizer["Forward"],
                Value = MoveDirection.Forward.ToString(),
            },
            new StandardSelectOption
            {
                Text = Localizer["Backwards"],
                Value = MoveDirection.Backwards.ToString(),
            },
            new StandardSelectOption
            {
                Text = Localizer["Left"],
                Value = MoveDirection.Left.ToString(),
            },
            new StandardSelectOption
            {
                Text = Localizer["Right"],
                Value = MoveDirection.Right.ToString(),
            },
        ];

        ControlInputConfig =
            JsonSerializer.Deserialize<Dictionary<string, ControlKeyInput>>(
                Property?.ToString() ?? "{}",
                JsonExtensions.DEFAULT_OPTIONS
            ) ?? [];

        foreach (var (key, keyInput) in ControlInputConfig)
        {
            keyInput.TypeOption =
                InputActionTypeOptions.FirstOrDefault(a => a.Value == keyInput.Type) ?? new();
            keyInput.CameraOption =
                CameraTypeOptions.FirstOrDefault(a => a.Value == keyInput.Camera)
                ?? CameraTypeOptions.First();
            keyInput.PressedOption =
                DirectionOptions.FirstOrDefault(a => a.Value == keyInput.Pressed?.ToString())
                ?? DirectionOptions.First();
            keyInput.ReleasedOption =
                DirectionOptions.FirstOrDefault(a => a.Value == keyInput.Released?.ToString())
                ?? DirectionOptions.First();
        }
    }

    private async Task TriggerChange()
    {
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

    protected string Message { get; set; } = string.Empty;

    protected async Task HandleAddNewInput()
    {
        Message = string.Empty;
        if (string.IsNullOrEmpty(NewKeyInput))
        {
            Message = Localizer["Key Input is required."];
            return;
        }
        else if (ControlInputConfig.ContainsKey(NewKeyInput))
        {
            Message = Localizer["Key Input already exists."];
            return;
        }

        ControlInputConfig[NewKeyInput] = new ControlKeyInput
        {
            Key = NewKeyInput,
            Type = KeyInputType.PlayerMove,
            TypeOption = InputActionTypeOptions.First(a => a.Value == KeyInputType.PlayerMove),
            Pressed = MoveDirection.Stationary,
            PressedOption = DirectionOptions.First(a =>
                a.Value == MoveDirection.Stationary.ToString()
            ),
            Released = MoveDirection.Stationary,
            ReleasedOption = DirectionOptions.First(a =>
                a.Value == MoveDirection.Stationary.ToString()
            ),
        };
        NewKeyInput = string.Empty;
        Message = Localizer["Key Input added."];

        await TriggerChange();
    }

    protected async Task HandleKeyInputTypeChanged(
        ControlKeyInput keyInput,
        StandardSelectOption option
    )
    {
        if (keyInput.Type == option.Value)
        {
            return;
        }

        keyInput.Type = option.Value;

        if (option.Value == KeyInputType.PlayerMove)
        {
            keyInput.Pressed = null;
            keyInput.Released = null;
        }
        else if (option.Value == KeyInputType.SetActiveCamera)
        {
            keyInput.Camera = string.Empty;
            keyInput.CameraOption = CameraTypeOptions.First();
        }

        keyInput.TypeOption = option;

        await TriggerChange();
    }

    protected async Task HandleRemoveKeyInput(string key)
    {
        ControlInputConfig.Remove(key);

        await TriggerChange();
    }

    #region PlayerMove
    public async Task HandlePlayerMovePressedChanged(
        ControlKeyInput keyInput,
        StandardSelectOption option
    )
    {
        if (option == null)
        {
            return;
        }

        var pressedOptionValue = Enum.TryParse(option.Value, out MoveDirection pressed)
            ? pressed
            : MoveDirection.Stationary;
        if (keyInput.Pressed == pressedOptionValue)
        {
            return;
        }

        keyInput.PressedOption = option;
        keyInput.Pressed = pressedOptionValue;

        await TriggerChange();
    }

    public async Task HandlePlayerMoveReleasedChanged(
        ControlKeyInput keyInput,
        StandardSelectOption option
    )
    {
        if (option == null)
        {
            return;
        }

        var pressedOptionValue = Enum.TryParse(option.Value, out MoveDirection pressed)
            ? pressed
            : MoveDirection.Stationary;
        if (keyInput.Released == pressedOptionValue)
        {
            return;
        }

        keyInput.ReleasedOption = option;
        keyInput.Released = pressedOptionValue;

        await TriggerChange();
    }
    #endregion

    #region Camera
    public async Task HandleCameraTypeChanged(ControlKeyInput keyInput, StandardSelectOption option)
    {
        if (keyInput.Camera == option.Value)
        {
            return;
        }

        keyInput.CameraOption = option;
        keyInput.Camera = option.Value;

        await TriggerChange();
    }
    #endregion

    public class ControlKeyInput : KeyInputBase
    {
        public string Key { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public StandardSelectOption TypeOption { get; set; } = new();
        public StandardSelectOption CameraOption { get; set; } = new();

        #region  PlayerMove Type
        public MoveDirection? Pressed { get; set; }
        public StandardSelectOption PressedOption { get; set; } = new();
        public MoveDirection? Released { get; set; }
        public StandardSelectOption ReleasedOption { get; set; } = new();
        #endregion

        #region SetActiveCamera Type
        public string? Camera { get; set; }
        #endregion
    }
}
