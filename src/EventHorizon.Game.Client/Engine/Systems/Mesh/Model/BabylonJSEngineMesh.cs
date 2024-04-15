namespace EventHorizon.Game.Client.Engine.Systems.Mesh.Model;

using System.Collections.Generic;
using BabylonJS;
using EventHorizon.Blazor.Interop;
using EventHorizon.Game.Client.Engine.Entity.Api;
using EventHorizon.Game.Client.Engine.Entity.Model;
using EventHorizon.Game.Client.Engine.Systems.Mesh.Api;

public class BabylonJSEngineMesh : IEngineMesh
{
    public static readonly string OWNER_ENTITY_ID_NAME = "ownerEntityId";

    private long _ownerEntityId;

    public Mesh Mesh { get; }

    public IVector3 Position { get; }
    public IVector3 Rotation { get; }
    public IVector3 Scaling { get; }
    public decimal ScalingDeterminant
    {
        get { return Mesh.scalingDeterminant; }
        set { Mesh.scalingDeterminant = value; }
    }
    public MeshSystemType SystemType { get; set; }
    public long OwnerEntityId
    {
        get => _ownerEntityId;
        set { SetOwnerEntityId(value); }
    }
    public IDictionary<string, object> MetaData { get; }

    public BabylonJSEngineMesh(Mesh mesh, MeshSystemType meshSystemType, long ownerEntityId)
    {
        Mesh = mesh;
        Position = new BabylonJSVector3(Mesh.position);
        Rotation = new BabylonJSVector3(Mesh.rotation);
        Scaling = new BabylonJSVector3(Mesh.scaling);
        SystemType = meshSystemType;
        MetaData = new Dictionary<string, object>();

        SetOwnerEntityId(ownerEntityId);
    }

    public BabylonJSEngineMesh(Mesh mesh)
        : this(mesh, MeshSystemType.NONE, -1) { }

    public void Dispose() => Mesh.dispose();

    public void SetEnabled(bool value)
    {
        Mesh.setEnabled(value);
        var children = Mesh.getChildMeshes();
        foreach (var child in children)
        {
            child.setEnabled(value);
        }
    }

    public void SetVisible(bool visible) => Mesh.isVisible = visible;

    public IEngineMesh Clone(string identifier) => new BabylonJSEngineMesh(Mesh.clone(identifier));

    public IVector3 GetDirection(IVector3 localAxis)
    {
        return Mesh.getDirection(localAxis.ToBabylonJS()).ToStandardVector3();
    }

    private void SetOwnerEntityId(long entityId)
    {
        EventHorizonBlazorInterop.Set(Mesh.___guid, OWNER_ENTITY_ID_NAME, entityId);
        _ownerEntityId = entityId;
    }
}
