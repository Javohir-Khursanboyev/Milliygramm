using Milliygramm.Domain.Commons;

namespace Milliygramm.Domain.Entities;

public sealed class Permission : Auditable
{
    public string Action {  get; set; }
    public string Controller { get; set; }
}
