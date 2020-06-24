namespace BabylonJS.Html
{
    using System.Threading.Tasks;
    using EventHorizon.Blazor.Interop;

    public class Canvas : CachedEntity
    {
        public static async Task<Canvas> Create(
            string elementId
        ) => new Canvas(
            await EventHorizonBlazorInteropt.Func(
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
