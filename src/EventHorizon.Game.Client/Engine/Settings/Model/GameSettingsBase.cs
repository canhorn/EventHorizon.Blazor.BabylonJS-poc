namespace EventHorizon.Game.Client.Engine.Settings.Model
{
    using EventHorizon.Game.Client.Core.Configuration;
    using EventHorizon.Game.Client.Engine.Settings.Api;

    public class GameSettingsBase
        : IGameSettings
    {
        public string CanvasTagId
        {
            get
            {
                return Configuration.GetConfig<string>(
                   "APPEND_TO_TAG"
               );
            }
        }

        public string GetProperty(
            string key
        )
        {
            return Configuration.GetConfig<string>(
               key
           );
        }
    }
}
