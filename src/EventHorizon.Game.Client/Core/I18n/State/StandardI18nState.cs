namespace EventHorizon.Game.Client.Core.I18n.State
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EventHorizon.Game.Client.Core.I18n.Api;
    using EventHorizon.Game.Client.Core.I18n.Model;

    public class StandardI18nState
        : II18nState,
        ILocalizer
    {
        private II18nBundle _bundle = new I18nBundleModel();

        public string this[string name]
        {
            get
            {
                if (_bundle.TryGetValue(
                    name,
                    out var value
                ))
                {
                    return value;
                }
                return name;
            }
        }
        public string this[string name, params object[] arguments]
        {
            get
            {
                return string.Format(
                    this[name],
                    arguments
                );
            }
        }

        public void SetResourceBundle(
            II18nBundle bundle
        )
        {
            var newBundleDictionary = new List<II18nBundle>
            {
                _bundle,
                bundle
            }.SelectMany(
                a => a
            ).ToLookup(
                pair => pair.Key, pair => pair.Value
            ).ToDictionary(
                a => a.Key,
                a => a.Last()
            );
            _bundle = new I18nBundleModel(
                newBundleDictionary
            );
        }
    }
}
