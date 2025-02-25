using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Milliygramm.Domain.Entities;

namespace Milliygramm.Data.EntityConfigurations;

public sealed class GroupDetailConfiguration : IEntityTypeConfiguration<GroupDetail>
{
    public void Configure(EntityTypeBuilder<GroupDetail> builder)
    {
        builder.ToTable("group_details");

        builder.HasKey(gd => gd.Id);

        builder.Property(gd => gd.Link)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasIndex(gd => gd.Link).IsUnique();

        builder.Property(gd => gd.Privacy).IsRequired();

        builder.Property(gd => gd.Description).HasMaxLength(255);

        builder.HasOne(gd => gd.Asset)
            .WithOne()
            .HasForeignKey<GroupDetail>(gd => gd.PictureId);    
    }
}
