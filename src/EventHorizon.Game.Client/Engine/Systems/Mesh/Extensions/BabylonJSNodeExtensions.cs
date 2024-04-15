namespace BabylonJS;

using System;
using EventHorizon.Blazor.Interop;
using EventHorizon.Game.Client.Engine.Systems.Mesh.Model;

public static class BabylonJSNodeExtensions
{
    /// <summary>
    // Find the <see cref="EventHorizon.Game.Client.Engine.Systems.Mesh.Model.BabylonJSEngineMesh.OWNER_ENTITY_ID_NAME" />, checks parents till null or is found.
    /// </summary>
    /// <param name="node">Node to find the <see cref="EventHorizon.Game.Client.Engine.Systems.Mesh.Model.BabylonJSEngineMesh.OWNER_ENTITY_ID_NAME" /> on.</param>
    /// <returns>The id of the owner entity.</returns>
    public static long? GetOwnerEntityId(this Node node)
    {
        var ownerEntityId = EventHorizonBlazorInterop.Get<long?>(
            node.___guid,
            BabylonJSEngineMesh.OWNER_ENTITY_ID_NAME
        );
        if (ownerEntityId.HasValue)
        {
            return ownerEntityId;
        }
        else if (node.parent.___guid != null)
        {
            return node.parent.GetOwnerEntityId();
        }
        return null;
    }
}
