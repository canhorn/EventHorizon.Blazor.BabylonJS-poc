namespace EventHorizon.Game.Editor.Client.DataStorage.Components.Modal;

using System;

using Microsoft.AspNetCore.Components;

public class DataValueModalSubmitBase : ComponentBase
{
    [Parameter]
    public bool IsNewValue { get; set; }

    [Parameter]
    public string DataName { get; set; } = string.Empty;

    [Parameter]
    public Func<string, bool> ContainsName { get; set; } = _ => false;

    [Parameter]
    public RenderFragment Override { get; set; } = null!;

    [Parameter]
    public RenderFragment Update { get; set; } = null!;

    [Parameter]
    public RenderFragment Clone { get; set; } = null!;

    [Parameter]
    public RenderFragment Create { get; set; } = null!;

    public RenderFragment? SectionFragment { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Setup();
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Setup();
    }

    private void Setup()
    {
        SectionFragment = Override;

        if (!IsNewValue && ContainsName(DataName))
        {
            SectionFragment = Update;
        }
        else if (!IsNewValue && !ContainsName(DataName))
        {
            SectionFragment = Clone;
        }
        else if (IsNewValue && !ContainsName(DataName))
        {
            SectionFragment = Create;
        }
    }
}
