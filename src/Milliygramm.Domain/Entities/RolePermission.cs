using Milliygramm.Domain.Commons;

namespace Milliygramm.Domain.Entities;

public sealed class RolePermission : Auditable
{
    public long RoleId { get; set; }
    public Role Role { get; set; }
    public long PermissionId { get; set; }
    public Permission Permission { get; set; }
}
