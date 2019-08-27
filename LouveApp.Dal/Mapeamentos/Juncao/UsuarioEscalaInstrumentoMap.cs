using LouveApp.Dominio.Entidades.Juncao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LouveApp.Dal.Mapeamentos.Juncao
{
    internal class UsuarioEscalaInstrumentoMap : IEntityTypeConfiguration<UsuarioEscalaInstrumento>
    {
        public const string Tabela = "tb_usuario_escala_instrumento";
        public void Configure(EntityTypeBuilder<UsuarioEscalaInstrumento> builder)
        {
            builder.ToTable(Tabela);

            builder.HasKey(u => new { u.UsuarioEscalaId, u.InstrumentoId });
        }
    }
}
