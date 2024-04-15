namespace EventHorizon.Game.Client.Systems.Entity.States.Move.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Exceptions;
using EventHorizon.Game.Client.Engine.Core.Api;
using EventHorizon.Game.Client.Engine.Entity.Api;
using EventHorizon.Game.Client.Engine.Entity.Model;
using EventHorizon.Game.Client.Engine.Entity.Vector3Math;
using EventHorizon.Game.Client.Engine.Rendering.Api;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
using EventHorizon.Game.Client.Systems.Entity.Modules.Move.Api;
using EventHorizon.Game.Client.Systems.Entity.Properties.Move.Api;
using EventHorizon.Game.Client.Systems.Entity.Stopping;
using EventHorizon.Game.Client.Systems.Height.Api;
using EventHorizon.Game.Client.Systems.Local.Modules.MeshManagement.Api;
using EventHorizon.Game.Client.Systems.Local.Modules.State.Api;
using EventHorizon.Game.Client.Systems.Local.Modules.Transform.Api;
using MediatR;

// TODO: Create StateBase on new IState
public class MoveState : ClientEntityBase, IState
{
    private static decimal DEFAULT_MOVE_SPEED => 105;
    private static decimal DEFAULT_ROTATION_SPEED => 0.05m;

    private readonly IMediator _mediator;
    private readonly IRenderingTime _renderingTime;
    private readonly IHeightResolver _heightResolver;
    private readonly IObjectEntity _entity;
    private readonly decimal _distanceEpsilon;
    private readonly IMovementState _movementState;
    private readonly IMoveModule _moveModule;
    private readonly ITransformModule _transformModule;
    private readonly IMeshModule _meshModule;
    private IList<IVector3> _path;

    public bool Remove { get; private set; }
    public string Name { get; }

    public MoveState(IObjectEntity entity, string name, decimal distanceEpsilon, IVector3[] path)
        : base(GameServiceProvider.GetService<IIndexPool>().NextIndex())
    {
        _mediator = GameServiceProvider.GetService<IMediator>();
        _renderingTime = GameServiceProvider.GetService<IRenderingTime>();
        _heightResolver = GameServiceProvider.GetService<IHeightResolver>();
        _entity = entity;
        Name = name;
        _distanceEpsilon = distanceEpsilon;
        _path = path.ToList();

        _movementState =
            _entity.GetProperty<IMovementState>(IMovementState.NAME)
            ?? throw new GameException(
                "move_state_requires_movement_state_property",
                $"{nameof(MoveState)} Requires {nameof(IMovementState)} Property to Function."
            );
        _moveModule =
            _entity.GetModule<IMoveModule>(IMoveModule.MODULE_NAME)
            ?? throw new GameException(
                "move_state_requires_move_module",
                $"{nameof(MoveState)} Requires {nameof(IMoveModule)} Module to Function."
            );
        _transformModule =
            _entity.GetModule<ITransformModule>(ITransformModule.MODULE_NAME)
            ?? throw new GameException(
                "move_state_requires_transform_module",
                $"{nameof(MoveState)} Requires {nameof(ITransformModule)} Module to Function."
            );
        _meshModule =
            _entity.GetModule<IMeshModule>(IMeshModule.MODULE_NAME)
            ?? throw new GameException(
                "move_state_requires_Mesh_module",
                $"{nameof(MoveState)} Requires {nameof(IMeshModule)} Module to Function."
            );
    }

    public Task Reset()
    {
        Remove = false;
        _path = Array.Empty<IVector3>();

        return Task.CompletedTask;
    }

    public async Task Update()
    {
        if (_path.Count == 0)
        {
            Remove = true;
            return;
        }
        var deltaTime = _renderingTime.DeltaTime;
        var currentMoveTo = this._path[0];
        _moveModule.SetCurrentMoveTo(currentMoveTo);
        var currentPosition = _entity.Transform.Position;
        var toDestination = currentMoveTo.Subtract(currentPosition);
        var distanceToDestination = toDestination.Length();

        if (distanceToDestination >= _distanceEpsilon)
        {
            var direction = toDestination.Normalize();
            Move(direction, deltaTime);
            Rotate(toDestination);
        }
        else
        {
            _path.Remove(currentMoveTo);
            await _mediator.Publish(new EntityStoppingEvent(_entity.ClientId));
        }
        return;
    }

    private void Move(IVector3 direction, long deltaTime)
    {
        var moveMultiply = deltaTime / (DEFAULT_MOVE_SPEED * (1m / _movementState.Speed));
        var velocity = direction.Multiply(new StandardVector3(moveMultiply, 0, moveMultiply));
        var position = _entity.Transform.Position;
        position.AddInPlace(velocity);

        _transformModule.Reset(position);
    }

    private void Rotate(IVector3 direction)
    {
        var mesh = _meshModule.Mesh;
        var targetDirection = direction;
        if (targetDirection.LengthSquared() > 0.0)
        {
            targetDirection = targetDirection.Normalize();
        }
        var facingDirection = mesh.GetDirection(StandardVector3.FORWARD_DIRECTION);
        if (facingDirection.LengthSquared() > 0.0)
        {
            facingDirection = facingDirection.Normalize();
        }
        var strafeDirection = mesh.GetDirection(StandardVector3.RIGHT_DIRECTION);
        if (strafeDirection.LengthSquared() > 0.0)
        {
            strafeDirection = strafeDirection.Normalize();
        }

        var faceTargetDot = Vector3Math.Dot(facingDirection, targetDirection);
        var sideToRotate = Vector3Math.Dot(strafeDirection, targetDirection);

        var angle = -Math.Acos(faceTargetDot);

        if (sideToRotate > 0)
        {
            angle = -angle;
        }
        var rotation = _entity.Transform.Rotation;
        rotation.AddInPlace(new StandardVector3(0, (decimal)angle * DEFAULT_ROTATION_SPEED, 0));
        _transformModule.SetRotation(rotation);
    }
}
