namespace EventHorizon.Game.Editor.Desktop;

using System;
using Microsoft.Extensions.DependencyInjection;
using Photino.Blazor;

class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        var appBuilder = PhotinoBlazorAppBuilder.CreateDefault(args);
        appBuilder.Services.AddLogging();

        // register root component
        appBuilder.RootComponents.Add<App>("app");

        var app = appBuilder.Build();

        // customize window
        app.MainWindow.SetIconFile("favicon.ico").SetTitle("Photino Hello World");

        AppDomain.CurrentDomain.UnhandledException += (sender, error) =>
        {
            app.MainWindow.ShowMessage("Fatal exception", error.ExceptionObject.ToString());
        };

        app.Run();
    }
}
