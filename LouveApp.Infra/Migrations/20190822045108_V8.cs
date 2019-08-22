using Microsoft.EntityFrameworkCore.Migrations;

namespace LouveApp.Infra.Migrations
{
    public partial class V8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_tb_dispositivo_Token",
                table: "tb_dispositivo",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_dispositivo_UsuarioId",
                table: "tb_dispositivo",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_dispositivo");
        }
    }
}
