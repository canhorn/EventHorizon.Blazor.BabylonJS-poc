namespace EventHorizon.Game.Client.Systems.Entity.Model
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Entity.Model;
    using EventHorizon.Game.Client.Engine.Lifecycle.Model;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Client.Systems.Entity.Modules.Animation.Api;
    using EventHorizon.Game.Client.Systems.Entity.Modules.Animation.Model;
    using EventHorizon.Game.Client.Systems.Entity.Modules.ModelLoader.Api;
    using EventHorizon.Game.Client.Systems.Entity.Modules.ModelLoader.Model;
    using EventHorizon.Game.Client.Systems.Entity.Modules.Move.Api;
    using EventHorizon.Game.Client.Systems.Entity.Modules.Move.Model;
    using EventHorizon.Game.Client.Systems.Local.Modules.InView.Api;
    using EventHorizon.Game.Client.Systems.Local.Modules.InView.Model;
    using EventHorizon.Game.Client.Systems.Local.Modules.MeshManagement.Api;
    using EventHorizon.Game.Client.Systems.Local.Modules.MeshManagement.Model;
    using EventHorizon.Game.Client.Systems.Local.Modules.State.Api;
    using EventHorizon.Game.Client.Systems.Local.Modules.State.Model;
    using EventHorizon.Game.Client.Systems.Local.Modules.Transform.Api;
    using EventHorizon.Game.Client.Systems.Local.Modules.Transform.Model;

    public class StandardServerEntity
        : ServerLifecycleEntityBase
    {
        public StandardServerEntity(
            IObjectEntityDetails details
        ) : base(details)
        {
        }

        public override Task Initialize()
        {
            SetProperty(
                "resolveHeight",
                true
            );

            RegisterModule(
                ITransformModule.MODULE_NAME,
                new TransformModule(
                    this
                )
            );
            RegisterModule(
                IStateModule.MODULE_NAME,
                new StateModule(
                    this
                )
            );
            RegisterModule(
                IModelLoaderModule.MODULE_NAME,
                new ModelLoaderModule(
                    this
                )
            );
            RegisterModule(
                IMeshModule.MODULE_NAME,
                new MeshModule(
                    this,
                    new MeshModuleOptions(
                        false,
                        false
                    )
                )
            );
            RegisterModule(
                IMoveModule.MODULE_NAME,
                new MoveModule(
                    this
                )
            );
            RegisterModule(
                IStoppingModule.MODULE_NAME,
                new StoppingModule(
                    this
                )
            );
            // TODO: SELECTED_INDICATOR_MODULE_NAME
            //this.registerModule(
            //    SELECTED_INDICATOR_MODULE_NAME,
            //    new SelectedIndicatorModule(this)
            //);
            RegisterModule(
                IAnimationModule.MODULE_NAME,
                new AnimationModule(
                    this
                )
            );
            // TODO: Add back after more testing
            RegisterModule(
                IInViewModule.MODULE_NAME,
                new InViewModule(
                    this
                )
            );
            // TODO: INTERACTION_MODULE_NAME
            //this.registerModule(
            //    INTERACTION_MODULE_NAME,
            //    new InteractionModule(this)
            //);
            // TODO: INTERACTION_INDICATOR_MODULE_NAME
            //if (this.getProperty <{ active: boolean }> ("interactionState").active) {
            //    this.registerModule(
            //        INTERACTION_INDICATOR_MODULE_NAME,
            //        new InteractionIndicatorModule(this)
            //    );
            //}
            // TODO: Register Base Modules
            //this._commandService.send(
            //    createRegisterAllBaseModulesCommand({
            //        entity: this,
            //    })
            //);
            return Task.CompletedTask;
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
}
