namespace EventHorizon.Html.Interop;

using BabylonJS;

using EventHorizon.Blazor.Interop;

public class Canvas : HTMLCanvasElementCachedEntity
{
    public static Canvas Create(string elementId) =>
        EventHorizonBlazorInterop.FuncClass(
            entity => new Canvas(entity),
            new string[] { "document", "getElementById" },
            elementId
        );

    private Canvas(ICachedEntity entity)
    {
        ___guid = entity.___guid;
    }
}
