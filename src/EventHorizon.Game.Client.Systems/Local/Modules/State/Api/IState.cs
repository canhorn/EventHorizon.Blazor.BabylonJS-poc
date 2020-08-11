namespace EventHorizon.Game.Client.Systems.Local.Modules.State.Api
{
    using System.Threading.Tasks;

    public interface IState
    {
        long ClientId { get; }
        bool Remove { get; }
        string Name { get; }

        Task Update();
        Task Reset();
    }
}