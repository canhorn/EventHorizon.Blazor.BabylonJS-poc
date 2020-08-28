namespace EventHorizon.Game.Client.Engine.Model.Scripting.Api
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Model.Scripting.Data;
    using EventHorizon.Game.Client.Engine.Model.Scripting.Services;

    public interface IServerScript
    {
        Task Run(
            ScriptServices services,
            ScriptData data
        );
    }
}