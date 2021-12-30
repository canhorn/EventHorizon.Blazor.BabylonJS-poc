namespace EventHorizon.Game.Editor.Automation.ArtifactManagement.Components;

using Atata;

public class ArtifactTable<TOwner>
    : Table<ArtifactTableRow<TOwner>, TOwner>
    where TOwner : PageObject<TOwner>
{
    public TOwner GetFirstRowReferenceId(
        out string referenceId
    )
    {
        referenceId = string.Empty;
        if (Rows.Count == 0)
        {
            return Owner;
        }

        referenceId = Rows[0].ReferenceId.Value;

        return Owner;
    }
}

public class ArtifactTableRow<TOwner>
    : TableRow<TOwner>
    where TOwner : PageObject<TOwner>
{
    public Text<TOwner> Service { get; private set; }

    public Text<TOwner> ReferenceId { get; private set; }

    public Text<TOwner> Created { get; private set; }

    public Link<TOwner> Download { get; private set; }
}
