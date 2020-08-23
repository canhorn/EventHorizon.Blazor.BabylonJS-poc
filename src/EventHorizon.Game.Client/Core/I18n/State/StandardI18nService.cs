namespace EventHorizon.Game.Client.Core.I18n.State
{
    using System;
    using EventHorizon.Game.Client.Core.I18n.Api;

    public class StandardI18nService
        : II18nService,
        ILocalizer
    {
        public string this[string name]
        {
            get
            {
                // TODO: [1i8n] - Return from Bundle
                return name;
            }
        }
        public string this[string name, params object[] arguments]
        {
            get
            {
                // TODO: [1i8n] - Return from Bundle
                return string.Format(
                    name,
                    arguments
                );
            }
        }

        public void SetResourceBundle(
            II18nBundle bundle
        )
        {
            // TODO: [1i8n] - Set Bundle
            throw new NotImplementedException();
        }
    }
}
