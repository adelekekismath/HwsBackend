using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HwsBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixManyToManyRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuideInvitations_AspNetUsers_InvitedUsersId",
                table: "GuideInvitations");

            migrationBuilder.DropForeignKey(
                name: "FK_GuideInvitations_Guides_InvitedGuidesId",
                table: "GuideInvitations");

            migrationBuilder.RenameColumn(
                name: "InvitedUsersId",
                table: "GuideInvitations",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "InvitedGuidesId",
                table: "GuideInvitations",
                newName: "GuideId");

            migrationBuilder.RenameIndex(
                name: "IX_GuideInvitations_InvitedUsersId",
                table: "GuideInvitations",
                newName: "IX_GuideInvitations_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GuideInvitations_AspNetUsers_UserId",
                table: "GuideInvitations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GuideInvitations_Guides_GuideId",
                table: "GuideInvitations",
                column: "GuideId",
                principalTable: "Guides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuideInvitations_AspNetUsers_UserId",
                table: "GuideInvitations");

            migrationBuilder.DropForeignKey(
                name: "FK_GuideInvitations_Guides_GuideId",
                table: "GuideInvitations");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "GuideInvitations",
                newName: "InvitedUsersId");

            migrationBuilder.RenameColumn(
                name: "GuideId",
                table: "GuideInvitations",
                newName: "InvitedGuidesId");

            migrationBuilder.RenameIndex(
                name: "IX_GuideInvitations_UserId",
                table: "GuideInvitations",
                newName: "IX_GuideInvitations_InvitedUsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_GuideInvitations_AspNetUsers_InvitedUsersId",
                table: "GuideInvitations",
                column: "InvitedUsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GuideInvitations_Guides_InvitedGuidesId",
                table: "GuideInvitations",
                column: "InvitedGuidesId",
                principalTable: "Guides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
