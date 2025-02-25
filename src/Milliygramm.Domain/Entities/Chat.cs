using Milliygramm.Domain.Enums;
using Milliygramm.Domain.Commons;

namespace Milliygramm.Domain.Entities;

public sealed class Chat : Auditable
{
    public string Name { get; set; }
    public ChatType ChatType { get; set; }
    public long OwnerId { get; set; }
    public User Owner { get; set; }
    public long? GroupId { get; set; }
    public Group Group { get; set; }
    public long? ParticipantId { get; set; }
    public User Participant { get; set; }
    public IEnumerable<Message> Messages { get; set; }

    public Chat()
    {
        if (Group != null)
            Name = Group.Name;
        else
            Name = $"{Participant.FirstName + Participant.LastName}";
    }
}
