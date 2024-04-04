namespace EventHorizon.Game.Client.Engine.Input.Model;

public interface KeyInputBase
{
    public string Key { get; }
    public string Type { get; }

    #region  PlayerMove Type
    public MoveDirection? Pressed { get; }
    public MoveDirection? Released { get; }
    #endregion

    #region SetActiveCamera Type
    public string? Camera { get; }
    #endregion
}
