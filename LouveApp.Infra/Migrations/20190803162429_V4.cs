using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LouveApp.Infra.Migrations
{
    public partial class V4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_escala",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    MinisterioId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_escala", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_escala_tb_ministerio_MinisterioId",
                        column: x => x.MinisterioId,
                        principalTable: "tb_ministerio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_usuario_escala",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(nullable: false),
                    EscalaId = table.Column<string>(nullable: false),
                    InstrumentoId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_usuario_escala", x => new { x.UsuarioId, x.EscalaId });
                    table.ForeignKey(
                        name: "FK_tb_usuario_escala_tb_escala_EscalaId",
                        column: x => x.EscalaId,
                        principalTable: "tb_escala",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_usuario_escala_tb_instrumento_InstrumentoId",
                        column: x => x.InstrumentoId,
                        principalTable: "tb_instrumento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_usuario_escala_tb_usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "tb_usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_escala_MinisterioId",
                table: "tb_escala",
                column: "MinisterioId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_usuario_escala_EscalaId",
                table: "tb_usuario_escala",
                column: "EscalaId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_usuario_escala_InstrumentoId",
                table: "tb_usuario_escala",
                column: "InstrumentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_usuario_escala");

            migrationBuilder.DropTable(
                name: "tb_escala");
        }
    }
}
