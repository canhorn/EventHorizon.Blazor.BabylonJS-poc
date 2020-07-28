namespace EventHorizon.Game.Client.Engine.Entity.Tag
{
    public struct TagBuilder
    {
        public string Tag { get; }

        public TagBuilder(
            string tag
        )
        {
            Tag = tag;
        }
    }

    public static class TagBuilderExtensions
    {
        public static string CreateNameTag(
            this TagBuilder tagBuilder
        ) => $"name:{tagBuilder.Tag}";

        public static string CreateTypeTag(
            this TagBuilder tagBuilder
        ) => $"type:{tagBuilder.Tag}";
    }
}
