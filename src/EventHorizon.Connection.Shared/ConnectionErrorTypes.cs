namespace EventHorizon.Connection.Shared
{
    public static class ConnectionErrorTypes
    {
        public static string None => nameof(None);
        public static string Unknown => nameof(Unknown);
        public static string Unauthorized => nameof(Unauthorized);
        public static string Operational => nameof(Operational);
        public static string NotInitialized => nameof(NotInitialized);
        public static string NotConnected => nameof(NotConnected);
    }
}
