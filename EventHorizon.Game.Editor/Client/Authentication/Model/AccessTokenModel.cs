namespace EventHorizon.Game.Editor.Client.Authentication.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AccessTokenModel
    {
        public bool IsFilled { get; }
        public string AccessToken { get; } = string.Empty;

        public AccessTokenModel()
        {
            IsFilled = false;
        }

        public AccessTokenModel(
            string accessToken
        )
        {
            IsFilled = !string.IsNullOrWhiteSpace(accessToken);
            AccessToken = accessToken;
        }
    }
}
