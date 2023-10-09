namespace EventHorizon.Game.Editor.Client.Zone.Api;

using System;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Systems.Entity.Api;

public interface EntityEditorState
{
    public Func<IObjectEntityDetails, Task> OnSave { get; }

    public bool ShowDelete { get; }
    public Func<IObjectEntityDetails, Task> OnDelete { get; }
}
