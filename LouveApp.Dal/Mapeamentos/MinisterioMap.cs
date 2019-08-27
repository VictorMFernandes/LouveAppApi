using LouveApp.Compartilhado.Padroes;
using LouveApp.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LouveApp.Dal.Mapeamentos
{
    internal class MinisterioMap : IEntityTypeConfiguration<Ministerio>
    {
        public const string Tabela = "tb_ministerio";
        public void Configure(EntityTypeBuilder<Ministerio> builder)
        {
            IgnorarVos(builder);

            builder.ToTable(Tabela);

            builder.HasKey(m => m.Id);

            builder.HasIndex(m => m.LinkConvite);

            builder.OwnsOne(m => m.Nome, n =>
            {
                n.Property(no => no.Texto).IsRequired().HasMaxLength(PadroesTamanho.MaxNome).HasColumnName("Nome");
            });

            builder.OwnsOne(m => m.Foto, f =>
            {
                f.Property(fo => fo.IdPublico).HasMaxLength(PadroesTamanho.MaxFotoIdPublico).HasColumnName("FotoIdPublico");
                f.Property(fo => fo.Url).HasMaxLength(PadroesTamanho.MaxFotoUrl).HasColumnName("FotoUrl");
            });
        }

        /// <summary>
        /// Ignora todos os VOs da entidade, deve ser o primeiro elemento a ser mapeado
        /// </summary>
        private static void IgnorarVos(EntityTypeBuilder<Ministerio> builder)
        {
            builder.Ignore(m => m.Nome);
            builder.Ignore(m => m.Foto);
        }
    }
}
