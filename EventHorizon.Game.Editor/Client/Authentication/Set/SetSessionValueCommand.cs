namespace EventHorizon.Game.Editor.Client.Authentication.Set;

using EventHorizon.Game.Client.Core.Command.Model;
using MediatR;

/// <summary>
/// Save a value to local session storage
/// </summary>
public record SetSessionValueCommand(string Key, string Value)
    : IRequest<StandardCommandResult>;
