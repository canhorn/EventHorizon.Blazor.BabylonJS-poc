namespace EventHorizon.Game.Editor.Zone
{
    using System;


    public static class ZoneAdminConnectionCodes
    {
        public static string HTTP_REQUEST_FAILURE => nameof(HTTP_REQUEST_FAILURE);
        public static string INVALID_OPERATION_FAILURE => nameof(INVALID_OPERATION_FAILURE);
        public static string GENERAL_FAILURE => nameof(GENERAL_FAILURE);
        public static string CLOSED_BY_CONNECTION => nameof(CLOSED_BY_CONNECTION);
        public static string EXPLICT_CLOSED => nameof(EXPLICT_CLOSED);
    }
}
