namespace EventHorizon.Game.Client.Core.Query.Model;

public struct QueryResult<T>
{
    public bool Success { get; }
    public T Result { get; }
    public string ErrorCode { get; }

    public QueryResult(T result)
    {
        Success = true;
        Result = result;
        ErrorCode = string.Empty;
    }

    public QueryResult(string errorCode)
    {
        Success = false;
        ErrorCode = errorCode;
        Result = default!;
    }
}
