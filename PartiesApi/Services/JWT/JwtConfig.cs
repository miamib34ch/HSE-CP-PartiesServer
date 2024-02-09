namespace PartiesApi.Services.JWT;

public record JwtConfig
{
    public string? Secret { get; init; }
}