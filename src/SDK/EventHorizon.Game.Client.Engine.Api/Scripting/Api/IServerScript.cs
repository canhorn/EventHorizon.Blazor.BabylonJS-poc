namespace EventHorizon.Game.Client.Engine.Scripting.Api;

using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Scripting.Data;
using EventHorizon.Game.Client.Engine.Scripting.Services;

public interface IServerScript
{
    Task Run(ScriptServices services, ScriptData data);
}
