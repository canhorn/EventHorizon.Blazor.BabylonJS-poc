namespace EventHorizon.Game.Client.Engine.Debugging.Model
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Blazor.Interop;

    public static class DebuggingLogger
    {
        public static ValueTask StartClientLogging()
        {
            return EventHorizonBlazorInterop.RunScript(
                "enableLogging",
                "if(!window['ENABLED']) { window['ENABLED'] = true; setTimeout(() => window['ENABLED'] = false, 100); }",
                new { }
            );
        }
    }
}
