namespace EventHorizon.Game.Editor.Client.Shared.Components.Select
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;

    public class StandardSelectModel
        : ComponentBase
    {
        private StandardSelectOption _value;

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
        public StandardSelectOption Value
        {
            get => _value;
            set
            {
                var newValue = value?.Value?.ToString(
                    CultureInfo.InvariantCulture
                ) ?? DefaultValue;
                _value = Options.FirstOrDefault(
                    option => option.Value == newValue
                );
            }
        }

        protected string SelectedValue
        {
            get => Value?.Value ?? DefaultValue;
            set
            {
                var newValue = value ?? DefaultValue;
                _value = Options.FirstOrDefault(
                    option => option.Value == newValue
                ); ;
            }
        }
        protected string SelectedText => Value?.Text ?? DefaultText;
        protected bool IsDisabled => Options.Count == 0 || Disabled;

        protected override void OnInitialized()
        {
            if (Options == null)
            {
                Options = new List<StandardSelectOption>();
            }
        }

        protected Task HandleSelectChanged(
            ChangeEventArgs changeEventArgs
        )
        {
            changeEventArgs.NullCheck(nameof(changeEventArgs));

            var newValue = changeEventArgs.Value?.ToString() ?? DefaultValue;
            return ValueChanged.InvokeAsync(
                Options.FirstOrDefault(
                    option => option.Value == newValue
                )
            );
        }

    }

    public class StandardSelectOption
    {
        public string Value { get; set; }
        public string Text { get; set; }
        public bool Disabled { get; set; }
        public bool Hidden { get; set; }
    }
}
