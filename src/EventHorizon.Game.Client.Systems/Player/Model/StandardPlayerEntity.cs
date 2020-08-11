namespace EventHorizon.Game.Client.Systems.Player.Model
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Lifecycle.Model;
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Api;
    using EventHorizon.Game.Client.Systems.Entity.Model;
    using EventHorizon.Game.Client.Systems.Player.Api;
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

            // TODO: CAMERA_MODULE_NAME
            //RegisterModule(
            //    ICameraModule.MODULE_NAME,
            //    new CameraModule(
            //        this
            //    )
            //);
            RegisterModule(
                IInputModule.MODULE_NAME,
                new InputModule(
                    this
                )
            );

            return Task.CompletedTask;
        }
    }
}