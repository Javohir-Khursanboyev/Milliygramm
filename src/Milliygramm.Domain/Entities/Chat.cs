using Milliygramm.Domain.Commons;
using Milliygramm.Domain.Enums;

namespace Milliygramm.Domain.Entities;

public class Chat : Auditable
{
    public string Name { get; set; }
    public ChatType ChatType { get; set; }  
    public long OwnerId { get; set; }
    public long? GroupId { get; set; }
    public Group Group { get; set; }
    public long? ParticipantId { get; set; }
    public User Participant { get; set; }
    public IEnumerable<Message> Messages { get; set; }
}
