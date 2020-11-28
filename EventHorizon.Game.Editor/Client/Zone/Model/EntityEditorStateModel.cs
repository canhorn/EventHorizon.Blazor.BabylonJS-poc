namespace EventHorizon.Game.Editor.Client.Zone.Model
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Editor.Client.Zone.Api;

    public class EntityEditorStateModel
        : EntityEditorState
    {
        public Func<IObjectEntityDetails, Task> OnSave { get; set; }
    }
}
