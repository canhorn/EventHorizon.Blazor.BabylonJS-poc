namespace EventHorizon.Zone.Systems.Wizard.Model;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

public class WizardData : Dictionary<string, string>
{
    public new string this[string key]
    {
        get => ContainsKey(key) ? base[key] : string.Empty;
        set => base[key] = value;
    }

    public new bool TryGetValue(
        string key,
        [MaybeNullWhen(false)] out string value
    ) => base.TryGetValue(key, out value);
}
