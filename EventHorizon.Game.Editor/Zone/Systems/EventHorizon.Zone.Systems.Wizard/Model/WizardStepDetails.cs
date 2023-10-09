namespace EventHorizon.Zone.Systems.Wizard.Model;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

public class WizardStepDetails : Dictionary<string, string>
{
    private static string NormalizeKey(string key) =>
        char.ToLowerInvariant(key[0]) + key[1..];

    public new string this[string key]
    {
        get =>
            ContainsKey(NormalizeKey(key))
                ? base[NormalizeKey(key)]
                : string.Empty;
    }

    public new bool TryGetValue(
        string key,
        [MaybeNullWhen(false)] out string value
    ) => base.TryGetValue(NormalizeKey(key), out value);
}
