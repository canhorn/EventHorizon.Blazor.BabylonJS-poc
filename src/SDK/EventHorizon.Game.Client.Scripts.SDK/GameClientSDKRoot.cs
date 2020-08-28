namespace EventHorizon.Game.Client.Scripts.SDK
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using EventHorizon.Game.Client.Core.Model;
    using EventHorizon.Game.Client.Engine.Events.Testing;
    using EventHorizon.Game.Client.Engine.Model.Scripting.Data;

    public static class GameClientSDKRoot
    { 

        public static IList<Assembly> ASSEMBLIES = new List<Assembly>
        {
            typeof(CoreModelClass).Assembly,
            typeof(ScriptData).Assembly,
            typeof(ScriptTestingEvent).Assembly,
        };
    }
}
