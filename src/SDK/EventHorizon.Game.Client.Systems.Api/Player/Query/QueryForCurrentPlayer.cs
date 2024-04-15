namespace EventHorizon.Game.Client.Systems.Player.Query;

using System;
using EventHorizon.Game.Client.Core.Query.Model;
using EventHorizon.Game.Client.Systems.Player.Api;
using MediatR;

public class QueryForCurrentPlayer : IRequest<QueryResult<IPlayerEntity>> { }
