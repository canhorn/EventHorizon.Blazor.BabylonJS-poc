namespace EventHorizon.Game.Client.Engine.Systems.ClientAction.Model;

using System;

public record ExternalAction(Type Event, Type Observer);
