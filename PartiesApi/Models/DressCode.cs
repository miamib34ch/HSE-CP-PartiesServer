using System.ComponentModel.DataAnnotations;

namespace PartiesApi.Models;

public class DressCode
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string? Name { get; set; }
    public string? Description { get; set; }
}