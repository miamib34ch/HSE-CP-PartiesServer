namespace PartiesApi.DTO.Auth;

public record UserRequest
{
    public string Login { get; }
    public string Password { get; }
}