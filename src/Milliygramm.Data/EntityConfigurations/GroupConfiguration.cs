using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Milliygramm.Domain.Entities;

namespace Milliygramm.Data.EntityConfigurations;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.ToTable("groups");

        builder.HasKey(group => group.Id);

        builder.Property(group => group.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasMany(group => group.GroupMembers)
            .WithOne()
            .HasForeignKey(gm => gm.MemberId);
    }
}
