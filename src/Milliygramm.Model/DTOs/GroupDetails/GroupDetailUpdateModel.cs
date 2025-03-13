using Milliygramm.Domain.Enums;

namespace Milliygramm.Model.DTOs.GroupDetails;

public sealed class GroupDetailUpdateModel
{
    public string Description { get; set; }
    public Privacy Privacy { get; set; }
    public string Link { get; set; }
}
