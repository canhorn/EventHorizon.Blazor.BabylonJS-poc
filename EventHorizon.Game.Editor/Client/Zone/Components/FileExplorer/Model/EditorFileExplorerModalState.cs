namespace EventHorizon.Game.Editor.Client.Zone.Components.FileExplorer.Model;

using EventHorizon.Game.Editor.Client.Localization;
using EventHorizon.Game.Editor.Client.Localization.Api;
using EventHorizon.Game.Editor.Zone.Editor.Services.Model;
using Microsoft.AspNetCore.Components;

public class EditorFileExplorerModalState
{
    public EditorNode Node { get; set; } = new EditorNode();
    public bool IsOpen { get; set; }
    public EditorFileModalType ModalType { get; set; } = EditorFileModalType.None;
    public string TextInput { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
    public ElementReference InputFocusElement { get; set; }
    public ElementReference ButtonFocusElement { get; set; }
    public bool TriggerInputFocus { get; set; } = false;
    public bool TriggerButtonFocus { get; set; } = false;

    public string Placeholder(Localizer<SharedResource> localizer)
    {
        localizer.NullCheck();
        switch (ModalType)
        {
            case EditorFileModalType.AddFolder:
                return localizer["Enter name of new folder"];
            case EditorFileModalType.AddFile:
                return localizer["Enter name of new file"];
            default:
                break;
        }
        return string.Empty;
    }

    public string ModalName(Localizer<SharedResource> localizer)
    {
        localizer.NullCheck();
        switch (ModalType)
        {
            case EditorFileModalType.AddFolder:
                return localizer["Add Folder"];
            case EditorFileModalType.DeleteFolder:
                return localizer["Delete Folder"];
            case EditorFileModalType.AddFile:
                return localizer["Add File"];
            case EditorFileModalType.DeleteFile:
                return localizer["Delete File"];
            default:
                break;
        }
        return string.Empty;
    }

    public string DisplayMessage(Localizer<SharedResource> localizer)
    {
        localizer.NullCheck();
        switch (ModalType)
        {
            case EditorFileModalType.AddFolder:
                return localizer["Supply the name of the new Folder"];
            case EditorFileModalType.AddFile:
                return localizer["Supply the name of the new File"];
            case EditorFileModalType.DeleteFolder:
                return localizer["Are you sure you want to delete this Folder?"];
            case EditorFileModalType.DeleteFile:
                return localizer["Are you sure you want to delete this File?"];
            default:
                break;
        }
        return string.Empty;
    }

    public string CloseButtonText(Localizer<SharedResource> localizer)
    {
        localizer.NullCheck();
        switch (ModalType)
        {
            case EditorFileModalType.AddFolder:
            case EditorFileModalType.AddFile:
                return localizer["Close"];
            case EditorFileModalType.DeleteFolder:
            case EditorFileModalType.DeleteFile:
                return localizer["No"];
            default:
                break;
        }
        return string.Empty;
    }

    public string SubmitButtonText(Localizer<SharedResource> localizer)
    {
        localizer.NullCheck();
        switch (ModalType)
        {
            case EditorFileModalType.AddFolder:
            case EditorFileModalType.AddFile:
                return localizer["Submit"];
            case EditorFileModalType.DeleteFolder:
            case EditorFileModalType.DeleteFile:
                return localizer["Yes"];
            default:
                break;
        }
        return string.Empty;
    }

    public string SubmitButtonCssState
    {
        get
        {
            switch (ModalType)
            {
                case EditorFileModalType.DeleteFolder:
                case EditorFileModalType.DeleteFile:
                    return "alert";
                default:
                    return "primary";
            }
        }
    }

    public void HandleModalClosed()
    {
        IsOpen = false;
    }

    public void Reset()
    {
        IsOpen = false;
        ModalType = EditorFileModalType.None;
        TextInput = string.Empty;
        ErrorMessage = string.Empty;
        TriggerInputFocus = false;
        TriggerButtonFocus = false;
    }
}
