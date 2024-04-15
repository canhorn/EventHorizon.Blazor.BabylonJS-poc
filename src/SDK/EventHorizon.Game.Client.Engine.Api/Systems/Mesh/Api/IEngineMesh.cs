namespace EventHorizon.Game.Client.Engine.Systems.Mesh.Api;

using System.Collections.Generic;
using EventHorizon.Game.Client.Engine.Entity.Api;
using EventHorizon.Game.Client.Engine.Systems.Mesh.Model;

public interface IEngineMesh
{
    IVector3 Position { get; }
    IVector3 Rotation { get; }
    IVector3 Scaling { get; }
    decimal ScalingDeterminant { get; set; }

    MeshSystemType SystemType { get; set; }
    long OwnerEntityId { get; set; }
    IDictionary<string, object> MetaData { get; }

    // TODO: Handle AnimationGroup as an extension method?? or Add to MetaData
    //AnimationList?: AnimationGroup[];

    void SetEnabled(bool enabled);
    void SetVisible(bool visible);
    void Dispose();
    IEngineMesh Clone(string identifier);
    IVector3 GetDirection(IVector3 localAxis);
}
