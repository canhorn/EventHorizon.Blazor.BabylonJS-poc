namespace EventHorizon.Game.Editor.Client.AssetManagement.Model;

public class AccessPermission
{
    public bool Copy = true;
    public bool Download = true;
    public bool Write = true;
    public bool WriteContents = true;
    public bool Read = true;
    public bool Upload = true;
    public string Message = string.Empty;
}
