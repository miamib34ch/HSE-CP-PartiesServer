namespace PartiesApi.Services.JWT;

internal record JwtConfig
{
    public string? Secret { get; init; }
}