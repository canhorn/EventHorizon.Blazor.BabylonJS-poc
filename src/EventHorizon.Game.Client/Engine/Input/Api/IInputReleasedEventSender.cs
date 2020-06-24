using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventHorizon.Game.Client.Engine.Input.Api
{
    public interface IInputReleasedEventSender
    {
        Task SendEvent(IKeyEvent keyEvent);
    }
}
