using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ausgaben_Management_App.Migrations
{
    /// <inheritdoc />
    public partial class AddMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "TbTransaktionen",
                type: "nvarchar(75)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "TbTransaktionen");
        }
    }
}
