using Microsoft.EntityFrameworkCore.Storage;
using Milliygramm.Data.DbContexts;
using Milliygramm.Data.Repositories;
using Milliygramm.Domain.Entities;

namespace Milliygramm.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext context;
    private IDbContextTransaction transaction;
    public UnitOfWork(AppDbContext context)
    {
        this.context = context;
        Chats = new Repository<Chat>(context);
        Roles = new Repository<Role>(context);
        Users = new Repository<User>(context);
        Groups = new Repository<Group>(context);
        Assets = new Repository<Asset>(context);
        Messages = new Repository<Message>(context);
        Permissions = new Repository<Permission>(context);
        UserDetails = new Repository<UserDetail>(context);
        GroupDetails = new Repository<GroupDetail>(context);
        GroupMembers = new Repository<GroupMember>(context);
        RolePermissions = new Repository<RolePermission>(context);
    }
    public IRepository<Chat> Chats { get; }

    public IRepository<Role> Roles { get; }

    public IRepository<User> Users { get; }

    public IRepository<Group> Groups { get; }

    public IRepository<Asset> Assets { get; }

    public IRepository<Message> Messages { get; }

    public IRepository<Permission> Permissions { get; }

    public IRepository<UserDetail> UserDetails { get; }

    public IRepository<GroupMember> GroupMembers { get; }

    public IRepository<GroupDetail> GroupDetails { get; }

    public IRepository<RolePermission> RolePermissions { get; }
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async Task BeginTransactionAsync()
    {
        transaction = await context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await transaction.CommitAsync();
    }


    public async Task<bool> SaveAsync()
    {
       return await context.SaveChangesAsync()>0;
    }
}
