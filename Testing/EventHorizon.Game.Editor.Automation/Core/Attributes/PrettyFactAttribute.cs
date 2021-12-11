namespace Xunit;

using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

public class PrettyFactAttribute : FactAttribute
{
    public PrettyFactAttribute(string testName = "")
    {
        if (!string.IsNullOrEmpty(testName))
        {
            base.DisplayName = SplitCamelCase(testName);
        }
    }

    public static string SplitCamelCase(
        string camelCaseString
    )
    {
        return Regex
            .Replace(
                camelCaseString,
                "([A-Z])",
                " $1",
                RegexOptions.Compiled
            )
            .Trim();
    }
}

[Sdk.XunitTestCaseDiscoverer(
    "Xunit.Sdk.TheoryDiscoverer",
    "xunit.execution.{Platform}"
)]
public class PrettyTheoryAttribute : PrettyFactAttribute
{
    public PrettyTheoryAttribute(
        [CallerMemberName] string testMethodName = ""
    ) : base(testMethodName) { }
}
