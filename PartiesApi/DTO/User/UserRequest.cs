namespace PartiesApi.DTO.User;

public record UserRequest
{
    public string Login { get; init; }
    public string Password { get; init; }
}