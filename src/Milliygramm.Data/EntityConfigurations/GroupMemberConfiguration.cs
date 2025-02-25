using Milliygramm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Milliygramm.Data.EntityConfigurations;

public sealed class GroupMemberConfiguration : IEntityTypeConfiguration<GroupMember>
{
    public void Configure(EntityTypeBuilder<GroupMember> builder)
    {
        builder.ToTable("group_members");

        builder.HasKey(gm => gm.Id);

        builder.HasOne(gm => gm.Group)
        .WithMany(g => g.GroupMembers)
        .HasForeignKey(gm => gm.GroupId)
        .OnDelete(DeleteBehavior.Cascade); // Guruh o‘chsa, a'zolik ham o‘chadi
    }
}

