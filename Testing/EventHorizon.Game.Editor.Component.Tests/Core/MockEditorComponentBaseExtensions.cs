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
        editorComponentBaseMocks = new EditorComponentBaseMocks(
            new StringLocalizerMock<SharedResource>(),
            new MediatorMock(),
            new SenderMock(),
            new PublisherMock()
        );

        services.AddSingleton<Localizer<SharedResource>>(editorComponentBaseMocks.Localizer);
        services.AddSingleton<IMediator>(editorComponentBaseMocks.Mediator);
        services.AddSingleton<ISender>(editorComponentBaseMocks.Sender);
        services.AddSingleton<IPublisher>(editorComponentBaseMocks.Publisher);

        return services;
    }
}

public record EditorComponentBaseMocks(
    StringLocalizerMock<SharedResource> Localizer,
    MediatorMock Mediator,
    SenderMock Sender,
    PublisherMock Publisher
);
