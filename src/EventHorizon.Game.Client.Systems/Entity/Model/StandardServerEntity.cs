namespace EventHorizon.Game.Client.Systems.Entity.Model;

using System;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Lifecycle.Model;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
using EventHorizon.Game.Client.Systems.Entity.Modules.Animation.Api;
using EventHorizon.Game.Client.Systems.Entity.Modules.Animation.Model;
using EventHorizon.Game.Client.Systems.Entity.Modules.Details.Api;
using EventHorizon.Game.Client.Systems.Entity.Modules.Details.Model;
using EventHorizon.Game.Client.Systems.Entity.Modules.Interaction.Api;
using EventHorizon.Game.Client.Systems.Entity.Modules.Interaction.Model;
using EventHorizon.Game.Client.Systems.Entity.Modules.InteractionIndicator.Api;
using EventHorizon.Game.Client.Systems.Entity.Modules.InteractionIndicator.Model;
using EventHorizon.Game.Client.Systems.Entity.Modules.ModelLoader.Api;
using EventHorizon.Game.Client.Systems.Entity.Modules.ModelLoader.Model;
using EventHorizon.Game.Client.Systems.Entity.Modules.Move.Api;
using EventHorizon.Game.Client.Systems.Entity.Modules.Move.Model;
using EventHorizon.Game.Client.Systems.Entity.Modules.SelectedCompanionIndicator.Api;
using EventHorizon.Game.Client.Systems.Entity.Modules.SelectedCompanionIndicator.Model;
using EventHorizon.Game.Client.Systems.Entity.Modules.SelectedIndicator.Api;
using EventHorizon.Game.Client.Systems.Entity.Modules.SelectedIndicator.Model;
using EventHorizon.Game.Client.Systems.EntityModule.Register;
using EventHorizon.Game.Client.Systems.Local.Modules.InView.Api;
using EventHorizon.Game.Client.Systems.Local.Modules.InView.Model;
using EventHorizon.Game.Client.Systems.Local.Modules.MeshManagement.Api;
using EventHorizon.Game.Client.Systems.Local.Modules.MeshManagement.Model;
using EventHorizon.Game.Client.Systems.Local.Modules.State.Api;
using EventHorizon.Game.Client.Systems.Local.Modules.State.Model;
using EventHorizon.Game.Client.Systems.Local.Modules.Transform.Api;
using EventHorizon.Game.Client.Systems.Local.Modules.Transform.Model;
using MediatR;

public class StandardServerEntity : ServerLifecycleEntityBase
{
    protected readonly IMediator _mediator = GameServiceProvider.GetService<IMediator>();

    public StandardServerEntity(IObjectEntityDetails details)
        : base(details) { }

    public override async Task Initialize()
    {
        SetProperty("resolveHeight", true);

        RegisterModule(DetailsModule.MODULE_NAME, new StandardDetailsModule(this));

        RegisterModule(ITransformModule.MODULE_NAME, new TransformModule(this));
        RegisterModule(IStateModule.MODULE_NAME, new StateModule(this));
        RegisterModule(IModelLoaderModule.MODULE_NAME, new ModelLoaderModule(this));
        RegisterModule(
            IMeshModule.MODULE_NAME,
            new MeshModule(this, new MeshModuleOptions(false, false))
        );
        RegisterModule(IMoveModule.MODULE_NAME, new MoveModule(this));
        RegisterModule(IStoppingModule.MODULE_NAME, new StoppingModule(this));
        RegisterModule(
            SelectedIndicatorModule.MODULE_NAME,
            new StandardSelectedIndicatorModule(this)
        );
        RegisterModule(IAnimationModule.MODULE_NAME, new AnimationModule(this));
        RegisterModule(IInViewModule.MODULE_NAME, new InViewModule(this));
        RegisterModule(InteractionModule.MODULE_NAME, new StandardInteractionModule(this));
        RegisterModule(
            InteractionIndicatorModule.MODULE_NAME,
            new StandardInteractionIndicatorModule(this)
        );
        RegisterModule(
            SelectedCompanionIndicatorModule.MODULE_NAME,
            new StandardSelectedCompanionIndicatorModule(this)
        );

        await _mediator.Send(new RegisterAllBaseModulesOnEntityCommand(this));
    }

    public override Task PostInitialize()
    {
        return base.PostInitialize();
    }

    public override Task Draw()
    {
        return Task.CompletedTask;
    }
}
