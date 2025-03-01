using Milliygramm.Domain.Entities;
using Milliygramm.Domain.Enums;

namespace Milliygramm.Model.DTOs.Chats;

public sealed class ChatVievModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public ChatType ChatType { get; set; }
}
