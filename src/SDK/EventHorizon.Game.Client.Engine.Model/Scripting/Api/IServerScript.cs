namespace EventHorizon.Game.Client.Engine.Model.Scripting.Api
{
    using EventHorizon.Game.Client.Engine.Model.Scripting.Data;

    public interface IServerScript
    {
        T Run<T>(
            ScriptData data
        );
    }
}