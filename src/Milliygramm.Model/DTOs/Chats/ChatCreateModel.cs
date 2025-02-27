using Milliygramm.Domain.Entities;
using Milliygramm.Domain.Enums;

namespace Milliygramm.Model.DTOs.Chats;

public sealed class ChatCreateModel
{
    public ChatType ChatType { get; set; }
    public long OwnerId { get; set; }
    public long? GroupId { get; set; }
    public long? ParticipantId { get; set; }
}
