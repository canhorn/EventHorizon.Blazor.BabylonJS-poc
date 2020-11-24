namespace EventHorizon.Game.Editor.Zone.Editor.Services.Service
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Zone.Editor.Services.Api;
    using EventHorizon.Game.Editor.Zone.Editor.Services.Model;
    using Microsoft.AspNetCore.SignalR.Client;

    public class SignalrZoneEditorApi
        : ZoneEditorApi
    {
        private readonly HubConnection _hubConnection;

        internal SignalrZoneEditorApi(
            HubConnection hubConnection
        )
        {
            _hubConnection = hubConnection;
        }

        public Task<EditorNodeList> GetEditorZoneList()
        {
            if (_hubConnection.IsNull())
            {
                return new EditorNodeList()
                    .FromResult();
            }
            return _hubConnection.InvokeAsync<EditorNodeList>(
                "GetEditorState"
            );
        }

        public Task<EditorFile> GetEditorFileContent(
            IList<string> path,
            string fileName
        ) => _hubConnection.InvokeAsync<EditorFile>(
            "GetEditorFileContent",
            path,
            fileName
        );

        public Task<EditorResponse> SaveEditorFileContent(
            IList<string> path,
            string fileName,
            string content
        ) => _hubConnection.InvokeAsync<EditorResponse>(
            "SaveEditorFileContent",
            path,
            fileName,
            content
        );

        public Task<EditorResponse> CreateEditorFile(
            IList<string> path,
            string fileName
        ) => _hubConnection.InvokeAsync<EditorResponse>(
            "CreateEditorFile",
            path,
            fileName
        );

        public Task<EditorResponse> CreateEditorFolder(
            IList<string> path,
            string folderName
        ) => _hubConnection.InvokeAsync<EditorResponse>(
            "CreateEditorFolder",
            path,
            folderName
        );

        public Task<EditorResponse> DeleteEditorFile(
            IList<string> path,
            string fileName
        ) => _hubConnection.InvokeAsync<EditorResponse>(
            "DeleteEditorFile",
            path,
            fileName
        );

        public Task<EditorResponse> DeleteEditorFolder(
            IList<string> path,
            string folderName
        ) => _hubConnection.InvokeAsync<EditorResponse>(
            "DeleteEditorFolder",
            path,
            folderName
        );
    }
}
