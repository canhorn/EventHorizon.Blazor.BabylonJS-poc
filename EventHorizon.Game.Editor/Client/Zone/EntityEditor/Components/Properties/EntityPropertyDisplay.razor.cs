namespace EventHorizon.Game.Editor.Client.Zone.EntityEditor.Components.Properties
{
    using EventHorizon.Game.Editor.Client.Localization;
    using EventHorizon.Game.Editor.Client.Localization.Api;
    using EventHorizon.Game.Editor.Client.Zone.Api;
    using EventHorizon.Game.Editor.Zone.Editor.Services.Model;
    using Microsoft.AspNetCore.Components;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class EntityPropertyDisplayModel : ComponentBase
    {
        [CascadingParameter]
        public ZoneState State { get; set; } = null!;

        [Parameter]
        public IDictionary<string, object> Data { get; set; }
        [Parameter]
        public EventCallback<IDictionary<string, object>> OnChanged { get; set; }
        [Parameter]
        public EventCallback<string> OnRemove { get; set; }

        [Inject]
        public Localizer<SharedResource> Localizer { get; set; } = null!;

        protected IDictionary<string, EntityPropertyDisplayType> DisplayProperties { get; private set; } = new Dictionary<string, EntityPropertyDisplayType>();

        protected override Task OnInitializedAsync()
        {
            SetupProperties();

            return base.OnInitializedAsync();
        }

        protected override void OnParametersSet()
        {
            SetupProperties();
            base.OnParametersSet();
        }

        protected async Task HandlePropertyChanged(
            PropertyChangedArgs args
        )
        {
            args.NullCheck();
            Data[args.PropertyName] = args.Property;
            await OnChanged.InvokeAsync(
                Data
            );
        }

        protected async Task HandleRemoveProperty(
            string propertyName
        )
        {
            await OnRemove.InvokeAsync(
                propertyName
            );
        }

        private void SetupProperties()
        {
            DisplayProperties.Clear();
            var data = Data.Where(
                prop => !prop.Key.StartsWith(
                    ZoneEditorMetadata.EDITOR_METADATA_PREFIX,
                    System.StringComparison.InvariantCulture
                )
            ).OrderBy(a => a.Key);
            foreach (var prop in data)
            {
                var type = State.EditorState.Metadata.GetPropertyType(
                    prop.Key
                );
                if (type == ZoneEditorPropertyType.PropertyString
                    && State.EditorState.Metadata.IsComplexPropertyType(
                        prop.Value
                    )
                )
                {
                    type = ZoneEditorPropertyType.PropertyComplex;
                }

                DisplayProperties.Add(
                    prop.Key,
                    new EntityPropertyDisplayType
                    {
                        Name = prop.Key,
                        Type = type,
                        Value = prop.Value
                    }
                );
            }
        }
    }

    public class EntityPropertyDisplayType
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public object Value { get; set; }
    }

    public class PropertyChangedArgs
    {
        public string PropertyName { get; set; }
        public object Property { get; set; }
    }
}
