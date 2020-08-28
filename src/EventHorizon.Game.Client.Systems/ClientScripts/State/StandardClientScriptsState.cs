namespace EventHorizon.Game.Client.Systems.ClientScripts.State
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using EventHorizon.Game.Client.Engine.Model.Scripting.Api;
    using EventHorizon.Game.Client.Systems.ClientScripts.Api;
    using CSScriptLib;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Model.Scripting.Services;
    using EventHorizon.Game.Client.Engine.Model.Scripting.Data;
    using EventHorizon.Game.Client.Scripts.SDK;
    using Microsoft.Extensions.Logging;
    using System.Linq;

    public class StandardClientScriptsState
        : ClientScriptsState
    {
        private static readonly IClientScript EMPTY_SCRIPT = new EmptyScript();
        private static readonly IList<Assembly> ASSEMBLIES = GameClientSDKRoot.ASSEMBLIES;

        public string Hash { get; private set; } = string.Empty;
        private Assembly? _scriptAssembly;
        private ILogger _logger;
        private readonly IDictionary<string, IClientScript> _scripts = new Dictionary<string, IClientScript>();

        public StandardClientScriptsState(
            ILogger<StandardClientScriptsState> logger
        )
        {
            _logger = logger;
        }

        public void SetScriptAssembly(
            string hash,
            Assembly scriptAssembly
        )
        {
            Hash = hash;
            _scriptAssembly = scriptAssembly;
        }

        public IClientScript GetScript(
            string id
        )
        {
            if (_scripts.TryGetValue(
                id,
                out var script
            ))
            {
                return script;
            }
            if (_scriptAssembly == null)
            {
                return EMPTY_SCRIPT;
            }

            // Get script from Assembly
            try
            {
                var assemblyScript = _scriptAssembly.CreateObject(
                    $"css_root+{id}"
                ) as IClientScript;
                if (assemblyScript != null)
                {
                    _scripts.Add(
                        id,
                        assemblyScript
                    );
                    return assemblyScript;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Failed to Create Script"
                );
                var scriptNames = string.Join(
                    ",",
                    _scriptAssembly.GetTypes()
                        .Select(
                            a => a.FullName?.Replace(
                                "css_root+", 
                                string.Empty
                            ) ?? string.Empty
                        )
                ); 
                _logger.LogInformation(
                    "Currently Supported Script Names: \n\r {AssemblyNames}",
                    scriptNames
                );
            }
            return EMPTY_SCRIPT;
        }

        private class EmptyScript
            : IClientScript
        {
            public string Id => "__EMPTY_SCRIPT__";

            public Task Run(
                ScriptServices services,
                ScriptData data
            )
            {
                return Task.CompletedTask;
            }
        }
    }
}
