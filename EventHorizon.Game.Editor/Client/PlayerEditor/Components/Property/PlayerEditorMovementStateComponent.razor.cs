namespace EventHorizon.Game.Editor.Client.PlayerEditor.Components.Property;

using System;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Systems.Entity.Properties.Move.Api;
using EventHorizon.Game.Client.Systems.Entity.Properties.Move.Model;
using EventHorizon.Game.Editor.Client.Shared.Components;
using Microsoft.AspNetCore.Components;

public class PlayerEditorMovementStateComponentBase : EditorComponentBase
{
    [Parameter]
    public required object Data { get; set; }

    [Parameter]
    public required EventCallback<PropertyChangeArgs> OnDataChange { get; set; }

    private object? _editingData;
    protected EditableMovementStateModel MovementState { get; private set; } =
        new EditableMovementStateModel();

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        var movementState = Data.To<MovementStateModel>();
        if (Data == _editingData)
        {
            return;
        }
        else if (movementState is not null)
        {
            MovementState.SetFrom(movementState);
            _editingData = Data;
        }
    }

    protected async Task HandleSave()
    {
        Console.WriteLine("Saving MovementState");
        await OnDataChange.InvokeAsync(
            new PropertyChangeArgs(IMovementState.NAME, MovementState.ToState())
        );
    }

    public record EditableMovementStateModel() : IMovementState
    {
        public decimal Speed { get; set; }

        public void SetFrom(IMovementState model)
        {
            Speed = model.Speed;
        }

        public IMovementState ToState()
        {
            return new MovementStateModel { Speed = Speed, };
        }
    }
}
