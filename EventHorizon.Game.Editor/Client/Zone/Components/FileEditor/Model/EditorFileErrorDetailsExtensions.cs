namespace EventHorizon.Game.Editor.Client.Zone.Components.FileEditor.Model;

using EventHorizon.Zone.System.Server.Scripts.Model;

public static class EditorFileErrorDetailsExtensions
{
    public static EditorFileErrorDetails From(
        this EditorFileErrorDetails details,
        GeneratedServerScriptErrorDetailsModel? from
    )
    {
        if (from.IsNull())
        {
            return details;
        }

        return new EditorFileErrorDetails(
            details.ScriptId,
            from.Message,
            from.ErrorLineContent,
            from.Column
        );
    }
}
