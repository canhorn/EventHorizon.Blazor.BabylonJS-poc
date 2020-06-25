namespace EventHorizon.Game.Client.Engine.Input.Register
{
    using EventHorizon.Game.Client.Engine.Input.Api;
    using MediatR;

    public struct RegisterInputCommand : IRequest<string>
    {
        public InputOptions InputOptions { get; }

        public RegisterInputCommand(
            InputOptions inputOptions
        )
        {
            InputOptions = inputOptions;
        }
    }
}
