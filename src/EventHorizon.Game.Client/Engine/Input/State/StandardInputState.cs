namespace EventHorizon.Game.Client.Engine.Input.State;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Input.Api;

public class StandardInputState : IRegisterInput, IUnregisterInput, IInputState
{
    private readonly IDictionary<string, InputOptions> _inputOptions =
        new Dictionary<string, InputOptions>();

    public string Register(InputOptions options)
    {
        var handle = Guid.NewGuid().ToString();
        _inputOptions.Add(handle, options);
        return handle;
    }

    public void Unregister(string handle)
    {
        _inputOptions.Remove(handle);
    }

    public IEnumerable<InputOptions> Where(Func<InputOptions, bool> predicate)
    {
        return _inputOptions.Values.Where(predicate);
    }
}
