namespace EventHorizon.Game.Editor.Client.Zone.Components.FileEditor.Model
{
    using EventHorizon.Game.Editor.Zone.Editor.Services.Model;

    public class FileEditorState
    {
        public EditorFile EditorFile { get; set; } = new EditorFile();
        public EditorNode EditorNode { get; set; } = new EditorNode();
        public EditorFileErrorDetails FileErrorDetails { get; set; } = new EditorFileErrorDetails(string.Empty);
    }
}
