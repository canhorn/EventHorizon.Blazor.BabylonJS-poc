using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Blazor.BabylonJS.Pages.Testing.DITesting.Model;
using MediatR;

namespace EventHorizon.Blazor.BabylonJS.Pages.Testing.MediatrTesting.Model
{
    public class RunMediatrRequestHandler
        : IRequestHandler<RunMediatrRequest, string>
    {
        private readonly IDIRunHandler _handler;

        public RunMediatrRequestHandler(IDIRunHandler handler, IDIRunHandler handler2)
        {
            _handler = handler;
            _handler = handler2;
        }

        public Task<string> Handle(
            RunMediatrRequest request,
            CancellationToken cancellationToken
        )
        {
            return Task.FromResult("Hello");
        }
    }
}
