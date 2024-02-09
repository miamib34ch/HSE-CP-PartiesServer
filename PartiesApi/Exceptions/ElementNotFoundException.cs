namespace PartiesApi.Exceptions;

public class ElementNotFoundException(string elementName, Guid elementId) : Exception
{
    public string ElementName { get; set; } = elementName;
    public Guid ElementId { get; set; } = elementId;
}