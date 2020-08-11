namespace EventHorizon.Game.Client.Core.Query.Model
{
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

        public QueryResult(
            string errorCode
        )
        {
            Success = false;
            ErrorCode = errorCode;
#pragma warning disable CS8601 // Possible null reference assignment.
            Result = default;
#pragma warning restore CS8601 // Possible null reference assignment.
        }
    }
}
