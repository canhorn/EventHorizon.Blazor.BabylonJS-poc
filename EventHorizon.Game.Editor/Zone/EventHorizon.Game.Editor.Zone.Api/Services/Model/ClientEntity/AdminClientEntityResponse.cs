namespace EventHorizon.Game.Editor.Services.Model.ClientEntity;

using EventHorizon.Game.Client.Engine.Systems.Entity.Model;

public class AdminClientEntityResponse
{
    public bool Success { get; set; }
    public string? ErrorCode { get; set; }
    public ObjectEntityDetailsModel? ClientEntity { get; set; }
}
