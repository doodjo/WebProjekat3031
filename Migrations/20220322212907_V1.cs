using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProjekat3031.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Putnici",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    eMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Putnici", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Stanice",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stanice", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Destinacije",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StanicaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinacije", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Destinacije_Stanice_StanicaID",
                        column: x => x.StanicaID,
                        principalTable: "Stanice",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Putovanja",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DestinacijaID = table.Column<int>(type: "int", nullable: false),
                    PutnikID = table.Column<int>(type: "int", nullable: false),
                    tipSedista = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cena = table.Column<int>(type: "int", nullable: false),
                    Vreme = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Putovanja", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Putovanja_Destinacije_DestinacijaID",
                        column: x => x.DestinacijaID,
                        principalTable: "Destinacije",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Putovanja_Putnici_PutnikID",
                        column: x => x.PutnikID,
                        principalTable: "Putnici",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Termin",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vreme = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DestinacijaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Termin", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Termin_Destinacije_DestinacijaID",
                        column: x => x.DestinacijaID,
                        principalTable: "Destinacije",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Destinacije_StanicaID",
                table: "Destinacije",
                column: "StanicaID");

            migrationBuilder.CreateIndex(
                name: "IX_Putovanja_DestinacijaID",
                table: "Putovanja",
                column: "DestinacijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Putovanja_PutnikID",
                table: "Putovanja",
                column: "PutnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Termin_DestinacijaID",
                table: "Termin",
                column: "DestinacijaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Putovanja");

            migrationBuilder.DropTable(
                name: "Termin");

            migrationBuilder.DropTable(
                name: "Putnici");

            migrationBuilder.DropTable(
                name: "Destinacije");

            migrationBuilder.DropTable(
                name: "Stanice");
        }
    }
}
