using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ausgaben_Management_App.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbKategorien",
                columns: table => new
                {
                    kategorieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titel = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbKategorien", x => x.kategorieId);
                });

            migrationBuilder.CreateTable(
                name: "TbTransaktionen",
                columns: table => new
                {
                    TransaktionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    kategorieId = table.Column<int>(type: "int", nullable: false),
                    Betrag = table.Column<int>(type: "int", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbTransaktionen", x => x.TransaktionId);
                    table.ForeignKey(
                        name: "FK_TbTransaktionen_TbKategorien_kategorieId",
                        column: x => x.kategorieId,
                        principalTable: "TbKategorien",
                        principalColumn: "kategorieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbTransaktionen_kategorieId",
                table: "TbTransaktionen",
                column: "kategorieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbTransaktionen");

            migrationBuilder.DropTable(
                name: "TbKategorien");
        }
    }
}
