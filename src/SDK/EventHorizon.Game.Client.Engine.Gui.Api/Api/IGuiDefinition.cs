namespace EventHorizon.Game.Client.Engine.Gui.Api;

using System;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Lifecycle.Api;

public interface IGuiDefinition : ILifecycleEntity
{
    string GuiId { get; }
    string LayoutId { get; }
    Task Activate();
    Task Hide();
    Task Show();

    /**
     * This is used to connect a GUI to a world elements, ie Mesh
     * @param obj The object that this GUi should be "linked" to.
     */
    Task LinkWith(object obj);
}
