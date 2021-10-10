namespace EventHorizon.Game.Client.Systems.Player.Modules.Input.Api
{
    using System.Threading.Tasks;

    using EventHorizon.Game.Client.Engine.Input.Api;
    using EventHorizon.Game.Client.Engine.Systems.Module.Api;

    public interface InputModule
        : IModule
    {
        public static string MODULE_NAME = "INPUT_MODULE_NAME";

        Task<Option<string>> RegisterInput(
            InputOptions options
        );

        Task UnRegisterInput(
            string inputHandler
        );

        Task ResetToDefaultLayout();
    }
}
