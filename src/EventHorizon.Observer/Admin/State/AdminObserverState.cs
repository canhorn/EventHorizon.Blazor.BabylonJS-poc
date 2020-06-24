using EventHorizon.Observer.Admin.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventHorizon.Observer.Admin.State
{
    public interface AdminObserverState
    {
        void RegisterAdminObserver(
            AdminObserverBase observer
        );
        void RemoveAdminObserver(
            AdminObserverBase observer
        );
    }
}
