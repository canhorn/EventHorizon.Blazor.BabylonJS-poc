namespace EventHorizon.Game.Editor.Client.PlayerEditor.Components.Property;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Systems.Entity.Properties.Model.Api;
using EventHorizon.Game.Client.Systems.Entity.Properties.Model.Model;
using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Shared.Components.Select;
using EventHorizon.Game.Editor.Client.Shared.Properties;
using EventHorizon.Game.Editor.Client.Zone.Api;
using Microsoft.AspNetCore.Components;

public class PlayerEditorModelStateComponentBase : EditorComponentBase
{
    [CascadingParameter]
    public ZoneState ZoneState { get; set; } = null!;

    [Parameter]
    public required object Data { get; set; }

    [Parameter]
    public required EventCallback<PropertyChangeArgs> OnDataChange { get; set; }

    private object? _editingData;
    protected EditableModelState ModelState { get; private set; } = new();

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        var movementState = Data.To<ModelStateModel>();
        Console.WriteLine("Scaling Determinant: " + movementState?.ScalingDeterminant);
        if (Data == _editingData)
        {
            return;
        }
        else if (movementState is not null)
        {
            ModelState.SetFrom(movementState);
            _editingData = Data;
        }
    }

    protected void HandleAssetIdChanged(PropertyChangedArgs args)
    {
        ModelState.Mesh.AssetId = args.Property?.ToString() ?? "DEFAULT_MESH";
    }

    protected async Task HandleSave()
    {
        await OnDataChange.InvokeAsync(
            new PropertyChangeArgs(IModelState.NAME, ModelState.ToState())
        );
    }

    public record EditableModelState() : IModelState
    {
        public decimal? ScalingDeterminant { get; set; }
        public EditableModelMesh Mesh { get; set; } = new EditableModelMesh();
        IModelMesh IModelState.Mesh => Mesh;

        public void SetFrom(IModelState model)
        {
            ScalingDeterminant = model.ScalingDeterminant;
            Mesh = new EditableModelMesh();
            Mesh.SetFrom(model.Mesh);
        }

        public IModelState ToState()
        {
            return new ModelStateModel
            {
                ScalingDeterminant = ScalingDeterminant,
                Mesh = Mesh.ToState(),
            };
        }
    }

    public class EditableModelMesh : IModelMesh
    {
        public string AssetId { get; set; } = string.Empty;

        public void SetFrom(IModelMesh model)
        {
            AssetId = model.AssetId;
        }

        public StandardModelMesh ToState()
        {
            return new StandardModelMesh { AssetId = AssetId, };
        }
    }
}
