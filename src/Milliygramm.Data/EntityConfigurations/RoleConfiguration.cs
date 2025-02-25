using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Milliygramm.Domain.Entities;

namespace Milliygramm.Data.EntityConfigurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");

        builder.HasKey(role => role.Id);

        builder.Property(role => role.Name).IsRequired();

        builder.HasIndex(role => role.Name).IsUnique();

        builder.HasData(
                new Role { Id = 1, Name = "Admin", CreatedAt = DateTime.UtcNow },
                new Role { Id = 2, Name = "User", CreatedAt = DateTime.UtcNow }
                //new Role { Id = 3, Name = "Guest", CreatedAt = DateTime.UtcNow }
            );
    }
}
