using LouveApp.Dominio.Entidades.Juncao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LouveApp.Dal.Mapeamentos.Juncao
{
    internal class UsuarioInstrumentoMap : IEntityTypeConfiguration<UsuarioInstrumento>
    {
        public const string Tabela = "tb_usuario_instrumento";
        public void Configure(EntityTypeBuilder<UsuarioInstrumento> builder)
        {
            builder.ToTable(Tabela);

            builder.HasKey(u => new { u.UsuarioId, u.InstrumentoId });
        }
    }
}
