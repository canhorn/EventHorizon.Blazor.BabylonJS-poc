namespace EventHorizon.Game.Client.Engine.Model.Scripting.Services
{
    using MediatR;

    public interface ClientScriptServices
    {
        IMediator Mediator { get; }
    }
}
