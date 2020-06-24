using System;
using System.Threading.Tasks;

namespace EventHorizon.Game.Client.Engine.Input.Api
{
    public interface IInputOptions
    {
        string Key { get; }
        Action<IInputKeyEvent> Pressed { get; }
        Action<IInputKeyEvent> Released { get; }
    }
}