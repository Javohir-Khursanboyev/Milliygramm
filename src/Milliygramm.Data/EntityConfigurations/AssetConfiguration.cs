using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Milliygramm.Domain.Entities;
using Milliygramm.Domain.Enums;

namespace Milliygramm.Data.EntityConfigurations;

public class AssetConfiguration : IEntityTypeConfiguration<Asset>
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
            .HasMaxLength(500);

        builder.Property(asset => asset.FileType)
            .IsRequired();

        builder.HasIndex(asset => asset.FileType);

        // Default Image Seed Data qo'shish
        builder.HasData(
            new Asset
            {
                Id = 1,
                Name = "DefaultImage.png",
                Path = "/images/DefaultImage.png",
                FileType = FileType.Images,
                CreatedAt = new DateTime(2025, 01, 01)
            }
        );
    }
}
