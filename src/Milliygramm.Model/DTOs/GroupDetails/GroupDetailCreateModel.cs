using Milliygramm.Domain.Entities;
using Milliygramm.Domain.Enums;

namespace Milliygramm.Model.DTOs.GroupDetails;

public sealed class GroupDetailCreateModel
{
    public long GroupId { get; set; }
    public string Description { get; set; }
    public Privacy Privacy { get; set; }
    public string Link { get; set; }
}

