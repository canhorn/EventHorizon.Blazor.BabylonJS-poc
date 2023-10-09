namespace EventHorizon.Game.Editor.Client.AssetManagement.Model;

public class UploadImportFileResult
{
    public string Service { get; }
    public string Url { get; }

    public UploadImportFileResult(string service, string url)
    {
        Service = service;
        Url = url;
    }
}
