namespace EventHorizon.Game.Client.Systems.Player.Action.Model.Send;

using System;
using System.Diagnostics.CodeAnalysis;
using EventHorizon.Game.Client.Systems.Player.Action.Api;
using MediatR;

public struct InvokePlayerActionEvent : INotification
{
    public string Action { get; }
    public IPlayerActionData? Data { get; }

    public InvokePlayerActionEvent(string action)
        : this(action, null) { }

    public InvokePlayerActionEvent(string action, IPlayerActionData? data)
    {
        Action = action;
        Data = data;
    }
}
