namespace EventHorizon.Game.Client.Engine.Services.Model;

using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Exceptions;
using EventHorizon.Game.Client.Engine.Services.Api;

public class StandardGameService : IGameService
{
    private IGame? _game;

    public IGame Get()
    {
#if DEBUG
        if (_game == null)
        {
            throw new GameRuntimeException(
                "game_is_null",
                "The Game has not been set."
            );
        }
#endif
        return _game;
    }

    public void Set(IGame game)
    {
#if DEBUG
        if (_game != null)
        {
            throw new GameRuntimeException(
                "game_already_set",
                "The Game service already has an set Game"
            );
        }
#endif
        _game = game;
    }

    public async Task Dispose()
    {
        if (_game != null)
        {
            await _game.Dispose();
            _game = null;
        }
    }
}
