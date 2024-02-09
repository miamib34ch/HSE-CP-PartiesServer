namespace PartiesApi.Models;

internal class PartyRule
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Description { get; set; }
    public IList<Party> Parties { get; set; }
}