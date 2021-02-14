namespace EventHorizon.Game.Editor.Client.Shared.Blades
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Client.Authentication.Api;
    using EventHorizon.Game.Editor.Client.Authentication.Set;
    using EventHorizon.Game.Editor.Client.Localization;
    using EventHorizon.Game.Editor.Client.Localization.Api;
    using EventHorizon.Game.Editor.Client.Shared.ClickCapture;
    using EventHorizon.Game.Editor.Client.Shared.Components.Select;
    using MediatR;
    using Microsoft.AspNetCore.Components;

    public class BladeSelectionModel
        : ComponentBase
    {
        [CascadingParameter]
        public SessionValues SessionValues { get; set; } = null!;

        [Parameter]
        public string Id { get; set; } = string.Empty;

        [Inject]
        public IMediator Mediator { get; set; } = null!;
        [Inject]
        public Localizer<SharedResource> Localizer { get; set; } = null!;

        private readonly IDictionary<string, string> DEFAULT_BLADES = new Dictionary<string, string>
        {
            {
                "NAV_MENU",
                "Nav"
            },
            {
                "ENTITY_LIST",
                "Entity List"
            },
            {
                "EDITOR_FILE_EXPLORER",
                "File Explorer"
            },
            {
                "OBJECT_ENTITY_EDITOR",
                "Entity Editor"
            },
        };

        protected StandardSelectOption SelectedBladeOption { get; private set; }
        protected List<StandardSelectOption> BladeOptions { get; private set; } = new List<StandardSelectOption>();

        public string CurrentBlade { get; set; } = "NAV_MENU";
        public bool CollapseContent { get; private set; } = true;
        public string ContentCssClass => CollapseContent ? null : "--expanded";

        public bool IsSettingsOpen { get; private set; }

        protected override void OnInitialized()
        {
            // TODO: Pull this from a Command
            BladeOptions = DEFAULT_BLADES.Select(
                a => new StandardSelectOption
                {
                    Value = a.Key,
                    Text = Localizer[a.Value],
                }
            ).ToList();
            SetSelectedOption(
                CurrentBlade
            );

            Setup();
            base.OnInitialized();
        }

        protected override void OnParametersSet()
        {
            Setup();
            base.OnParametersSet();
        }

        protected string LocalizeBlade(string bladeKey)
        {
            return Localizer[
                DEFAULT_BLADES.FirstOrDefault(
                    a => a.Key == bladeKey
                ).Value ?? "Nav"
            ];
        }

        protected async Task HandleBladeValueChanged(
            StandardSelectOption option
        )
        {
            CurrentBlade = option.Value;
            SetSelectedOption(
                CurrentBlade
            );
            if (string.IsNullOrWhiteSpace(Id).IsNotTrue())
            {
                await Mediator.Send(
                    new SetSessionValueCommand(
                    $"currentBlade__{Id}",
                        CurrentBlade
                    )
                );
            }
        }

        protected void ToggleNavMenu()
        {
            CollapseContent = !CollapseContent;
        }

        protected void HandleOpenSettings()
        {
            IsSettingsOpen = true;
        }

        protected void HandleCloseSettings()
        {
            IsSettingsOpen = false;
        }

        private void Setup()
        {
            if (string.IsNullOrWhiteSpace(Id).IsNotTrue())
            {
                CurrentBlade = SessionValues.Get(
                    $"currentBlade__{Id}",
                    CurrentBlade
                );
                SetSelectedOption(
                    CurrentBlade
                );
            }
        }

        private void SetSelectedOption(
            string bladeOptionValue
        )
        {
            SelectedBladeOption = BladeOptions.Where(
                a => a.Value == bladeOptionValue
            ).FirstOrDefault();
        }
    }
}
