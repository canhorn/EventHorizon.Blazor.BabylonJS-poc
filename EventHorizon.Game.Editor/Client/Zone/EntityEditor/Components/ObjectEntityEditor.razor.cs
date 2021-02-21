namespace EventHorizon.Game.Editor.Client.Zone.EntityEditor.Components
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Model;
    using EventHorizon.Game.Editor.Client.Localization;
    using EventHorizon.Game.Editor.Client.Localization.Api;
    using EventHorizon.Game.Editor.Client.Shared.Components;
    using EventHorizon.Game.Editor.Client.Shared.Toast.Model;
    using EventHorizon.Game.Editor.Client.Shared.Toast.Show;
    using EventHorizon.Game.Editor.Client.Zone.Api;
    using EventHorizon.Game.Editor.Client.Zone.EntityEditor.Model;
    using EventHorizon.Game.Editor.Client.Zone.EntityEditor.Components.Properties;
    using EventHorizon.Game.Editor.Client.Zone.Edited;
    using EventHorizon.Game.Editor.Zone.Editor.Clone;
    using Microsoft.AspNetCore.Components;

    public class ObjectEntityEditorModel
        : ObservableComponentBase,
        ObjectEntityDetailsEditedEventObserver
    {
        [CascadingParameter]
        public ZoneState ZoneState { get; set; } = null!;
        [CascadingParameter]
        public EntityEditorState EditorState { get; set; } = null!;
        [CascadingParameter]
        public IObjectEntityDetails Entity { get; set; } = null!;

        [Inject]
        public Localizer<SharedResource> Localizer { get; set; } = null!;

        public ObjectEntityDetailsModel EditEntity { get; set; }
        public NewPropertyModel NewPropertyModel { get; set; } = new NewPropertyModel();

        public bool IsPendingChange { get; set; }

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
            if (IsPendingChange)
            {
                return;
            }
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

        protected async Task HandlePropertyChanged(
            PropertyChangedArgs args
        )
        {
            IsPendingChange = true;
            switch (args.PropertyName)
            {
                case nameof(EditEntity.Name):
                    EditEntity.Name = args.Property.Cast<string>();
                    break;
                case nameof(EditEntity.Transform.Position):
                    EditEntity.Transform.Position = args.Property.Cast<ServerVector3>();
                    break;
                case nameof(EditEntity.Transform.Rotation):
                    EditEntity.Transform.Rotation = args.Property.Cast<ServerVector3>();
                    break;
                case nameof(EditEntity.Transform.Scaling):
                    EditEntity.Transform.Scaling = args.Property.Cast<ServerVector3>();
                    break;
                case nameof(EditEntity.Transform.ScalingDeterminant):
                    EditEntity.Transform.ScalingDeterminant = args.Property.Cast<decimal>();
                    break;
            }
            await Mediator.Publish(
                new ObjectEntityDetailsEditedEvent(
                    EditEntity
                )
            );
        }

        protected async Task HandleDataChanged(
            IDictionary<string, object> data
        )
        {
            IsPendingChange = true;
            EditEntity.Data = new Dictionary<string, object>(data);

            await Mediator.Publish(
                new ObjectEntityDetailsEditedEvent(
                    EditEntity
                )
            );
        }

        public async Task HandleRemoveData(
            string key
        )
        {
            // TODO: Add Delete Confirmation Modal
            IsPendingChange = true;
            EditEntity.Data.Remove(
                key
            );

            await Mediator.Publish(
                new ObjectEntityDetailsEditedEvent(
                    EditEntity
                )
            );
        }

        public async Task HandleAddProperty()
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
            IsPendingChange = true;
            EditEntity.Data.Add(
                NewPropertyModel.Name,
                ZoneState.EditorState.Metadata.GetDefaultValueForPropertyName(
                    NewPropertyModel.Name
                )
            );
            NewPropertyModel.Name = string.Empty;
            await Mediator.Publish(
                new ObjectEntityDetailsEditedEvent(
                    EditEntity
                )
            );
        }

        public async Task Handle(
            ObjectEntityDetailsEditedEvent args
        )
        {
            if (args.Entity.GlobalId != EditEntity.GlobalId)
            {
                // Mostly likely triggered from here. ;)
                return;
            }
            var result = await Mediator.Send(
                new CloneObjectEntityDetailsCommand(
                    args.Entity
                )
            );
            if (result.Success.IsNotTrue())
            {
                await Mediator.Publish(
                    new ShowMessageEvent(
                        Localizer["Entity Edit"],
                        Localizer["Failed to create temporary Entity to edit. Reason: {0}", result.ErrorCode],
                        MessageLevel.Error
                    )
                );
                return;
            }
            IsPendingChange = true;
            EditEntity = result.Result;
            await InvokeAsync(StateHasChanged);
        }
    }
}
