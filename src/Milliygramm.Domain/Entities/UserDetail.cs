using Milliygramm.Domain.Commons;

namespace Milliygramm.Domain.Entities;

public sealed class UserDetail : Auditable
{
    public string Bio {  get; set; }
    public DateTime DataOfBirth { get; set; }
    public string Location { get; set; }
    public long UserId { get; set; }
    public long? PictureId { get; set; }
    public Asset Picture { get; set; }
}
