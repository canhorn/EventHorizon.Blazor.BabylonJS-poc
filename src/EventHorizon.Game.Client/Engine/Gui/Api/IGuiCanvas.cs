namespace EventHorizon.Game.Client.Engine.Gui.Api
{
    using System;
    using System.Threading.Tasks;

    public interface IGuiCanvas
    {
        Task Initialize();
        Task Dispose();
        void AddControl(
            IGuiControl control
        );
    }
}
