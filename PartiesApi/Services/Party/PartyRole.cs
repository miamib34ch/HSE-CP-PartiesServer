using System.ComponentModel;

namespace PartiesApi.Services.Party;

internal enum PartyRole
{
    [Description("Member")]
    Member,
    [Description("Editor")]
    Editor,
    [Description("Organizer")]
    Organizer
}