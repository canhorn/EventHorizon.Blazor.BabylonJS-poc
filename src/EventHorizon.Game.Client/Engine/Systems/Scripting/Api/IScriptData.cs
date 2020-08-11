namespace EventHorizon.Game.Client.Engine.Systems.Scripting.Api
{
    using System;

    public interface IScriptData
    {
        T Get<T>(
            string name
        );
    }
}
