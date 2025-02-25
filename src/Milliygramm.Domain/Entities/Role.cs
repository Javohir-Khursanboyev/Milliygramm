using Milliygramm.Domain.Commons;

namespace Milliygramm.Domain.Entities;

public sealed class Role : Auditable
{
    public const string Admin = "Admin";
    public const string User = "User";
    public const long AdminId = 1;
    public const long UserId = 2;

    public string Name { get; set; }
    public IEnumerable<RolePermission> RolePermissions { get; set; }
}
