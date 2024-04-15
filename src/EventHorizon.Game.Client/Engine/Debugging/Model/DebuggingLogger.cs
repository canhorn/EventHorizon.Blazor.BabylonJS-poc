namespace EventHorizon.Game.Client.Engine.Debugging.Model;

using System;
using System.Threading.Tasks;
using EventHorizon.Blazor.Interop;

public static class DebuggingLogger
{
    public static IDebuggingLoggerGroup CreateLoggerGroup()
    {
        return new StandardDebuggingLoggerGroup();
    }

    public static async Task EnableClientLogging()
    {
        await EventHorizonBlazorInterop.RunScript(
            "enableLogging",
            "// if(!window['ENABLED']){ window['ENABLED'] = true; setTimeout(() => window['ENABLED'] = false, 100); }",
            new { }
        );
    }
}

public class StandardDebuggingLoggerGroup : IDebuggingLoggerGroup
{
    private string _guid = Guid.NewGuid().ToString();

    public StandardDebuggingLoggerGroup()
    {
        Console.WriteLine($"=== Start Logging | {_guid}");
    }

    public void Dispose()
    {
        Console.WriteLine($"==== End Logging | {_guid}");
    }
}

public interface IDebuggingLoggerGroup : IDisposable { }
