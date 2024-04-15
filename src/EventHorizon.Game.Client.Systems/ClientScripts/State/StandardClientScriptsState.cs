namespace EventHorizon.Game.Client.Systems.ClientScripts.State;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EventHorizon.Game.Client.Engine.Scripting.Api;
using EventHorizon.Game.Client.Scripts.SDK;
using EventHorizon.Game.Client.Systems.ClientScripts.Api;
using Microsoft.Extensions.Logging;

public class StandardClientScriptsState : ClientScriptsState
{
    private static readonly IList<Assembly> ASSEMBLIES = GameClientSDKRoot.ASSEMBLIES;

    private readonly ILogger _logger;
    private readonly IDictionary<string, IClientScript> _scripts =
        new Dictionary<string, IClientScript>();
    private readonly IDictionary<string, IStartupClientScript> _startupScripts =
        new Dictionary<string, IStartupClientScript>();

    private Assembly? _scriptAssembly;

    public string Hash { get; private set; } = string.Empty;
    public IEnumerable<IStartupClientScript> StartupScripts => _startupScripts.Values;

    public StandardClientScriptsState(ILogger<StandardClientScriptsState> logger)
    {
        _logger = logger;
    }

    public void Reset()
    {
        _scriptAssembly = null;
        Hash = string.Empty;
        _scripts.Clear();
        _startupScripts.Clear();
    }

    public void SetScriptAssembly(string hash, Assembly scriptAssembly)
    {
        Hash = hash;
        _scriptAssembly = scriptAssembly;
        LogSupportedScripts();
        _scripts.Clear();

        FillStartupScripts();
    }

    public Option<IClientScript> GetScript(string id)
    {
        id = NormailzeIdForAssemblyScriptLookup(id);
        if (_scripts.TryGetValue(id, out var script))
        {
            return script.ToOption();
        }
        if (_scriptAssembly == null)
        {
            return new Option<IClientScript>(null);
        }

        // Get script from Assembly
        try
        {
            if (_scriptAssembly.CreateInstance($"css_root+{id}") is IClientScript assemblyScript)
            {
                _scripts.Add(id, assemblyScript);
                return assemblyScript.ToOption();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to Create Script: {Id}", id);
            LogSupportedScripts();
        }
        return new Option<IClientScript>(null);
    }

    private void FillStartupScripts()
    {
        _startupScripts.Clear();
        if (_scriptAssembly == null)
        {
            return;
        }

        var scriptTypes = _scriptAssembly
            .GetTypes()
            .Where(a => a.IsAssignableTo(typeof(IStartupClientScript)));
        foreach (var scriptType in scriptTypes)
        {
            var script = Activator.CreateInstance(scriptType);
            if (script is IStartupClientScript startupScript)
            {
                _startupScripts.Add(startupScript.Id, startupScript);
            }
        }
        _logger.LogInformation(
            "Currently Supported Startup Scripts: \n\r\t{StartupScriptNames}",
            string.Join("\n\r\t", _startupScripts.Keys)
        );
    }

    private void LogSupportedScripts()
    {
        if (_scriptAssembly.IsNull())
        {
            return;
        }
        var scriptNames = string.Join(
            $"{Environment.NewLine}\t",
            _scriptAssembly
                .GetTypes()
                .Where(a => a.FullName?.StartsWith("css_root+") ?? false)
                .Select(a => a.FullName?.Replace("css_root+", string.Empty) ?? string.Empty)
        );
        _logger.LogInformation(
            "Currently Supported Script Names: \n\r\t{ScriptNames}",
            scriptNames
        );
    }

    /// <summary>
    /// This "fixes" the client script id so it can be looked up in the Assembly.
    /// TODO: [TS_DEPRECATED] - Remove after old ts client is deprecated.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private string NormailzeIdForAssemblyScriptLookup(string id)
    {
        return id.Replace(".js", string.Empty);
    }
}
