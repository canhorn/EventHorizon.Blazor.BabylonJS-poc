namespace Microsoft.AspNetCore.SignalR.Client
{
    public static class HubConnectionExtensions
    {
        public static bool IsNotConnected(
            this HubConnection? connection
        ) => connection.IsNull()
            || connection.State != HubConnectionState.Connected;
    }
}
