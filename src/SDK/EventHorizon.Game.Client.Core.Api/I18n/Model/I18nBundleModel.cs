namespace EventHorizon.Game.Client.Core.I18n.Model
{
    using System;
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Core.I18n.Api;

    public class I18nBundleModel
        : Dictionary<string, string>,
        II18nBundle
    {
        public I18nBundleModel() : base() { }
        public I18nBundleModel(
            IDictionary<string, string> bundle
        ) : base(bundle)
        {
        }
    }
}
