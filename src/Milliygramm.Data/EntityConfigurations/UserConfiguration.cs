using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Milliygramm.Domain.Entities;

namespace Milliygramm.Data.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(user => user.Id);

        builder.Property(user => user.UserName)
            .IsRequired()
            .HasMaxLength(255);
        builder.HasIndex(user => user.UserName).IsUnique();

        builder.Property(user => user.FirstName)
            .IsRequired()
            .HasMaxLength(255);
        builder.Property(user => user.Email).IsRequired();

        builder.HasIndex(user => user.Email).IsUnique();

        builder.Property(user => user.Password).IsRequired();

        builder.HasOne(user => user.UserDetail)
        .WithOne()
        .HasForeignKey<UserDetail>(detail => detail.UserId);
    }
}
