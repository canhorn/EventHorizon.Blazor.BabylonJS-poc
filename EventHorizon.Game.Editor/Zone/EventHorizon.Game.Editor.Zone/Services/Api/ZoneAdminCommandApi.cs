namespace EventHorizon.Game.Editor.Zone.Services.Api;

using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;

public interface ZoneAdminCommandApi
{
    Task<StandardCommandResult> Send(string command, object data);
}
