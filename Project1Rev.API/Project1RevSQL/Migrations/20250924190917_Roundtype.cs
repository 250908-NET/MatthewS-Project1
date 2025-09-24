using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project1RevSQL.Migrations
{
    /// <inheritdoc />
    public partial class Roundtype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rounds",
                table: "Tournaments");

            migrationBuilder.AddColumn<string>(
                name: "RoundType",
                table: "Tournaments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoundType",
                table: "Tournaments");

            migrationBuilder.AddColumn<string>(
                name: "Rounds",
                table: "Tournaments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
