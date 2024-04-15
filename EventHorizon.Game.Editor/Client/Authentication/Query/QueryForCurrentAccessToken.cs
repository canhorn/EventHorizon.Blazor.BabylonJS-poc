namespace EventHorizon.Game.Editor.Client.Authentication.Query;

using EventHorizon.Game.Client.Core.Command.Model;
using MediatR;

public class QueryForCurrentAccessToken : IRequest<CommandResult<string>> { }
