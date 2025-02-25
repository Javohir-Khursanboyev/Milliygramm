using Milliygramm.Domain.Enums;
using Milliygramm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Milliygramm.Data.EntityConfigurations;

public sealed class AssetConfiguration : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {

        builder.ToTable("assets");

        builder.HasKey(asset => asset.Id);

        builder.Property(asset => asset.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(asset => asset.Path)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(asset => asset.FileType)
            .IsRequired();

        builder.HasIndex(asset => asset.FileType);

        // Default Image Seed Data qo'shish
        builder.HasData(
            new Asset
            {
                Id = Asset.DefaultPictureId,
                Name = Asset.DefaultPictureName,
                Path = Asset.DefaultPicturePath,
                FileType = FileType.Images,
                CreatedAt = new DateTime(2025, 01, 01)
            }
        );
    }
}
