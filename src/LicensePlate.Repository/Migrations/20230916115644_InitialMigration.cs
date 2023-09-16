using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LicensePlate.Repository.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LicensePlates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Chars = table.Column<string>(maxLength: 4, nullable: false),
                    Numbers = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicensePlates", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LicensePlates");
        }
    }
}
