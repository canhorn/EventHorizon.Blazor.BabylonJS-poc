namespace EventHorizon.Game.Editor.Client.GraphEditor.Models;

using System.Diagnostics.CodeAnalysis;

public abstract class ProcessStep : BaseNode
{
    public NodePortCollection? Inputs;
    public NodePortCollection? Outputs;

    /// <summary>
    /// Check if this node has already computed and cached its outputs
    /// </summary>
    /// <returns>True if the outputs are already computed, false otherwise</returns>
    [MemberNotNullWhen(false, nameof(Outputs))]
    public virtual bool HasCachedOutput()
    {
        if (Outputs == null)
        {
            return true;
        }

        foreach (var output in Outputs.Enumerate())
        {
            if (!output.HasValue)
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Reset the inputs and outputs clearing all computed values
    /// </summary>
    public void Reset()
    {
        if (Inputs != null)
        {
            foreach (var input in Inputs.Enumerate())
            {
                input.Clear();
            }
        }

        if (Outputs != null)
        {
            foreach (var output in Outputs.Enumerate())
            {
                output.Clear();
            }
        }
    }

    /// <summary>
    /// Force the node to recalculate all of its outputs
    /// </summary>
    public abstract void Recalculate();
}
