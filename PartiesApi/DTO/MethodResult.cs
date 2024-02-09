namespace PartiesApi.DTO;

public record MethodResult
{
    public MethodResult(string methodName, bool isSuccess, string error)
    {
        MethodName = methodName;
        IsSuccess = isSuccess;
        Error = error;
    }

    public string MethodName { get; init; }
    public bool IsSuccess { get; init; }
    public string? Error { get; init; }
}

public record MethodResult<T> : MethodResult
{
    public MethodResult(string methodName, bool isSuccess, string error) : base(methodName, isSuccess, error)
    {
    }

    public MethodResult(string methodName, bool isSuccess, string error, T result) : base(methodName, isSuccess, error)
    {
        Result = result;
    }

    public T? Result { get; init; }
}