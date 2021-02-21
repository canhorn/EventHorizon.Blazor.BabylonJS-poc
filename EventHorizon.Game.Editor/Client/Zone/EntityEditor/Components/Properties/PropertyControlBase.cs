namespace EventHorizon.Game.Editor.Client.Zone.EntityEditor.Components.Properties
{
    using EventHorizon.Game.Editor.Client.Localization;
    using EventHorizon.Game.Editor.Client.Localization.Api;
    using Microsoft.AspNetCore.Components;
    using System.Threading.Tasks;

    public abstract class PropertyControlBase 
        : ComponentBase
    {
        [Parameter]
        public string PropertyName { get; set; }
        [Parameter]
        public object Property { get; set; }
        [Parameter]
        public EventCallback<PropertyChangedArgs> OnChange { get; set; }
        [Parameter]
        public EventCallback<string> OnRemove { get; set; }

        [Inject]
        public Localizer<SharedResource> Localizer { get; set; }


        public bool ShowRemove => OnRemove.HasDelegate;

        protected async Task HandleChange(
            ChangeEventArgs args
        )
        {
            args.NullCheck();
            await OnChange.InvokeAsync(
                new PropertyChangedArgs
                {
                    PropertyName = PropertyName,
                    Property = Parse(
                        args.Value
                    )
                }
            );
        }

        protected async Task HandleRemove()
        {
            await OnRemove.InvokeAsync(
                PropertyName
            );
        }

        protected abstract object Parse(
            object value
        );
    }
}
