namespace EventHorizon.Game.Client.Systems.Player.Model
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Api;
    using EventHorizon.Game.Client.Systems.Entity.Model;
    using EventHorizon.Game.Client.Systems.EntityModule.Register;
    using EventHorizon.Game.Client.Systems.Local.Modules.ScreenPointer.Api;
    using EventHorizon.Game.Client.Systems.Local.Modules.ScreenPointer.Model;
    using EventHorizon.Game.Client.Systems.Player.Api;
    using EventHorizon.Game.Client.Systems.Player.Modules.Camera.Api;
    using EventHorizon.Game.Client.Systems.Player.Modules.Camera.Model;
    using EventHorizon.Game.Client.Systems.Player.Modules.Input.Api;
    using EventHorizon.Game.Client.Systems.Player.Modules.Input.Model;
    using EventHorizon.Game.Client.Systems.Player.Modules.MoveSelected.Api;
    using EventHorizon.Game.Client.Systems.Player.Modules.MoveSelected.Model;
    using EventHorizon.Game.Client.Systems.Player.Modules.PlayerInteraction.Api;
    using EventHorizon.Game.Client.Systems.Player.Modules.PlayerInteraction.Model;
    using EventHorizon.Game.Client.Systems.Player.Modules.SelectedCompanionTracker.Api;
    using EventHorizon.Game.Client.Systems.Player.Modules.SelectedCompanionTracker.Model;
    using EventHorizon.Game.Client.Systems.Player.Modules.SelectedTracker.Api;
    using EventHorizon.Game.Client.Systems.Player.Modules.SelectedTracker.Model;
    using EventHorizon.Game.Client.Systems.Player.Modules.SkillSelection.Api;
    using EventHorizon.Game.Client.Systems.Player.Modules.SkillSelection.Model;
    using MediatR;

    public class BabylonJSPlayerEntity
        : StandardServerEntity,
        IPlayerEntity
    {
        private readonly IPlayerZoneDetails _player;

        public BabylonJSPlayerEntity(
            IPlayerZoneDetails player
        ) : base(player)
        {
            _player = player;
            Transform.Scaling.Set(1, 1, 1);
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            RegisterModule(
                CameraModule.MODULE_NAME,
                new BabylonJSCameraModule(
                    this
                )
            );
            RegisterModule(
                InputModule.MODULE_NAME,
                new StandardInputModule(
                    this
                )
            );
            RegisterModule(
                ScreenPointerModule.MODULE_NAME,
                new BabylonJSScreenPointerModule(
                    this
                )
            );
            RegisterModule(
                SelectedTrackerModule.MODULE_NAME,
                new StandardSelectedTrackerModule(
                    this
                )
            );
            RegisterModule(
                MoveSelectedModule.MODULE_NAME,
                new StandardMoveSelectedModule(
                    this
                )
            );
            RegisterModule(
                SelectedCompanionTrackerModule.MODULE_NAME,
                new StandardSelectedCompanionTrackerModule(
                    this
                )
            );
            RegisterModule(
                PlayerInteractionModule.MODULE_NAME,
                new StandardPlayerInteractionModule(
                    this
                )
            );

            await _mediator.Send(
                new RegisterAllPlayerModulesOnEntityCommand(
                    this
                )
            );

            // TODO: Put this into a Server Player EntityModule
            RegisterModule(
                SkillSelectionModule.MODULE_NAME,
                new StandardSkillSelectionModule(
                    this
                )
            );
        }
    }
}