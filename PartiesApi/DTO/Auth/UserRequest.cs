namespace PartiesApi.DTO.Auth;

public record UserRequest
{
    public string Login { get; init; }
    public string Password { get; init; }
}