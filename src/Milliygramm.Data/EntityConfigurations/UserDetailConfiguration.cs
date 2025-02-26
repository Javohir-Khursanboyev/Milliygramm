using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Milliygramm.Domain.Entities;

namespace Milliygramm.Data.EntityConfigurations;

public class UserDetailConfiguration : IEntityTypeConfiguration<UserDetail>
{
    public void Configure(EntityTypeBuilder<UserDetail> builder)
    {
        builder.ToTable("user_details");

        builder.HasKey(ud => ud.Id);

        builder.HasIndex(ud => ud.UserId);

        builder.Property(ud => ud.Bio)
            .HasMaxLength(500);

        builder.Property(ud => ud.Location)
            .HasMaxLength(255);

        builder.HasOne(ud => ud.Picture)
            .WithMany()  
            .HasForeignKey(ud => ud.PictureId);
    }
}
