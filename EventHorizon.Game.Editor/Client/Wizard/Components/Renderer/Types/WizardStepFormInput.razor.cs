namespace EventHorizon.Game.Editor.Client.Wizard.Components.Renderer.Types
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Model;
    using EventHorizon.Game.Editor.Client.Localization;
    using EventHorizon.Game.Editor.Client.Localization.Api;
    using EventHorizon.Game.Editor.Client.Shared.Toast.Model;
    using EventHorizon.Game.Editor.Client.Shared.Toast.Show;
    using EventHorizon.Game.Editor.Client.Wizard.Api;
    using EventHorizon.Game.Editor.Properties.Api;
    using EventHorizon.Game.Editor.Properties.Model;
    using EventHorizon.Zone.Systems.Wizard.Model;
    using MediatR;
    using Microsoft.AspNetCore.Components;

    public class WizardStepFormInputModel
        : ComponentBase
    {
        [CascadingParameter]
        public WizardState State { get; set; } = null!;

        [Parameter]
        public WizardStep Step { get; set; } = null!;
        [Parameter]
        public WizardData Data { get; set; } = null!;

        [Inject]
        public IMediator Mediator { get; set; } = null!;
        [Inject]
        public Localizer<SharedResource> Localizer { get; set; } = null!;

        public Dictionary<string, string> PropertyLabelMap { get; private set; } = new();
        public Dictionary<string, object> PropertiesData { get; private set; } = new();
        public PropertiesMetadataModel PropertiesMetadata { get; private set; } = new();

        protected override void OnInitialized()
        {
            base.OnInitialized();

            var properties = Step.Details.Where(
                a => a.Key.StartsWith(
                    "property:"
                ) && a.Key.EndsWith(
                    ":label"
                ).IsNotTrue()
            );
            foreach (var property in properties)
            {
                var propertyKey = property.Key.Replace(
                    "property:",
                    string.Empty
                );
                PropertiesMetadata[propertyKey] = property.Value;
                PropertiesData[propertyKey] = ParseDataValue(
                    propertyKey,
                    property.Value
                );
                PropertyLabelMap[propertyKey] = GetLabel(
                    propertyKey
                );
                Data[property.Key] = property.Value;
            }
        }

        public string GetLabel(
            string key
        )
        {
            var label = Step.Details[$"property:{key}:label"];
            if (string.IsNullOrWhiteSpace(
                label
            ).IsNotTrue())
            {
                return Localizer[label];
            }

            return label;
        }

        public object ParseDataValue(
            string key,
            string keyType
        )
        {
            switch (keyType)
            {
                case PropertyType.Boolean:
                    return bool.TryParse(
                        Data[key], out var boolValue
                    ) ? boolValue : GetDefaultProperty(keyType);
                case PropertyType.Decimal:
                    return decimal.TryParse(
                        Data[key], out var decimalValue
                    ) ? decimalValue : GetDefaultProperty(keyType);
                case PropertyType.Long:
                    return long.TryParse(
                        Data[key], out var longValue
                    ) ? longValue : GetDefaultProperty(keyType);
                case PropertyType.String:
                    return Data[key];
                default:
                    Mediator.Send(
                        new ShowMessageEvent(
                            Localizer["Wizard Input Form Issue"],
                            Localizer[
                                "'{0}' is an invalid Property Type used for {1}.",
                                keyType,
                                key
                            ],
                            MessageLevel.Warning
                        )
                    );
                    return string.Empty;
            }
        }

        public static object GetDefaultProperty(
            string propertyType
        )
        {
            return propertyType switch
            {
                PropertyType.Boolean => false,
                PropertyType.Decimal => 0.0m,
                PropertyType.Long => 0,
                PropertyType.Vector3 => ServerVector3.Zero(),
                _ => string.Empty,
            };
        }

        public Task HandleDataChanged(
            IDictionary<string, object> data
        )
        {
            PropertiesData = new Dictionary<string, object>(
                data
            );

            foreach (var property in PropertiesData)
            {
                Data[property.Key] = property.Value?.ToString() ?? string.Empty;
            }

            return Task.CompletedTask;
        }

        public class PropertiesMetadataModel
            : Dictionary<string, string>,
            PropertiesMetadata
        {
            public string GetPropertyType(
                string key,
                object _
            )
            {
                return TryGetValue(
                    key,
                    out var value
                ) ? value : PropertyType.String;
            }

            public object GetDefaultValueForPropertyType(
                string propertyType
            )
            {
                return string.Empty;
            }
        }
    }
}
