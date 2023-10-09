namespace EventHorizon.Game.Client.Core.Command.Model;

public class CommandResult<T>
{
    public bool Success { get; }
    public T Result { get; }
    public string ErrorCode { get; }

    public CommandResult(T result)
    {
        Success = true;
        Result = result;
        ErrorCode = string.Empty;
    }

    public CommandResult(bool success, T result)
    {
        Success = success;
        Result = result;
        ErrorCode = string.Empty;
    }

    public CommandResult(string errorCode)
    {
        Success = false;
        ErrorCode = errorCode;
        Result = default!;
    }

    public static implicit operator CommandResult<T>(T result) => new(result);

    public static implicit operator CommandResult<T>(string errorCode) =>
        new(errorCode);

    public static implicit operator bool(CommandResult<T> result) =>
        result.Success;
}
