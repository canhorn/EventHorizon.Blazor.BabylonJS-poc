namespace Atata
{
    public class SelectorAttribute
        : FindByAttributeAttribute
    {
        public SelectorAttribute(string selector)
            : base("data-selector", selector) { }
    }
}
