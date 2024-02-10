namespace PartiesApi.DTO.DressCode;

public record DressCodeRequest
{
    public Guid? Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
}