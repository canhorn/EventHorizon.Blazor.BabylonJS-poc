﻿namespace EventHorizon.Game.Client.Engine.Input.Api;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

public interface IInputPressedEventSender
{
    Task SendEvent(IKeyEvent keyEvent);
}
