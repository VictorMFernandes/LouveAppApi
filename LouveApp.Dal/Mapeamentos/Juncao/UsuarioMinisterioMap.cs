using LouveApp.Dominio.Entidades.Juncao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LouveApp.Dal.Mapeamentos.Juncao
{
    internal class UsuarioMinisterioMap : IEntityTypeConfiguration<UsuarioMinisterio>
    {
        public const string Tabela = "tb_usuario_ministerio";
        public void Configure(EntityTypeBuilder<UsuarioMinisterio> builder)
        {
            builder.ToTable(Tabela);

            builder.HasKey(u => new { u.UsuarioId, u.MinisterioId });

            builder.Property(u => u.Administrador);
        }
    }
}
