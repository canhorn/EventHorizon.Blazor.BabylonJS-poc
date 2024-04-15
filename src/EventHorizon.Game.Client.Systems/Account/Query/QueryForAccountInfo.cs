namespace EventHorizon.Game.Client.Systems.Account.Query;

using System;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Systems.Account.Api;
using MediatR;

public class QueryForAccountInfo : IRequest<CommandResult<IAccountInfo>> { }
