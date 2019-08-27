using LouveApp.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LouveApp.Dal.Mapeamentos
{
    internal class EscalaMap : IEntityTypeConfiguration<Escala>
    {
        public const string Tabela = "tb_escala";
        public void Configure(EntityTypeBuilder<Escala> builder)
        {
            IgnorarVos(builder);

            builder.ToTable(Tabela);

            builder.HasKey(e => e.Id);
        }

        /// <summary>
        /// Ignora todos os VOs da entidade, deve ser o primeiro elemento a ser mapeado
        /// </summary>
        private static void IgnorarVos(EntityTypeBuilder<Escala> builder) { }
    }
}
