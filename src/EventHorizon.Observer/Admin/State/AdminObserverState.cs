namespace EventHorizon.Observer.Admin.State;

using System;
using System.Collections.Generic;
using System.Text;

using EventHorizon.Observer.Admin.Model;

public interface AdminObserverState
{
    void RegisterAdminObserver(AdminObserverBase observer);
    void RemoveAdminObserver(AdminObserverBase observer);
}
