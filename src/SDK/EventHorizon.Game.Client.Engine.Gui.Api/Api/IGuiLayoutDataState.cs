namespace EventHorizon.Game.Client.Engine.Gui.Api;

public interface IGuiLayoutDataState
{
    Option<IGuiLayoutData> Get(string id);

    void Set(IGuiLayoutData layout);

    void Clear();
}
