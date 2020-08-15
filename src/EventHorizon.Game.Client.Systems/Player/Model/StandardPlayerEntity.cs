namespace EventHorizon.Game.Client.Systems.Player.Model
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Lifecycle.Model;
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Api;
    using EventHorizon.Game.Client.Systems.Entity.Model;
    using EventHorizon.Game.Client.Systems.Local.Modules.ScreenPointer.Api;
    using EventHorizon.Game.Client.Systems.Local.Modules.ScreenPointer.Model;
    using EventHorizon.Game.Client.Systems.Player.Api;
    using EventHorizon.Game.Client.Systems.Player.Modules.Camera.Api;
    using EventHorizon.Game.Client.Systems.Player.Modules.Camera.Model;
    using EventHorizon.Game.Client.Systems.Player.Modules.Input.Api;
    using EventHorizon.Game.Client.Systems.Player.Modules.Input.Model;

    public class StandardPlayerEntity
        : StandardServerEntity,
        IPlayerEntity
    {
        private IPlayerZoneDetails _player;

        public StandardPlayerEntity(
            IPlayerZoneDetails player
        ) : base(player)
        {
            _player = player;
            Transform.Scaling.Set(1, 1, 1);
        }

        public override Task Initialize()
        {
            base.Initialize();

            RegisterModule(
                ICameraModule.MODULE_NAME,
                new BabylonJSCameraModule(
                    this
                )
            );
            RegisterModule(
                IInputModule.MODULE_NAME,
                new InputModule(
                    this
                )
            );
            RegisterModule(
                IScreenPointerModule.MODULE_NAME,
                new BabylonJSScreenPointerModule(
                    this
                )
            );
            // TODO: SELECTED_TRACKER_MODULE_NAME
            //RegisterModule(
            //    ISelectedTrackerModule.MODULE_NAME,
            //    new SelectedTrackerModule(
            //        this
            //    )
            //);
            // TODO: MOVE_SELECTED_MODULE_NAME
            //RegisterModule(
            //    IMoveSelectedModule.MODULE_NAME,
            //    new MoveSelectedModule(
            //        this
            //    )
            //);
            // TODO: SELECTED_COMPANION_TRACKER_MODULE_NAME
            //RegisterModule(
            //    IMoveSelectedModule.MODULE_NAME,
            //    new MoveSelectedModule(
            //        this
            //    )
            //);
            // TODO: PLAYER_INTERACTION_MODULE_NAME
            //RegisterModule(
            //    IMoveSelectedModule.MODULE_NAME,
            //    new MoveSelectedModule(
            //        this
            //    )
            //);

            // TODO: Register Player Modules
            //this._commandService.send(
            //    createRegisterAllPlayerModulesCommand({
            //        entity: this,
            //    })
            //);

            return Task.CompletedTask;
        }
    }
}