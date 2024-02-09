namespace PartiesApi.DTO.User;

public record UserResponse
{
    public Guid Id { get; init; }
    public string Login { get; init; }
}