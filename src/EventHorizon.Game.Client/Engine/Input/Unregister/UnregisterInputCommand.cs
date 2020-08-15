namespace EventHorizon.Game.Client.Engine.Input.Unregister
{
    using MediatR;

    public struct UnregisterInputCommand
        : IRequest
    {
        public string Handle { get; }

        public UnregisterInputCommand(
            string handle
        )
        {
            Handle = handle;
        }
    }
}
