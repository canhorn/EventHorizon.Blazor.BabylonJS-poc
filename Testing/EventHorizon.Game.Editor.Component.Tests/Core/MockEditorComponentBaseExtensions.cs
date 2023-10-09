namespace EventHorizon.Game.Editor.Client.Core;

using EventHorizon.Game.Editor.Client.Localization;
using EventHorizon.Game.Editor.Client.Localization.Api;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

public static class MockEditorComponentBaseExtensions
{
    public static IServiceCollection AddEditorComponentBase(
        this IServiceCollection services,
        out EditorComponentBaseMocks editorComponentBaseMocks
    )
    {
        editorComponentBaseMocks = new EditorComponentBaseMocks
        {
            Localizer = new StringLocalizerMock<SharedResource>(),
            Mediator = new MediatorMock(),
            Sender = new SenderMock(),
            Publisher = new PublisherMock(),
        };

        services.AddSingleton<Localizer<SharedResource>>(
            editorComponentBaseMocks.Localizer
        );
        services.AddSingleton<IMediator>(editorComponentBaseMocks.Mediator);
        services.AddSingleton<ISender>(editorComponentBaseMocks.Sender);
        services.AddSingleton<IPublisher>(editorComponentBaseMocks.Publisher);

        return services;
    }
}

public class EditorComponentBaseMocks
{
    public StringLocalizerMock<SharedResource> Localizer { get; internal set; }
    public MediatorMock Mediator { get; internal set; }
    public SenderMock Sender { get; internal set; }
    public PublisherMock Publisher { get; internal set; }
}
