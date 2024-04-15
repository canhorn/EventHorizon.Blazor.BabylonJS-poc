namespace EventHorizon.Game.Client.Engine.Loading.Show;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Rendering.Api;
using MediatR;

public class ShowLoadingUIEventDisplayLoadingUIHandler : INotificationHandler<ShowLoadingUIEvent>
{
    private readonly IRenderingEngine _renderingEngine;

    public ShowLoadingUIEventDisplayLoadingUIHandler(IRenderingEngine renderingEngine)
    {
        _renderingEngine = renderingEngine;
    }

    public Task Handle(ShowLoadingUIEvent notification, CancellationToken cancellationToken)
    {
        _renderingEngine.GetEngine().DisplayLoadingUI();

        return Task.CompletedTask;
    }
}
