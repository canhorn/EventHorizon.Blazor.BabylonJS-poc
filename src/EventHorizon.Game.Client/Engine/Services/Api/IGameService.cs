namespace EventHorizon.Game.Client.Engine.Services.Api
{
    using System.Threading.Tasks;

    public interface IGameService
    {
        IGame Get();
        void Set(
            IGame game
        );
        Task Dispose();
    }
}
