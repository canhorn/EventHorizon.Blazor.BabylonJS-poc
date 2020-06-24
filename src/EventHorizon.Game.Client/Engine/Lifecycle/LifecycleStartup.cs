namespace EventHorizon.Game.Client.Engine.Lifecycle
{
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Api;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Model;
    using Microsoft.Extensions.DependencyInjection;

    public static class LifecycleStartup
    {
        public static IServiceCollection AddEngineLifecycleServices(
            this IServiceCollection services
        ) => services
            .AddSingleton<IRegisterBeforeRenderable, RegisterBeforeRenderableBase>()
            .AddSingleton<IRegisterDisposable, RegisterDisposableBase>()
            .AddSingleton<IRegisterDrawable, RegisterDrawableBase>()
            .AddSingleton<IRegisterInitializable, RegisterInitializableBase>()
            .AddSingleton<IRegisterUpdatable, RegisterUpdatableBase>()
        ;
    }
}
