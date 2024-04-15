namespace EventHorizon.Game.Editor.Client.Shared.Properties;

using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorMonaco.Editor;
using EventHorizon.Game.Editor.Client.Zone.Api;
using EventHorizon.Game.Editor.Properties.Model;
using Microsoft.AspNetCore.Components;

public partial class ComplexPropertyControl
{
    [CascadingParameter]
    public ZoneState ZoneState { get; set; } = null!;

    public StandaloneCodeEditor MonacoEditor { get; set; } = null!;
    protected string PropertyEditorId => $"{PropertyName}-Editor";
    protected bool IsEditorOpen { get; set; }

    protected override object Parse(object value)
    {
        return JsonSerializer.Deserialize<Dictionary<string, object>>(value?.ToString() ?? "{}")
            ?? new Dictionary<string, object>();
    }

    public async Task HandleOpenEditor()
    {
        if (MonacoEditor.IsNotNull())
        {
            await MonacoEditor.SetValue(ToStringProperty());
        }
        IsEditorOpen = true;
    }

    public async Task HandleSave()
    {
        await HandleChange(new ChangeEventArgs { Value = await MonacoEditor.GetValue() });
        IsEditorOpen = false;
    }

    public StandaloneEditorConstructionOptions BuildConstructionOptions(StandaloneCodeEditor _) =>
        new()
        {
            Theme = "vs-dark",
            Language = "json",
            Value = ToStringProperty(),
            AutomaticLayout = true,
        };

    private string ToStringProperty()
    {
        if (Property is ComplexProperty)
        {
            return JsonSerializer.Serialize(Property);
        }

        return Property?.ToString() ?? "{ }";
    }
}
