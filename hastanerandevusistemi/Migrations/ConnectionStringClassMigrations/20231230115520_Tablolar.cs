using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hastanerandevusistemi.Migrations.ConnectionStringClassMigrations
{
    public partial class Tablolar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doktorlar",
                columns: table => new
                {
                    DoktorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    klinik = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    isim = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    durum = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doktorlar", x => x.DoktorID);
                });

            migrationBuilder.CreateTable(
                name: "Randevular",
                columns: table => new
                {
                    randID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    randklinik = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    randhekim = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    randtarih = table.Column<DateTime>(type: "datetime2", maxLength: 200, nullable: false),
                    randsahip = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Randevular", x => x.randID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doktorlar");

            migrationBuilder.DropTable(
                name: "Randevular");
        }
    }
}
