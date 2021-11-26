namespace Microsoft.AspNetCore.SignalR.Client;

using System.Diagnostics.CodeAnalysis;

public static class HubConnectionExtensions
{
    public static bool IsNotConnected(
        [NotNullWhen(false)]
                this HubConnection? connection
    ) =>
        connection.IsNull()
        || connection.State
            != HubConnectionState.Connected;
}
