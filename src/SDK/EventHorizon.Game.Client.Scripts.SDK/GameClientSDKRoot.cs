namespace EventHorizon.Game.Client.Scripts.SDK
{
    using System.Collections.Generic;
    using System.Reflection;

    public static class GameClientSDKRoot
    { 
        public static IList<Assembly> ASSEMBLIES = new List<Assembly>
        {
            // Client SDK
            typeof(CoreSdkRoot).Assembly,
            typeof(EngineSdkRoot).Assembly,
            typeof(EngineGuiSdkRoot).Assembly,
            typeof(SystemsSdkRoot).Assembly,

            // Server SDK
            typeof(ServerModuleSdkRoot).Assembly,
        };
    }
}
