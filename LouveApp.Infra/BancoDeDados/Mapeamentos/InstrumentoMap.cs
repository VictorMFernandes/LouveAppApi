using LouveApp.Compartilhado.Padroes;
using LouveApp.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LouveApp.Infra.BancoDeDados.Mapeamentos
{
    internal class InstrumentoMap : IEntityTypeConfiguration<Instrumento>
    {
        public const string Tabela = "tb_instrumento";
        public void Configure(EntityTypeBuilder<Instrumento> builder)
        {
            IgnorarVos(builder);

            builder.ToTable(Tabela);

            builder.HasKey(m => m.Id);

            builder.OwnsOne(m => m.Nome, n =>
            {
                n.HasIndex(no => no.Texto).IsUnique();
                n.Property(no => no.Texto).IsRequired().HasMaxLength(PadroesTamanho.MaxNome).HasColumnName("Nome");
            });
        }

        /// <summary>
        /// Ignora todos os VOs da entidade, deve ser o primeiro elemento a ser mapeado
        /// </summary>
        private static void IgnorarVos(EntityTypeBuilder<Instrumento> builder)
        {
            builder.Ignore(m => m.Nome);
        }
    }
}
