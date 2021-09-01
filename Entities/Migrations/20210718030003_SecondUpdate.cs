using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class SecondUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_owner_OwnerId",
                table: "Accounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "account");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_OwnerId",
                table: "account",
                newName: "IX_account_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_account",
                table: "account",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_account_owner_OwnerId",
                table: "account",
                column: "OwnerId",
                principalTable: "owner",
                principalColumn: "OwnerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_account_owner_OwnerId",
                table: "account");

            migrationBuilder.DropPrimaryKey(
                name: "PK_account",
                table: "account");

            migrationBuilder.RenameTable(
                name: "account",
                newName: "Accounts");

            migrationBuilder.RenameIndex(
                name: "IX_account_OwnerId",
                table: "Accounts",
                newName: "IX_Accounts_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_owner_OwnerId",
                table: "Accounts",
                column: "OwnerId",
                principalTable: "owner",
                principalColumn: "OwnerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
