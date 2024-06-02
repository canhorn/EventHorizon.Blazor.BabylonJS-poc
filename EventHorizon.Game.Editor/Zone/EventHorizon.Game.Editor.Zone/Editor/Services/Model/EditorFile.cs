#pragma warning disable IDE0057 // Use range operator
namespace EventHorizon.Game.Editor.Zone.Editor.Services.Model;

using System.Collections.Generic;

public class EditorFile
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public IList<string> Path { get; set; } = [];
    public string Content { get; set; } = string.Empty;

    public (bool IsSimpleContent, string Content) GetContent()
    {
        if (Content.Contains("// <SimpleEditor>") && Content.Contains("// </SimpleEditor>"))
        {
            var simpleContent = Content.Substring(
                Content.IndexOf("// <SimpleEditor>") + "// <SimpleEditor>".Length,
                Content.IndexOf("// </SimpleEditor>")
                    - (Content.IndexOf("// <SimpleEditor>") + "// <SimpleEditor>".Length)
            );
            return (true, simpleContent);
        }

        return (false, Content);
    }

    public string BuildFromSimpleContent(string simpleContent, bool isSimpleContent)
    {
        if (!isSimpleContent)
        {
            return simpleContent;
        }

        var header = Content.Substring(0, Content.IndexOf("// <SimpleEditor>"));
        var footer = Content.Substring(
            Content.IndexOf("// </SimpleEditor>") + "// </SimpleEditor>".Length
        );

        return $"{header}// <SimpleEditor>\r\n{simpleContent}\r\n// </SimpleEditor>{footer}";
    }
}
