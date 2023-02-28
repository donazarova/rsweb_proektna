using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCTurizam.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Klient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImePrezime = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefon = table.Column<int>(type: "int", nullable: true),
                    SlikaOdPasos = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vodic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImePrezime = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefon = table.Column<int>(type: "int", nullable: true),
                    Iskustvo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vodic", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Destinacija",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Drzava = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kontinent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dalecina = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Temperatura = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CenaKarta = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SlikaOdDestinacija = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VodicId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinacija", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Destinacija_Vodic_VodicId",
                        column: x => x.VodicId,
                        principalTable: "Vodic",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Patuvanje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KlientId = table.Column<int>(type: "int", nullable: false),
                    DestinacijaId = table.Column<int>(type: "int", nullable: false),
                    DatumOd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DatumDo = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patuvanje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patuvanje_Destinacija_DestinacijaId",
                        column: x => x.DestinacijaId,
                        principalTable: "Destinacija",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Patuvanje_Klient_KlientId",
                        column: x => x.KlientId,
                        principalTable: "Klient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Destinacija_VodicId",
                table: "Destinacija",
                column: "VodicId");

            migrationBuilder.CreateIndex(
                name: "IX_Patuvanje_DestinacijaId",
                table: "Patuvanje",
                column: "DestinacijaId");

            migrationBuilder.CreateIndex(
                name: "IX_Patuvanje_KlientId",
                table: "Patuvanje",
                column: "KlientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patuvanje");

            migrationBuilder.DropTable(
                name: "Destinacija");

            migrationBuilder.DropTable(
                name: "Klient");

            migrationBuilder.DropTable(
                name: "Vodic");
        }
    }
}
