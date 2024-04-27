namespace EventHorizon.Game.Editor.Client.Shared.Properties;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventHorizon.Game.Editor.Client.Localization;
using EventHorizon.Game.Editor.Client.Localization.Api;
using EventHorizon.Game.Editor.Client.Zone.Api;
using EventHorizon.Game.Editor.Properties.Api;
using EventHorizon.Game.Editor.Zone.Editor.Services.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

public class PropertiesDisplayModel : ComponentBase
{
    [CascadingParameter]
    public required ZoneState State { get; set; }

    [Parameter]
    [EditorRequired]
    public required IDictionary<string, object> Data { get; set; }

    [Parameter]
    [EditorRequired]
    public required PropertiesMetadata PropertiesMetadata { get; set; }

    [Parameter]
    public Dictionary<string, string> LabelMap { get; set; } = [];

    [Parameter]
    public Dictionary<string, int> SortMap { get; set; } = [];

    [Parameter]
    public EventCallback<PropertiesDisplayChangedArgs> OnChanged { get; set; }

    [Parameter]
    public EventCallback<string> OnRemove { get; set; }

    [Inject]
    public required ILogger<PropertiesDisplayModel> Logger { get; set; }

    [Inject]
    public required Localizer<SharedResource> Localizer { get; set; }

    protected IDictionary<string, PropertyDisplayType> DisplayProperties { get; private set; } =
        new Dictionary<string, PropertyDisplayType>();

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        SetupProperties();
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        try
        {
            SetupProperties();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Failed to setup properties for display.");
        }
    }

    protected async Task HandlePropertyChanged(PropertyChangedArgs args)
    {
        args.NullCheck();
        Data[args.PropertyName] = args.Property;
        await OnChanged.InvokeAsync(new PropertiesDisplayChangedArgs(args.PropertyName, Data));
    }

    private void SetupProperties()
    {
        DisplayProperties.Clear();
        var data = Data.Where(prop =>
                !prop.Key.StartsWith(
                    ZoneEditorMetadata.EDITOR_METADATA_PREFIX,
                    System.StringComparison.InvariantCulture
                )
            )
            .OrderBy(a => a.Key);
        foreach (var prop in data)
        {
            var type = PropertiesMetadata.GetPropertyType(prop.Key, prop.Value);

            DisplayProperties.Add(
                prop.Key,
                new PropertyDisplayType
                {
                    Label = GetLabel(prop.Key),
                    Order = GetOrder(prop.Key),
                    Name = prop.Key,
                    Type = type,
                    Value = prop.Value
                }
            );
        }
    }

    public string GetLabel(string key)
    {
        return LabelMap.TryGetValue(key, out var value) ? value : string.Empty;
    }

    public int GetOrder(string key)
    {
        return SortMap.TryGetValue(key, out var value) ? value : 0;
    }
}
