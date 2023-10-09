namespace EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;

using System;

public interface IClientActionDataResolver
{
    T Resolve<T>(string argumentName);
    T? ResolveNullable<T>(string argumentName);
}
