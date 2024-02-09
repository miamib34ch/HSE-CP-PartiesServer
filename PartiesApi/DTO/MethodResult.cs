namespace PartiesApi.DTO;

public record MethodResult
{
    public MethodResult(string methodName, bool isSuccess, string error)
    {
        MethodName = methodName;
        IsSuccess = isSuccess;
        Error = error;
    }

    public string MethodName { get; set; }
    public bool IsSuccess { get; set; }
    public string? Error { get; set; }
}