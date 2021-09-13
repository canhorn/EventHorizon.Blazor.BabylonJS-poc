namespace EventHorizon.Game.Editor.Client.Zone.Components.FileEditor
{
    using System.Threading.Tasks;

    using BlazorMonaco;
    using BlazorMonaco.Bridge;

    using EventHorizon.Game.Editor.Client.Localization;
    using EventHorizon.Game.Editor.Client.Localization.Api;
    using EventHorizon.Game.Editor.Client.Shared.Toast.Model;
    using EventHorizon.Game.Editor.Client.Shared.Toast.Show;
    using EventHorizon.Game.Editor.Client.Zone.Components.FileEditor.Model;
    using EventHorizon.Game.Editor.Zone.Editor.Services.Save;

    using MediatR;

    using Microsoft.AspNetCore.Components;

    public class ZoneFileEditorComponentModel
        : ComponentBase
    {
        [CascadingParameter]
        public FileEditorState FileEditorState { get; set; } = null!;

        [Inject]
        public Localizer<SharedResource> Localizer { get; set; } = null!;
        [Inject]
        public IMediator Mediator { get; set; } = null!;

        public MonacoEditor MonacoEditor { get; set; } = null!;

        public bool IsDetailsModalOpen { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (MonacoEditor.IsNotNull()
                && FileEditorState.EditorNode.IsNotNull()
                && FileEditorState.EditorFile.IsNotNull()
            )
            {
                await MonacoEditor.SetValue(
                    FileEditorState.EditorFile.Content
                );
            }
            await base.OnParametersSetAsync();
        }

        public StandaloneEditorConstructionOptions BuildConstructionOptions(
            MonacoEditor _
        )
        {
            return new StandaloneEditorConstructionOptions
            {
                Theme = "vs-dark",
                Language = FileEditorState.EditorNode.Properties.Language,
                Value = FileEditorState.EditorFile.Content,
                AutomaticLayout = true,
            };
        }

        public async Task HandleSaveFile()
        {
            var value = await MonacoEditor.GetValue();
            var result = await Mediator.Send(
                new SaveEditorFileContentCommand(
                    FileEditorState.EditorNode.Path,
                    FileEditorState.EditorNode.Name,
                    value
                )
            );
            if (result.Successful.IsNotTrue())
            {
                await Mediator.Publish(
                    new ShowMessageEvent(
                        Localizer["File Save Status"],
                        Localizer[
                            "File failed to save, Error Code of '{0}'",
                            result.ErrorCode
                        ],
                        MessageLevel.Error
                    )
                );
                return;
            }
            await Mediator.Publish(
                new ShowMessageEvent(
                    Localizer["File Save Status"],
                    Localizer["File was Successfully Saved"],
                    MessageLevel.Success
                )
            );
        }

        public void HandleOpenDetails()
        {
            IsDetailsModalOpen = true;
        }

        public void HandleCloseDeatils()
        {
            IsDetailsModalOpen = false;
        }
    }
}
