using LouveApp.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LouveApp.Infra.BancoDeDados.Mapeamentos
{
    internal class EscalaMap : IEntityTypeConfiguration<Escala>
    {
        public const string Tabela = "tb_escala";
        public void Configure(EntityTypeBuilder<Escala> builder)
        {
            IgnorarVos(builder);

            builder.ToTable(Tabela);

            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Ministerio)
                    .WithMany(m => m.Escalas)
                    .IsRequired();
        }

        /// <summary>
        /// Ignora todos os VOs da entidade, deve ser o primeiro elemento a ser mapeado
        /// </summary>
        private static void IgnorarVos(EntityTypeBuilder<Escala> builder) { }
    }
}
