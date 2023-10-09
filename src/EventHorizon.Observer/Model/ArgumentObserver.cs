namespace EventHorizon.Observer.Model;

using System.Threading.Tasks;

public interface ArgumentObserver<TArgs> : ObserverBase
{
    Task Handle(TArgs args);
}
