namespace EventHorizon.Game.Editor.Client.Zone.Components.FileEditor.Model
{
    public struct EditorFileErrorDetails
    {
        public bool HasError { get; }
        public string ScriptId { get; }
        public string Message { get; }
        public string ErrorLineContent { get; }
        public int Column { get; }

        public EditorFileErrorDetails(
            string scriptId
        )
        {
            HasError = false;
            ScriptId = scriptId;
            Message = string.Empty;
            ErrorLineContent = string.Empty;
            Column = 0;
        }

        public EditorFileErrorDetails(
            string scriptId,
            string message,
            string errorLineContent,
            int column
        )
        {
            HasError = true;
            ScriptId = scriptId;
            Message = message;
            ErrorLineContent = errorLineContent;
            Column = column;
        }
    }
}
