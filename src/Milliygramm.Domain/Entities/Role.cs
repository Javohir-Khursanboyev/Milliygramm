using Milliygramm.Domain.Commons;

namespace Milliygramm.Domain.Entities;

public class Role : Auditable
{
    public string Name { get; set; }
    public IEnumerable<RolePermission> RolePermissions { get; set; }
}
