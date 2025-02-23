using Milliygramm.Domain.Commons;
using Milliygramm.Domain.Enums;

namespace Milliygramm.Domain.Entities;

public class GroupDetail : Auditable
{
    public long GroupId { get; set; }
    public string Description { get; set; }
    public Privacy Privacy { get; set; }
    public string Link { get; set; }
    public long PictureId { get; set; }
    public Asset Asset { get; set; }
}
