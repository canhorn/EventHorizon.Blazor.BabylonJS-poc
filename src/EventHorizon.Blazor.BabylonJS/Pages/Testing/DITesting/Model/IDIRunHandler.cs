using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventHorizon.Blazor.BabylonJS.Pages.Testing.DITesting.Model
{
    public interface IDIRunHandler
    {
        Task Handle(
            DIRunEvent @event
        );
    }
}
