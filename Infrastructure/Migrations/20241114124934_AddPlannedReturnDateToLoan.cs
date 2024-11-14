using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddPlannedReturnDateToLoan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReturnDate",
                table: "Loans",
                newName: "ActualReturnDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "PlannedReturnDate",
                table: "Loans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlannedReturnDate",
                table: "Loans");

            migrationBuilder.RenameColumn(
                name: "ActualReturnDate",
                table: "Loans",
                newName: "ReturnDate");
        }
    }
}
