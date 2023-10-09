namespace EventHorizon.Game.Client.Systems.Dialog.Query;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Query.Model;
using EventHorizon.Game.Client.Systems.ClientAssets.Fetch;
using EventHorizon.Game.Client.Systems.Dialog.Api;

using MediatR;

public class QueryForDialogTreeHandler
    : IRequestHandler<QueryForDialogTree, QueryResult<DialogTree>>
{
    private readonly IMediator _mediator;
    private readonly DialogState _state;

    public QueryForDialogTreeHandler(IMediator mediator, DialogState state)
    {
        _mediator = mediator;
        _state = state;
    }

    public async Task<QueryResult<DialogTree>> Handle(
        QueryForDialogTree request,
        CancellationToken cancellationToken
    )
    {
        if (string.IsNullOrEmpty(request.Id))
        {
            return new QueryResult<DialogTree>("dialog_tree_is_null_or_empty");
        }
        // Check for DialogTree in State
        var dialogTreeOption = _state.Get(request.Id);
        // Check for existing DialogTree
        if (dialogTreeOption.HasValue)
        {
            // Return Result
            return new QueryResult<DialogTree>(dialogTreeOption.Value);
        }

        // Query Client Assets for Dialog Tree
        var dialogTreeClientAsset = await _mediator.Send(
            new FetchClientAssetQuery(request.Id),
            cancellationToken
        );
        // If found, Set in Cache and return
        if (dialogTreeClientAsset.Success)
        {
            if (dialogTreeClientAsset.Result.Config is DialogTree dialogTree)
            {
                _state.Set(request.Id, dialogTree);
                return new QueryResult<DialogTree>(dialogTree);
            }
        }

        return new QueryResult<DialogTree>("tree_not_found");
    }
}
