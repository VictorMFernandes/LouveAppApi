using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LouveApp.Infra.Migrations
{
    public partial class V1 : Migration
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
                name: "tb_usuario_escala_instrumento",
                columns: table => new
                {
                    UsuarioEscalaId = table.Column<string>(nullable: false),
                    InstrumentoId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_usuario_escala_instrumento", x => new { x.UsuarioEscalaId, x.InstrumentoId });
                });

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
                name: "tb_musica",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Nome = table.Column<string>(maxLength: 60, nullable: false),
                    Letra = table.Column<string>(maxLength: 200, nullable: true),
                    Cifra = table.Column<string>(maxLength: 200, nullable: true),
                    Video = table.Column<string>(maxLength: 200, nullable: true),
                    MinisterioId = table.Column<string>(nullable: false),
                    Artista = table.Column<string>(maxLength: 60, nullable: false),
                    Tom = table.Column<string>(maxLength: 10, nullable: true),
                    Bpm = table.Column<int>(maxLength: 3, nullable: true),
                    Classificacao = table.Column<string>(maxLength: 60, nullable: true)
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
                name: "tb_dispositivo",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Token = table.Column<string>(nullable: false),
                    Nome = table.Column<string>(maxLength: 60, nullable: false),
                    UsuarioId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_dispositivo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_dispositivo_tb_usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "tb_usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "tb_usuario_escala",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(nullable: false),
                    EscalaId = table.Column<string>(nullable: false)
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
                        name: "FK_tb_usuario_escala_tb_usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "tb_usuario",
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
                name: "IX_tb_dispositivo_Token",
                table: "tb_dispositivo",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_dispositivo_UsuarioId",
                table: "tb_dispositivo",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_escala_MinisterioId",
                table: "tb_escala",
                column: "MinisterioId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_escala_musica_MusicaId",
                table: "tb_escala_musica",
                column: "MusicaId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_instrumento_Nome",
                table: "tb_instrumento",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_ministerio_LinkConvite",
                table: "tb_ministerio",
                column: "LinkConvite");

            migrationBuilder.CreateIndex(
                name: "IX_tb_musica_MinisterioId",
                table: "tb_musica",
                column: "MinisterioId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_usuario_Login",
                table: "tb_usuario",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_usuario_escala_EscalaId",
                table: "tb_usuario_escala",
                column: "EscalaId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_usuario_instrumento_InstrumentoId",
                table: "tb_usuario_instrumento",
                column: "InstrumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_usuario_ministerio_MinisterioId",
                table: "tb_usuario_ministerio",
                column: "MinisterioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_dispositivo");

            migrationBuilder.DropTable(
                name: "tb_escala_musica");

            migrationBuilder.DropTable(
                name: "tb_usuario_escala");

            migrationBuilder.DropTable(
                name: "tb_usuario_escala_instrumento");

            migrationBuilder.DropTable(
                name: "tb_usuario_instrumento");

            migrationBuilder.DropTable(
                name: "tb_usuario_ministerio");

            migrationBuilder.DropTable(
                name: "tb_musica");

            migrationBuilder.DropTable(
                name: "tb_escala");

            migrationBuilder.DropTable(
                name: "tb_instrumento");

            migrationBuilder.DropTable(
                name: "tb_usuario");

            migrationBuilder.DropTable(
                name: "tb_ministerio");
        }
    }
}
