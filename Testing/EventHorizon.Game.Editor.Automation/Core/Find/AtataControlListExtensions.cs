namespace Atata
{
    public static class AtataControlListExtensions
    {
        public static TItem FindByTestKey<TItem, TOwner>(
            this ControlList<TItem, TOwner> control,
            string key
        ) where TItem : Control<TOwner>
            where TOwner : PageObject<TOwner>
        {
            return control.GetByXPathCondition(
                $"//*[@data-test-key=\"{key}\"]"
            );
        }
    }
}
