using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Books_BookFK",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Users_UserFK",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_BookFK",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_UserFK",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "BookFK",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "UserFK",
                table: "Loans");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_BookId",
                table: "Loans",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_UserId",
                table: "Loans",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Books_BookId",
                table: "Loans",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Users_UserId",
                table: "Loans",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Books_BookId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Users_UserId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_BookId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_UserId",
                table: "Loans");

            migrationBuilder.AddColumn<int>(
                name: "BookFK",
                table: "Loans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserFK",
                table: "Loans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Loans_BookFK",
                table: "Loans",
                column: "BookFK");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_UserFK",
                table: "Loans",
                column: "UserFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Books_BookFK",
                table: "Loans",
                column: "BookFK",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Users_UserFK",
                table: "Loans",
                column: "UserFK",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
