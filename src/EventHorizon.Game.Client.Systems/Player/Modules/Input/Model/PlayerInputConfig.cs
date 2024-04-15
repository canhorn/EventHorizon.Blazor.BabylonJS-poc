namespace EventHorizon.Game.Client.Systems.Player.Modules.Input.Model;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EventHorizon.Game.Client.Engine.Input.Model;

public class PlayerInputConfig
{
    public const string PROPERTY_NAME = "playerInput";

    public int MovementDelay { get; set; } = 1000; // 1 second delay
    public List<PlayerKeyInput> KeyInputList { get; set; } = [];
    public Dictionary<string, PlayerKeyInput> KeyInputMap { get; set; } = [];
    public bool StopMovementOnTick { get; set; } = true;

    public class PlayerKeyInput : Dictionary<string, object>, KeyInputBase
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

        public MoveDirection? Pressed =>
            TryGet<int>("pressed", out var pressedResult) ? (MoveDirection)pressedResult : null;

        public MoveDirection? Released =>
            TryGet<int>("released", out var releasedResult) ? (MoveDirection)releasedResult : null;

        public string? Camera =>
            TryGet<string>("camera", out var cameraResult) ? cameraResult : null;

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
