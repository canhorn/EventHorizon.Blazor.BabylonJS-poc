namespace EventHorizon.Game.Client.Engine.Systems.Camera.Query;

using System;
using EventHorizon.Game.Client.Core.Query.Model;
using EventHorizon.Game.Client.Engine.Systems.Camera.Model;
using MediatR;

public class QueryForActiveCamera : IRequest<QueryResult<ICamera>> { }
