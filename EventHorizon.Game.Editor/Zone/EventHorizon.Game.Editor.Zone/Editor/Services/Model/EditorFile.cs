#pragma warning disable IDE0057 // Use range operator
namespace EventHorizon.Game.Editor.Zone.Editor.Services.Model;

using System.Collections.Generic;

public class EditorFile
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public IList<string> Path { get; set; } = new List<string>();
    public string Content { get; set; } = string.Empty;

    public (bool IsSimpleContent, string Content) GetContent(bool forceFull)
    {
        if (forceFull)
        {
            return (false, Content);
        }
        else if (Content.Contains("// <SimpleEditor>") && Content.Contains("// </SimpleEditor>"))
        {
            var simpleContent = Content.Substring(
                Content.IndexOf("// <SimpleEditor>") + "// <SimpleEditor>".Length,
                Content.IndexOf("// </SimpleEditor>")
                    - (Content.IndexOf("// <SimpleEditor>") + "// <SimpleEditor>".Length)
            );
            return (true, simpleContent.Trim());
        }

        return (false, Content);
    }

    public string BuildFromSimpleContent(string simpleContent, bool forceFull)
    {
        var (isSimpleContent, _) = GetContent(forceFull);
        if (!isSimpleContent)
        {
            return simpleContent;
        }

        var header = Content.Substring(0, Content.IndexOf("// <SimpleEditor>"));
        var footer = Content.Substring(
            Content.IndexOf("// </SimpleEditor>") + "// </SimpleEditor>".Length
        );

        return $"{header}// <SimpleEditor>\n{simpleContent}\n// </SimpleEditor>\n{footer}";
    }
}
