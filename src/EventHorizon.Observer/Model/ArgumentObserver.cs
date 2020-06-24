using System.Threading.Tasks;

namespace EventHorizon.Observer.Model
{
    public interface ArgumentObserver<TArgs> : ObserverBase
    {
        Task Handle(
            TArgs args
        );
    }
}
