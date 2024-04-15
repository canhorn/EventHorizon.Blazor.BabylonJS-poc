namespace EventHorizon.Activity;

using EventHorizon.Activity.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

public static class ActivityStartupExtensions
{
    public static IServiceCollection AddActivityServices(this IServiceCollection services) =>
        services.AddTransient(
            typeof(IPipelineBehavior<,>),
            typeof(PublishActivityEventsBehavior<,>)
        );
}
