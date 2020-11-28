namespace EventHorizon.Game.Editor.Client.Zone.Components.EntityEditor
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Editor.Client.Localization;
    using EventHorizon.Game.Editor.Client.Localization.Api;
    using EventHorizon.Game.Editor.Client.Shared.Toast.Model;
    using EventHorizon.Game.Editor.Client.Shared.Toast.Show;
    using EventHorizon.Game.Editor.Client.Zone.Api;
    using EventHorizon.Game.Editor.Client.Zone.Components.EntityEditor.Model;
    using EventHorizon.Game.Editor.Zone.Editor.Clone;
    using MediatR;
    using Microsoft.AspNetCore.Components;

    public class ObjectEntityEditorModel
        : ComponentBase
    {
        [CascadingParameter]
        public ZoneState ZoneState { get; set; } = null!;
        [CascadingParameter]
        public EntityEditorState EditorState { get; set; } = null!;
        [CascadingParameter]
        public IObjectEntityDetails Entity { get; set; } = null!;

        [Inject]
        public IMediator Mediator { get; set; } = null!;
        [Inject]
        public Localizer<SharedResource> Localizer { get; set; } = null!;

        public IObjectEntityDetails EditEntity { get; set; }
        public NewPropertyModel NewPropertyModel { get; set; } = new NewPropertyModel();

        public bool IsHelpOpen { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await Setup();
            await base.OnInitializedAsync();
        }
        protected override async Task OnParametersSetAsync()
        {
            await Setup();
            await base.OnParametersSetAsync();
        }

        private async Task Setup()
        {
            var result = await Mediator.Send(
                   new CloneObjectEntityDetailsCommand(
                       Entity
                   )
               );
            if (result.Success.IsNotTrue())
            {
                await Mediator.Send(
                    new ShowMessageEvent(
                        Localizer["Entity Edit"],
                        Localizer["Failed to create temporary Entity to edit. Reason: {0}", result.ErrorCode],
                        MessageLevel.Error
                    )
                );
                return;
            }
            EditEntity = result.Result;
        }

        public Task HandleSave()
        {
            return EditorState.OnSave(
                EditEntity
            );
        }

        public void HandleOpenHelp()
        {
            IsHelpOpen = true;
        }
        public void HandleCloseHelp()
        {
            IsHelpOpen = false;
        }

        public void HandleAddProperty()
        {
            var (valid, _) = NewPropertyModel.Validate();
            if (!valid)
            {
                return;
            }
            if (EditEntity.Data.ContainsKey(NewPropertyModel.Name))
            {
                NewPropertyModel.IsValid = false;
                NewPropertyModel.ErrorMessage = Localizer["property_already_exists"];
                return;
            }
            EditEntity.Data.Add(
                NewPropertyModel.Name,
                ZoneState.EditorState.Metadata.GetDefaultValueForPropertyName(
                    NewPropertyModel.Name
                )
            );
            NewPropertyModel.Name = string.Empty;
        }
    }
}
