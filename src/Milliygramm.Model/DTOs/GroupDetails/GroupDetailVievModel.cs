using Milliygramm.Domain.Enums;
using Milliygramm.Model.DTOs.Assets;

namespace Milliygramm.Model.DTOs.GroupDetails;

public sealed class GroupDetailVievModel
{
    public long Id { get; set; }
    public string Description { get; set; }
    public Privacy Privacy { get; set; }
    public string Link { get; set; }
    public AssetViewModel Asset { get; set; }
}
