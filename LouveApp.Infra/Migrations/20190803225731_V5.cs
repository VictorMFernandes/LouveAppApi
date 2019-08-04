using Microsoft.EntityFrameworkCore.Migrations;

namespace LouveApp.Infra.Migrations
{
    public partial class V5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_musica",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Nome = table.Column<string>(maxLength: 60, nullable: false),
                    Referencia = table.Column<string>(maxLength: 200, nullable: true),
                    MinisterioId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_musica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_musica_tb_ministerio_MinisterioId",
                        column: x => x.MinisterioId,
                        principalTable: "tb_ministerio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_escala_musica",
                columns: table => new
                {
                    EscalaId = table.Column<string>(nullable: false),
                    MusicaId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_escala_musica", x => new { x.EscalaId, x.MusicaId });
                    table.ForeignKey(
                        name: "FK_tb_escala_musica_tb_escala_EscalaId",
                        column: x => x.EscalaId,
                        principalTable: "tb_escala",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_escala_musica_tb_musica_MusicaId",
                        column: x => x.MusicaId,
                        principalTable: "tb_musica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_escala_musica_MusicaId",
                table: "tb_escala_musica",
                column: "MusicaId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_musica_MinisterioId",
                table: "tb_musica",
                column: "MinisterioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_escala_musica");

            migrationBuilder.DropTable(
                name: "tb_musica");
        }
    }
}
