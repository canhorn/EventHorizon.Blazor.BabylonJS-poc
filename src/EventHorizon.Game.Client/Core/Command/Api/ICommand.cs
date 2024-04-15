namespace EventHorizon.Game.Client.Core.Command.Api;

using EventHorizon.Game.Client.Core.Command.Model;
using MediatR;

public interface ICommand<T> : IRequest<CommandResult<T>> { }
