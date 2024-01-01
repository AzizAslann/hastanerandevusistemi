using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hastanerandevusistemi.Migrations.ConnectionStringClassMigrations
{
    public partial class RandevuAls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RandevuAl",
                columns: table => new
                {
                    randID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    randklinik = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    randhekim = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    randtarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    randsaat = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    randsahip = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RandevuAl", x => x.randID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RandevuAl");
        }
    }
}
