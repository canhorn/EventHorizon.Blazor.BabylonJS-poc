namespace EventHorizon.Observer.Admin.Model
{
    using System.Threading.Tasks;

    public interface AdminObserverBase
    {
        Task HandleAdminTrigger(
            string observerFullName,
            params object[] args
        );
    }
}
