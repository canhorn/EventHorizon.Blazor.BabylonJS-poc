namespace EventHorizon.Game.Editor.Client.Authentication.Model
{
    using System.Collections.Generic;
    using EventHorizon.Game.Editor.Client.Authentication.Api;

    public class SessionValuesModel
        : Dictionary<string, string>,
        SessionValues
    {
        public static string SESSION_VALUES_KEY => "SESSION_VALUES";

        public string Get(
            string key,
            string defaultValue
        )
        {
            if (TryGetValue(
                key,
                out var value
            ))
            {
                return value;
            }

            return defaultValue ?? string.Empty;
        }

        public long GetLong(
            string key
        )
        {
            var value = Get(key, "0");
            if (string.IsNullOrWhiteSpace(
                value
            ).IsNotTrue() && long.TryParse(
                value,
                out var parsedValue
            ))
            {
                return parsedValue;
            }
            return 0;
        }

        public decimal GetDecimal(
            string key
        )
        {
            var value = Get(key, "0.0");
            if (string.IsNullOrWhiteSpace(
                value
            ).IsNotTrue() && decimal.TryParse(
                value,
                out var parsedValue
            ))
            {
                return parsedValue;
            }
            return 0;
        }
    }
}
