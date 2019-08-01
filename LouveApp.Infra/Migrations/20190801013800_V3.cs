using Microsoft.EntityFrameworkCore.Migrations;

namespace LouveApp.Infra.Migrations
{
    public partial class V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_instrumento",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Nome = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_instrumento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_usuario_instrumento",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(nullable: false),
                    InstrumentoId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_usuario_instrumento", x => new { x.UsuarioId, x.InstrumentoId });
                    table.ForeignKey(
                        name: "FK_tb_usuario_instrumento_tb_instrumento_InstrumentoId",
                        column: x => x.InstrumentoId,
                        principalTable: "tb_instrumento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_usuario_instrumento_tb_usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "tb_usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_instrumento_Nome",
                table: "tb_instrumento",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_usuario_instrumento_InstrumentoId",
                table: "tb_usuario_instrumento",
                column: "InstrumentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_usuario_instrumento");

            migrationBuilder.DropTable(
                name: "tb_instrumento");
        }
    }
}
