using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventHorizon.Blazor.BabylonJS.Pages.Testing.DITesting.Model
{
    public class DIRunHandlerImplementation : IDIRunHandler
    {
        public Task Handle(
            DIRunEvent @event
        )
        {
            return Task.CompletedTask;
        }
    }
}
