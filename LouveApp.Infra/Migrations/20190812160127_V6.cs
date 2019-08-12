using Microsoft.EntityFrameworkCore.Migrations;

namespace LouveApp.Infra.Migrations
{
    public partial class V6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Bpm",
                table: "tb_musica",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Classificacao",
                table: "tb_musica",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tom",
                table: "tb_musica",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Artista",
                table: "tb_musica",
                maxLength: 60,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bpm",
                table: "tb_musica");

            migrationBuilder.DropColumn(
                name: "Classificacao",
                table: "tb_musica");

            migrationBuilder.DropColumn(
                name: "Tom",
                table: "tb_musica");

            migrationBuilder.DropColumn(
                name: "Artista",
                table: "tb_musica");
        }
    }
}
