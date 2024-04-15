namespace EventHorizon.Game.Editor.Client.PlayerEditor.Components.Property;

using System.Text.Json;
using System.Threading.Tasks;
using BlazorMonaco.Editor;
using EventHorizon.Game.Client.Systems.Combat.Model;
using EventHorizon.Game.Editor.Client.Shared.Components;
using Microsoft.AspNetCore.Components;

public class PlayerEditorSkillStateComponentBase : EditorComponentBase
{
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true, };

    [Parameter]
    public required object Data { get; set; }

    [Parameter]
    public required EventCallback<PropertyChangeArgs> OnDataChange { get; set; }

    protected string PropertyEditorId = "skillState-Editor";
    public StandaloneCodeEditor MonacoEditor { get; set; } = null!;
    public bool IsEditorOpen { get; set; }

    protected void HandleOpenEditor()
    {
        IsEditorOpen = true;
    }

    private string ToStringProperty()
    {
        return JsonSerializer.Serialize(Data, JsonOptions);
    }

    protected async Task HandleUpdate()
    {
        var dataAsString = await MonacoEditor.GetValue();
        await OnDataChange.InvokeAsync(
            new PropertyChangeArgs(
                ISkillState.NAME,
                JsonSerializer.Deserialize<object>(dataAsString)!
            )
        );
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
}
