namespace EventHorizon.Game.Editor.Client.Shared.Components.Dropdown;

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

public record DropdownOption(
    string Text,
    string Value,
    bool Selected = false,
    bool Disabled = false
);

public class DropdownBase<TOption> : ComponentBase
{
    [Parameter]
    public required string Label { get; set; }

    [Parameter]
    public required string DropdownHeader { get; set; }

    [Parameter]
    public required string DeleteOptionTitle { get; set; }

    [Parameter]
    public required Func<TOption, string> OptionText { get; set; }

    [Parameter]
    public required IEnumerable<TOption> Options { get; set; }

    [Parameter]
    public required IEnumerable<TOption> SelectedOptions { get; set; }

    [Parameter]
    public required EventCallback<IEnumerable<TOption>> SelectedOptionsChanged { get; set; }

    protected bool _showForceSet = false;
    protected string _componentId = Guid.NewGuid().ToString();

    protected void HandleDropdownShow()
    {
        _showForceSet = !_showForceSet;
    }

    protected void HandleMenuItemClicked(TOption option)
    {
        var selectedOptions = new List<TOption>(SelectedOptions);
        if (selectedOptions.Contains(option))
        {
            selectedOptions.Remove(option);
        }
        else
        {
            selectedOptions.Add(option);
        }
        SelectedOptionsChanged.InvokeAsync(selectedOptions);
        _showForceSet = true;
    }

    protected void HandleRemoveOption(TOption option)
    {
        var selectedOptions = new List<TOption>(SelectedOptions);
        selectedOptions.Remove(option);
        SelectedOptionsChanged.InvokeAsync(selectedOptions);
    }
}
