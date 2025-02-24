using Milliygramm.Data.Repositories;
using Milliygramm.Domain.Entities;

namespace Milliygramm.Data.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IRepository<Chat> Chats { get; }
    IRepository<Role> Roles { get; }
    IRepository<User> Users {  get; }
    IRepository<Group> Groups { get; }
    IRepository<Asset> Assets { get; }
    IRepository<Message> Messages { get; }
    IRepository<Permission> Permissions { get; }
    IRepository<UserDetail> UserDetails { get; }
    IRepository<GroupMember> GroupMembers { get; }
    IRepository<GroupDetail> GroupDetails { get; }
    IRepository<RolePermission> RolePermissions { get; }

    Task<bool> SaveAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
}
