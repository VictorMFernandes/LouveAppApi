using LouveApp.Dominio.Entidades.Juncao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LouveApp.Infra.BancoDeDados.Mapeamentos.Juncao
{
    internal class UsuarioEscalaMap : IEntityTypeConfiguration<UsuarioEscala>
    {
        public const string Tabela = "tb_usuario_escala";
        public void Configure(EntityTypeBuilder<UsuarioEscala> builder)
        {
            builder.ToTable(Tabela);

            builder.HasKey(u => new { u.UsuarioId, u.EscalaId });
        }
    }
}
