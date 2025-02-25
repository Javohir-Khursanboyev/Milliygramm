using Milliygramm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Milliygramm.Data.EntityConfigurations;

public sealed class ChatConfiguration : IEntityTypeConfiguration<Chat>
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
            .HasForeignKey<Chat>(chat => chat.GroupId)
            .IsRequired(false);

        builder.HasOne(chat => chat.Participant)
            .WithOne()
            .HasForeignKey<Chat>(chat => chat.ParticipantId)
            .IsRequired(false);

        builder.HasOne(chat => chat.Owner)
            .WithMany(owner => owner.Chats)
            .HasForeignKey(chat => chat.OwnerId);
    }
}
