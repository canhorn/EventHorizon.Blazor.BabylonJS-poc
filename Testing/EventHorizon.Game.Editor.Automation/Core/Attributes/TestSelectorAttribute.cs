namespace Atata
{
    public class TestSelectorAttribute
        : FindByAttributeAttribute
    {
        public TestSelectorAttribute(string selector)
            : base("data-test-selector", selector) { }
    }
}
