namespace EventHorizon.Game.Client.Systems.Local.Modules.Transform.Model
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Entity.Api;
    using EventHorizon.Game.Client.Engine.Entity.Model;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Client.Engine.Systems.Module.Model;
    using EventHorizon.Game.Client.Systems.Entity.Changed;
    using EventHorizon.Game.Client.Systems.Height.Api;
    using EventHorizon.Game.Client.Systems.Local.Modules.MeshManagement.Api;
    using EventHorizon.Game.Client.Systems.Local.Modules.Transform.Api;
    using EventHorizon.Game.Client.Systems.Local.Transform.Set;
    using EventHorizon.Game.Client.Systems.Map.Ready;

    public class TransformModule
        : ModuleEntityBase,
        ITransformModule,
        SetEntityPositionEventObserver,
        MapMeshReadyEventObserver,
        EntityChangedSuccessfullyEventObserver
    {
        private readonly IObjectEntity _entity;
        private readonly IHeightResolver _heightResolver = GameServiceProvider.GetService<IHeightResolver>();

        public override int Priority => 0;

        public TransformModule(
            IObjectEntity entity
        )
        {
            _entity = entity;

            GamePlatfromServices.RegisterObserver(this);

            Reset(
                entity.Transform.Position
            );
        }

        public void Reset(
            IVector3 position
        )
        {
            SetAllPosition(
                position
            );
        }

        public override Task Initialize()
        {
            return Task.CompletedTask;
        }

        public override Task Dispose()
        {
            GamePlatfromServices.UnRegisterObserver(this);
            return Task.CompletedTask;
        }

        public override Task Update()
        {
            return Task.CompletedTask;
        }

        public Task Handle(
            SetEntityPositionEvent args
        )
        {
            if (_entity.ClientId != args.ClientId)
            {
                return Task.CompletedTask;
            }
            Reset(
                args.Position
            );
            return Task.CompletedTask;
        }

        public Task Handle(
            MapMeshReadyEvent args
        )
        {
            Reset(
                _entity.Transform.Position
            );
            return Task.CompletedTask;
        }

        public Task Handle(
            EntityChangedSuccessfullyEvent args
        )
        {
            if (_entity.EntityId != args.EntityId)
            {
                return Task.CompletedTask;
            }
            var meshModule = _entity.GetModule<IMeshModule>(
                IMeshModule.MODULE_NAME
            );
            if (meshModule != null)
            {
                this.Reset(
                    meshModule.Mesh.Position
                );
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Take in the position and assign it to Entity and any modules that need position.
        /// </summary>
        /// <param name="position"></param>
        private void SetAllPosition(
            IVector3 position
        )
        {
            var newPosition = new StandardVector3(
                position
            );
            var yPosition = newPosition.Y;
            if (_entity.GetProperty<bool>("resolveHeight"))
            {
                yPosition = _heightResolver.FindHeight(
                    newPosition.X,
                    newPosition.Z
                );
            }

            var heightOffset = _entity.GetProperty<decimal?>("heightOffset");
            if (heightOffset.HasValue)
            {
                yPosition += heightOffset.Value;
            }

            // Set Y on newPosition
            newPosition.Set(
                newPosition.X,
                yPosition,
                newPosition.Z
            );

            // Set Mesh to Position
            var meshModule = _entity.GetModule<IMeshModule>(
                IMeshModule.MODULE_NAME
            );
            if (meshModule != null)
            {
                meshModule.Mesh.Position.Set(
                    newPosition
                );
            }

            // Set Entity to Position
            _entity.Transform.Position.Set(
                newPosition
            );
        }

        public void SetRotation(
            IVector3 rotation
        )
        {
            _entity.Transform.Rotation.Set(
                rotation
            );
            // Set Mesh to Rotation
            var meshModule = _entity.GetModule<IMeshModule>(
                IMeshModule.MODULE_NAME
            );
            if (meshModule != null)
            {
                meshModule.Mesh.Rotation.Set(
                    rotation
                );
            }
        }
    }
}
