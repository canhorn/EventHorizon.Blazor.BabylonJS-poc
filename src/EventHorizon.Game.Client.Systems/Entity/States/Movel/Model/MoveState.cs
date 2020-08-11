namespace EventHorizon.Game.Client.Systems.Entity.States.Move.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Core.Api;
    using EventHorizon.Game.Client.Engine.Debugging.Model;
    using EventHorizon.Game.Client.Engine.Entity.Api;
    using EventHorizon.Game.Client.Engine.Entity.Model;
    using EventHorizon.Game.Client.Engine.Rendering.Api;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Client.Systems.Entity.Modules.Move.Api;
    using EventHorizon.Game.Client.Systems.Entity.Properties.Move.Api;
    using EventHorizon.Game.Client.Systems.Height.Api;
    using EventHorizon.Game.Client.Systems.Local.Modules.State.Api;
    using EventHorizon.Game.Client.Systems.Local.Modules.Transform.Api;
    using EventHorizon.Game.Client.Systems.Local.Transform.Set;
    using MediatR;

    // TODO: Create StateBase on new IState
    public class MoveState
        : ClientEntityBase,
        IState
    {
        private static decimal DEFAULT_MOVE_SPEED = 100;

        private readonly IRenderingTime _renderingTime;
        private readonly IHeightResolver _heightResolver;
        private readonly IObjectEntity _entity;
        private readonly decimal _distanceEpsilon;
        private readonly IMovementState _movementState;

        private IList<IVector3> _path;

        public bool Remove { get; private set; }
        public string Name { get; }

        public MoveState(
            IObjectEntity entity,
            string name,
            // Epsilon
            decimal distanceEpsilon,
            IVector3[] path,
            IMovementState movementState
        ) : base(GameServiceProvider.GetService<IIndexPool>().NextIndex())
        {
            _renderingTime = GameServiceProvider.GetService<IRenderingTime>();
            _heightResolver = GameServiceProvider.GetService<IHeightResolver>();
            _entity = entity;
            Name = name;
            _distanceEpsilon = distanceEpsilon;
            _path = path.ToList();
            _movementState = movementState;
        }

        public Task Reset()
        {
            Remove = false;
            _path = new IVector3[0];

            return Task.CompletedTask;
        }

        public async Task Update()
        {
            if (_path.Count == 0)
            {
                Remove = true;
                return;
            }
            Console.WriteLine($"Starting Move");
            await DebuggingLogger.StartClientLogging();
            var deltaTime = _renderingTime.DeltaTime;
            var currentMoveTo = this._path[0];
            _entity.GetModule<IMoveModule>(
                IMoveModule.MODULE_NAME
            ).SetCurrentMoveTo(
                currentMoveTo
            );
            var currentPosition = _entity.Transform.Position;
            var toDestination = currentMoveTo.Subtract(currentPosition);
            var distanceToDestination = toDestination.Length();

            //Console.WriteLine($"currentMoveTo: {currentMoveTo.X}, {currentMoveTo.Y}, {currentMoveTo.Z}");
            if (distanceToDestination >= _distanceEpsilon)
            {
                var direction = toDestination.Normalize();
                Move(
                    direction,
                    deltaTime
                );
                // TODO: Rotation
                //Rotate(
                //    toDestination
                //);
            }
            else
            {
                _path.Remove(currentMoveTo);
            }
            Console.WriteLine($"Finished Move");
            return;
        }

        private void Move(
            IVector3 direction,
            long deltaTime
        )
        {
            var moveMultiply = deltaTime / (DEFAULT_MOVE_SPEED * (1m / _movementState.Speed));
            var velocity = direction.Multiply(
                new StandardVector3(
                    moveMultiply,
                    0,
                    moveMultiply
                )
            );
            var position = _entity.Transform.Position;
            position.AddInPlace(
                velocity
            );
            Console.WriteLine($"position: {position.X}, {position.Y}, {position.Z}");
            _entity.GetModule<ITransformModule>(
                ITransformModule.MODULE_NAME
            ).Reset(
                position
            );
            //position.Set(
            //    position.X,
            //    _heightResolver.FindHeight(
            //        position.X,
            //        position.Z
            //    ),
            //    position.Z
            //);
        }
    }
}
