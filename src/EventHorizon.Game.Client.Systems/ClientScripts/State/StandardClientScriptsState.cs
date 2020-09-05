﻿namespace EventHorizon.Game.Client.Systems.ClientScripts.State
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using EventHorizon.Game.Client.Engine.Scripting.Api;
    using EventHorizon.Game.Client.Systems.ClientScripts.Api;
    using CSScriptLib;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Scripting.Services;
    using EventHorizon.Game.Client.Engine.Scripting.Data;
    using EventHorizon.Game.Client.Scripts.SDK;
    using Microsoft.Extensions.Logging;
    using System.Linq;

    public class StandardClientScriptsState
        : ClientScriptsState
    {
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

        public Option<IClientScript> GetScript(
            string id
        )
        {
            id = NormailzeIdForAssemblyScriptLookup(
                id
            );
            if (_scripts.TryGetValue(
                id,
                out var script
            ))
            {
                return script.ToOption();
            }
            if (_scriptAssembly == null)
            {
                return new Option<IClientScript>(
                    null
                );
            }

            // Get script from Assembly
            try
            {
                if (_scriptAssembly.CreateObject(
                    $"css_root+{id}"
                ) is IClientScript assemblyScript)
                {
                    _scripts.Add(
                        id,
                        assemblyScript
                    );
                    return assemblyScript.ToOption();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Failed to Create Script: {Id}",
                    id
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
            return new Option<IClientScript>(
                null
            );
        }

        /// <summary>
        /// This "fixes" the client script id so it can be looked up in the Assembly.
        /// TODO: [TS_DEPRECATED] - Remove after old ts client is deprecated.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string NormailzeIdForAssemblyScriptLookup(
            string id
        )
        {
            return id.Replace(
                ".js",
                string.Empty
            );
        }
    }
}