namespace PartiesApi.DTO.Auth;

public record AuthResult
{
    public string? AccessToken { get; init; }
    public bool IsSuccess { get; init; }
    public string? Error { get; init; }
    public Guid? UserId { get; init; }
}