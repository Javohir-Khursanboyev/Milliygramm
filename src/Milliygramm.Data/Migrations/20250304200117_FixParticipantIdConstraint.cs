using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Milliygramm.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixParticipantIdConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_group_details_PictureId",
                table: "group_details");

            migrationBuilder.DropIndex(
                name: "IX_chats_ParticipantId",
                table: "chats");

            migrationBuilder.CreateIndex(
                name: "IX_group_details_PictureId",
                table: "group_details",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_chats_ParticipantId",
                table: "chats",
                column: "ParticipantId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_group_details_PictureId",
                table: "group_details");

            migrationBuilder.DropIndex(
                name: "IX_chats_ParticipantId",
                table: "chats");

            migrationBuilder.CreateIndex(
                name: "IX_group_details_PictureId",
                table: "group_details",
                column: "PictureId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_chats_ParticipantId",
                table: "chats",
                column: "ParticipantId");
        }
    }
}
