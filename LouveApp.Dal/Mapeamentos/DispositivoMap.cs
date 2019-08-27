using LouveApp.Compartilhado.Padroes;
using LouveApp.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LouveApp.Dal.Mapeamentos
{
    internal class DispositivoMap : IEntityTypeConfiguration<Dispositivo>
    {
        public const string Tabela = "tb_dispositivo";
        public void Configure(EntityTypeBuilder<Dispositivo> builder)
        {
            IgnorarVos(builder);

            builder.ToTable(Tabela);

            builder.HasKey(d => d.Id);

            builder.HasIndex(d => d.Token).IsUnique();

            builder.Property(d => d.Token).IsRequired();

            builder.OwnsOne(d => d.Nome, n =>
            {
                n.Property(no => no.Texto).IsRequired().HasMaxLength(PadroesTamanho.MaxNome).HasColumnName("Nome");
            });

            builder.HasOne(d => d.Usuario).WithMany(u => u.Dispositivos).IsRequired();
        }

        /// <summary>
        /// Ignora todos os VOs da entidade, deve ser o primeiro elemento a ser mapeado
        /// </summary>
        private static void IgnorarVos(EntityTypeBuilder<Dispositivo> builder)
        {
            builder.Ignore(u => u.Nome);
        }
    }
}
