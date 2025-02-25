using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Milliygramm.Domain.Entities;

namespace Milliygramm.Data.EntityConfigurations;

public class ChatConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.ToTable("chats");

        builder.HasKey(chat =>  chat.Id);   

        builder.Property(chat => chat.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(chat => chat.ChatType).IsRequired();

        builder.HasOne(chat => chat.Group)
            .WithOne()
            .HasForeignKey<Chat>(chat => chat.GroupId);

        builder.HasOne(chat => chat.Owner)
            .WithMany(owner => owner.Chats)
            .HasForeignKey(chat => chat.OwnerId);
    }
}
