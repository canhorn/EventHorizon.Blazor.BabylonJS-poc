namespace EventHorizon.Game.Editor.Client.Shared.Properties;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EventHorizon.Game.Editor.Client.Localization;
using EventHorizon.Game.Editor.Client.Localization.Api;
using EventHorizon.Game.Editor.Client.Zone.Api;
using EventHorizon.Game.Editor.Properties.Api;
using EventHorizon.Game.Editor.Zone.Editor.Services.Model;

using Microsoft.AspNetCore.Components;

public class PropertiesDisplayModel : ComponentBase
{
    [CascadingParameter]
    public ZoneState State { get; set; } = null!;

    [Parameter]
    public IDictionary<string, string> LabelMap { get; set; } =
        new Dictionary<string, string>();

    [Parameter]
    public IDictionary<string, object> Data { get; set; } = null!;

    [Parameter]
    public PropertiesMetadata PropertiesMetadata { get; set; } = null!;

    [Parameter]
    public EventCallback<IDictionary<string, object>> OnChanged { get; set; }

    [Parameter]
    public EventCallback<string> OnRemove { get; set; }

    [Inject]
    public Localizer<SharedResource> Localizer { get; set; } = null!;

    protected IDictionary<string, PropertyDisplayType> DisplayProperties
    {
        get;
        private set;
    } = new Dictionary<string, PropertyDisplayType>();

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        SetupProperties();
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        SetupProperties();
    }

    protected async Task HandlePropertyChanged(PropertyChangedArgs args)
    {
        args.NullCheck();
        Data[args.PropertyName] = args.Property;
        await OnChanged.InvokeAsync(Data);
    }

    private void SetupProperties()
    {
        DisplayProperties.Clear();
        var data = Data.Where(
                prop =>
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
}
