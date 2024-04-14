namespace EventHorizon.Game.Editor.Client.Shared.Components.Select;

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

public class StandardSelectModel : ComponentBase
{
    private StandardSelectOption _value = new();

    [Parameter]
    public IList<StandardSelectOption> Options { get; set; } = null!;

    [Parameter]
    public string DefaultValue { get; set; } = string.Empty;

    [Parameter]
    public string DefaultText { get; set; } = string.Empty;

    [Parameter]
    public EventCallback<StandardSelectOption> ValueChanged { get; set; }

    [Parameter]
    public bool Disabled { get; set; }

    [Parameter]
    public required StandardSelectOption Value { get; set; }

    protected string SelectedValue
    {
        get => Value?.Value ?? DefaultValue;
        set
        {
            var newValue = value ?? DefaultValue;
            _value =
                Options.FirstOrDefault(option => option.Value == newValue)
                ?? new StandardSelectOption();
        }
    }
    protected string SelectedText => Value?.Text ?? DefaultText;
    protected bool IsDisabled => Options.Count == 0 || Disabled;

    protected override void OnInitialized()
    {
        Options ??= [];
    }

    protected override void OnParametersSet()
    {
        _value = Value;
        base.OnParametersSet();
    }

    protected Task HandleSelectChanged(ChangeEventArgs changeEventArgs)
    {
        changeEventArgs.NullCheck(nameof(changeEventArgs));

        var newValue = changeEventArgs.Value?.ToString() ?? DefaultValue;
        return ValueChanged.InvokeAsync(
            Options.FirstOrDefault(option => option.Value == newValue)
        );
    }

    protected Task HandleSelectedOptionChanged(StandardSelectOption option)
    {
        var newValue = option.Value?.ToString() ?? DefaultValue;
        return ValueChanged.InvokeAsync(
            Options.FirstOrDefault(option => option.Value == newValue)
        );
    }
}

public record StandardSelectOption
{
    public string Value { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public bool Disabled { get; set; }
    public bool Hidden { get; set; }
}
