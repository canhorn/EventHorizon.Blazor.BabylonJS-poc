namespace BabylonJS.Html
{
    using System.Threading.Tasks;
    using EventHorizon.Blazor.Interop;

    public class Canvas : CachedEntity
    {
        public static Canvas Create(
            string elementId
        ) => new Canvas(
            EventHorizonBlazorInteropt.Func(
                new string[] { "document", "getElementById" },
                elementId
            )
        );

        private Canvas(
            CachedEntity entity
        )
        {
            ___guid = entity.___guid;
        }
    }
}
