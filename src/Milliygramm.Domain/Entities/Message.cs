using System.Reflection.Metadata;
using Milliygramm.Domain.Commons;

namespace Milliygramm.Domain.Entities;

public class Message : Auditable
{
    public long ChatId { get; set; }
    public Chat Chat { get; set; }
    public string Text { get; set; }
    public long? ContentId { get; set; }
    public long SendetId { get; set; }
    public User User { get; set; }
}
