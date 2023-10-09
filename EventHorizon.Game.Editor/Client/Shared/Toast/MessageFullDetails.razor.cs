namespace EventHorizon.Game.Editor.Client.Shared.Toast;

using EventHorizon.Game.Editor.Client.Localization;
using EventHorizon.Game.Editor.Client.Localization.Api;
using EventHorizon.Game.Editor.Client.Shared.Toast.Model;

using Microsoft.AspNetCore.Components;

public class MessageFullDetailsModel : ComponentBase
{
    [Parameter]
    public MessageModel Message { get; set; }

    [Inject]
    public Localizer<SharedResource> Localizer { get; set; } = null!;

    public string LocalizedMessageLevel { get; set; } = string.Empty;
    public string MessageLevelStyle { get; set; } = string.Empty;

    protected override void OnInitialized()
    {
        SetupStyle();
    }

    protected override void OnParametersSet()
    {
        SetupStyle();
    }

    private void SetupStyle()
    {
        MessageLevelStyle = string.Empty;
        LocalizedMessageLevel = string.Empty;
        if (Message.Level == MessageLevel.Success)
        {
            LocalizedMessageLevel = Localizer["Success Message"];
            MessageLevelStyle = "--success";
        }
        else if (Message.Level == MessageLevel.Warning)
        {
            LocalizedMessageLevel = Localizer["Warning Message"];
            MessageLevelStyle = "--warning";
        }
        else if (Message.Level == MessageLevel.Error)
        {
            LocalizedMessageLevel = Localizer["Error Message"];
            MessageLevelStyle = "--error";
        }
    }
}
