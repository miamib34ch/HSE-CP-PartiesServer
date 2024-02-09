namespace PartiesApi.DTO.DressCode;

public record DressCodeResponse
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
}