using Microsoft.EntityFrameworkCore.Migrations;

namespace LouveApp.Infra.Migrations
{
    public partial class V7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Referencia",
                table: "tb_musica",
                newName: "Video");

            migrationBuilder.AddColumn<string>(
                name: "Cifra",
                table: "tb_musica",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Letra",
                table: "tb_musica",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cifra",
                table: "tb_musica");

            migrationBuilder.DropColumn(
                name: "Letra",
                table: "tb_musica");

            migrationBuilder.RenameColumn(
                name: "Video",
                table: "tb_musica",
                newName: "Referencia");
        }
    }
}
