using Milliygramm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Milliygramm.Data.EntityConfigurations;

public sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");

        builder.HasKey(role => role.Id);

        builder.Property(role => role.Name).IsRequired();

        builder.HasIndex(role => role.Name).IsUnique();

        builder.HasData(
                new Role { Id = Role.AdminId, Name = Role.Admin, CreatedAt = DateTime.UtcNow },
                new Role { Id = Role.UserId, Name = Role.User, CreatedAt = DateTime.UtcNow }
                //new Role { Id = 3, Name = "Guest", CreatedAt = DateTime.UtcNow }
            );
    }
}
