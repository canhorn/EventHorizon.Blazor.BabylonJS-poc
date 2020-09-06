namespace EventHorizon.Game.Client.Engine.Entity.Tag
{
    public static class TagBuilder
    {
        public static string UNDEFINED = "UNDEFINED";

        public static string CreateNameTag(
            string identifier
        ) => CreateTag("name", identifier);

        public static string CreateIdTag(
            string identifier
        ) => CreateTag("entityId", identifier);

        public static string CreateEntityIdTag(
            string identifier
        ) => CreateTag("entityId", identifier);

        public static string CreateGlobalIdTag(
            string identifier
        ) => CreateTag("globalId", identifier);

        public static string CreateTypeTag(
            string identifier
        ) => CreateTag("type", identifier);

        public static string CreateTag(
            string name,
            string identifier
        ) => $"{name}:{identifier}";
    }
}
