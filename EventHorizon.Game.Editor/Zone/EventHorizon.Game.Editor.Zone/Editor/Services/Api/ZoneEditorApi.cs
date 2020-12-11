namespace EventHorizon.Game.Editor.Zone.Editor.Services.Api
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Editor.Zone.Editor.Services.Model;

    public interface ZoneEditorApi
    {
        Task<CommandResult<EditorNodeList>> GetEditorZoneList();

        Task<EditorFile> GetEditorFileContent(
            IList<string> path,
            string fileName
        );
        Task<EditorResponse> SaveEditorFileContent(
            IList<string> path,
            string fileName,
            string content
        );
        Task<EditorResponse> CreateEditorFile(
            IList<string> path,
            string fileName
        );
        Task<EditorResponse> CreateEditorFolder(
            IList<string> path,
            string folderName
        );
        Task<EditorResponse> DeleteEditorFile(
            IList<string> path,
            string fileName
        );
        Task<EditorResponse> DeleteEditorFolder(
            IList<string> path,
            string folderName
        );
    }
}
