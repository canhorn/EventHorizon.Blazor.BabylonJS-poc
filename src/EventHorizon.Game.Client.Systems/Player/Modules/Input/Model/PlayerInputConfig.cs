namespace EventHorizon.Game.Client.Systems.Player.Modules.Input.Model;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

public class PlayerInputConfig
{
    public const string PROPERTY_NAME = "playerInput";

    public int MovementDelay { get; set; } = 1000; // 1 second delay
    public List<PlayerKeyInput> KeyInputList { get; set; } =
        new List<PlayerKeyInput>();
    public bool StopMovementOnTick { get; set; } = true;

    public class PlayerKeyInput : Dictionary<string, object>
    {
        public string Key
        {
            get { return this["key"].ToString() ?? string.Empty; }
            set { this["key"] = value; }
        }
        public string Type
        {
            get { return this["type"].ToString() ?? string.Empty; }
            set { this["type"] = value; }
        }

        public Option<T> Get<T>(string key)
        {
            if (TryGetValue(key.LowercaseFirstChar(), out var result))
            {
                return result.To<T>();
            }

            return default(T);
        }

        public bool TryGet<T>(string key, [NotNullWhen(true)] out T? result)
        {
            result = default;
            if (TryGetValue(key.LowercaseFirstChar(), out var found))
            {
                result = found.To<T>();
                if (result.IsNotNull())
                {
                    return true;
                }
            }

            return false;
        }
    }
}
