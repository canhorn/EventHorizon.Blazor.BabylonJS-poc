namespace EventHorizon.Game.Editor.Automation.ArtifactManagement.Components;

using Atata;

public class ArtifactTableRow<TOwner>
    : TableRow<TOwner>
    where TOwner : PageObject<TOwner>
{
    public Text<TOwner> Service { get; private set; }

    public Text<TOwner> ReferenceId { get; private set; }

    public Text<TOwner> Created { get; private set; }

    public Link<TOwner> Download { get; private set; }

}
