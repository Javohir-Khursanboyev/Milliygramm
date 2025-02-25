using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Milliygramm.Domain.Entities;

namespace Milliygramm.Data.DbContexts;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Asset> Assets {  get; set; }   
    public DbSet<Message> Messages { get; set; }
    public DbSet<UserDetail> UserDetails { get; set; }
    public DbSet<GroupDetail> GroupDetail { get; set; }
    public DbSet<GroupMember> GroupMembers { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
