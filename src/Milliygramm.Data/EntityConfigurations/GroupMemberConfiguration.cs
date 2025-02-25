using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Milliygramm.Domain.Entities;

namespace Milliygramm.Data.EntityConfigurations;

public class GroupMemberConfiguration : IEntityTypeConfiguration<GroupMember>
{
    public void Configure(EntityTypeBuilder<GroupMember> builder)
    {
        builder.ToTable("group_members");

        builder.HasKey(gm => gm.Id);

        builder.HasOne(gm => gm.Group)
        .WithMany(g => g.GroupMembers)
        .HasForeignKey(gm => gm.GroupId)
        .OnDelete(DeleteBehavior.Cascade); // Guruh o‘chsa, a'zolik ham o‘chadi

        builder.HasOne(gm => gm.Member)
            .WithMany()
            .HasForeignKey(gm => gm.MemberId)
            .OnDelete(DeleteBehavior.Cascade); // User o‘chsa, a'zolik ham o‘chadi
    }
}

