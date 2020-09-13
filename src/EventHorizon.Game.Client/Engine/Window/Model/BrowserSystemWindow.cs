namespace EventHorizon.Game.Client.Engine.Window.Model
{
    using System;
    using EventHorizon.Game.Client.Engine.Window.Api;
    using Microsoft.AspNetCore.Components;

    public class BrowserSystemWindow
        : ISystemWindow
    {
        private readonly NavigationManager _navigationManager;

        public BrowserSystemWindow(
            NavigationManager navigationManager
        )
        {
            _navigationManager = navigationManager;
        }

        public void NavigateTo(
            string url
        )
        {
            _navigationManager.NavigateTo(
                url,
                true
            );
        }
    }
}
