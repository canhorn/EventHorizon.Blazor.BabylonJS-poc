namespace EventHorizon.Game.Editor.Client.Shared.Properties
{
    using System.Reflection.Metadata;
    using System.Threading.Tasks;

    using EventHorizon.Game.Editor.Client.Shared.Components;

    using Microsoft.AspNetCore.Components;

    public abstract class PropertyControlBase
        : EditorComponentBase
    {
        [Parameter]
        public string Label { get; set; } = string.Empty;
        [Parameter]
        public string PropertyName { get; set; } = string.Empty;
        [Parameter]
        public object Property { get; set; } = null!;
        [Parameter]
        public EventCallback<PropertyChangedArgs> OnChange { get; set; }
        [Parameter]
        public EventCallback<string> OnRemove { get; set; }

        public string LabelText => string.IsNullOrWhiteSpace(Label) ? PropertyName : Label;
        public bool ShowRemove => OnRemove.HasDelegate;

        protected async Task HandleChange(
            ChangeEventArgs args
        )
        {
            args.NullCheck();
            args.Value.NullCheck();
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
