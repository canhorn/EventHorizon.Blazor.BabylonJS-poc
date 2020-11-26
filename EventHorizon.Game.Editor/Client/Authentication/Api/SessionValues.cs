namespace EventHorizon.Game.Editor.Client.Authentication.Api
{
    using System.Collections.Generic;

    public interface SessionValues
        : IDictionary<string, string>
    {
        string Get(
            string key,
            string defaultValue
        );
        long GetLong(
            string key
        );
        decimal GetDecimal(
            string key
        );
    }
}
