using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProjekat3031.Migrations
{
    public partial class V32978 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Termin_Destinacije_DestinacijaID",
                table: "Termin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Termin",
                table: "Termin");

            migrationBuilder.RenameTable(
                name: "Termin",
                newName: "Termini");

            migrationBuilder.RenameIndex(
                name: "IX_Termin_DestinacijaID",
                table: "Termini",
                newName: "IX_Termini_DestinacijaID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Termini",
                table: "Termini",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Termini_Destinacije_DestinacijaID",
                table: "Termini",
                column: "DestinacijaID",
                principalTable: "Destinacije",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Termini_Destinacije_DestinacijaID",
                table: "Termini");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Termini",
                table: "Termini");

            migrationBuilder.RenameTable(
                name: "Termini",
                newName: "Termin");

            migrationBuilder.RenameIndex(
                name: "IX_Termini_DestinacijaID",
                table: "Termin",
                newName: "IX_Termin_DestinacijaID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Termin",
                table: "Termin",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Termin_Destinacije_DestinacijaID",
                table: "Termin",
                column: "DestinacijaID",
                principalTable: "Destinacije",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
