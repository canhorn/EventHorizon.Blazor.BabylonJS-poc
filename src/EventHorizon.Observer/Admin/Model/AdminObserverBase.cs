using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventHorizon.Observer.Admin.Model
{
    public interface AdminObserverBase
    {
        Task HandleAdminTrigger(
            string observerFullName,
            params object[] args
        );
    }
}
