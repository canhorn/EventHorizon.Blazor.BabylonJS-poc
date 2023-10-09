namespace EventHorizon.Game.Client.Systems.ClientScripts.Fetch;

using System;

using EventHorizon.Game.Client.Core.Query.Model;
using EventHorizon.Game.Client.Systems.ClientScripts.Model;

using MediatR;

public class FetchClientScriptsAssembly
    : IRequest<QueryResult<ClientScriptsAssemblyResult>> { }
