using Milliygramm.Domain.Entities;
using Milliygramm.Domain.Enums;

namespace Milliygramm.Model.DTOs.Chats;

public sealed class ChatVievModel
{
    public string Name { get; set; }
    public ChatType ChatType { get; set; }
}
