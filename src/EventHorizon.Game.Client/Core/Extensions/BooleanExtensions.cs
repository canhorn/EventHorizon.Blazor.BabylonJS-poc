#pragma warning disable CA1050 // Declare types in namespaces
public static class BooleanExtensions
#pragma warning restore CA1050 // Declare types in namespaces
{
    public static bool IsNotTrue(
        this bool value
    )
    {
        return !value;
    }
}
