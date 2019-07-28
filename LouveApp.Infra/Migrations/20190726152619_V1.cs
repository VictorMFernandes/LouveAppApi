using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LouveApp.Infra.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_ministerio",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Nome = table.Column<string>(maxLength: 60, nullable: false),
                    FotoUrl = table.Column<string>(maxLength: 160, nullable: true),
                    FotoIdPublico = table.Column<string>(maxLength: 25, nullable: true),
                    LinkConvite = table.Column<string>(nullable: true),
                    LinkConviteAtivado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_ministerio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_usuario",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Nome = table.Column<string>(maxLength: 60, nullable: false),
                    Email = table.Column<string>(maxLength: 160, nullable: false),
                    Login = table.Column<string>(maxLength: 100, nullable: false),
                    Senha = table.Column<string>(fixedLength: true, maxLength: 32, nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    FotoUrl = table.Column<string>(maxLength: 160, nullable: true),
                    FotoIdPublico = table.Column<string>(maxLength: 25, nullable: true),
                    DtCriacao = table.Column<DateTime>(nullable: false),
                    DtUltimaAtividade = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_usuario_ministerio",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(nullable: false),
                    MinisterioId = table.Column<string>(nullable: false),
                    Administrador = table.Column<bool>(nullable: false),
                    DtIngresso = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_usuario_ministerio", x => new { x.UsuarioId, x.MinisterioId });
                    table.ForeignKey(
                        name: "FK_tb_usuario_ministerio_tb_ministerio_MinisterioId",
                        column: x => x.MinisterioId,
                        principalTable: "tb_ministerio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_usuario_ministerio_tb_usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "tb_usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_ministerio_LinkConvite",
                table: "tb_ministerio",
                column: "LinkConvite");

            migrationBuilder.CreateIndex(
                name: "IX_tb_usuario_ministerio_MinisterioId",
                table: "tb_usuario_ministerio",
                column: "MinisterioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_usuario_ministerio");

            migrationBuilder.DropTable(
                name: "tb_ministerio");

            migrationBuilder.DropTable(
                name: "tb_usuario");
        }
    }
}
