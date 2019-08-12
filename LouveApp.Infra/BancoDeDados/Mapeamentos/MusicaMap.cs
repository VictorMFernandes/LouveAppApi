using LouveApp.Compartilhado.Padroes;
using LouveApp.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LouveApp.Infra.BancoDeDados.Mapeamentos
{
    internal class MusicaMap : IEntityTypeConfiguration<Musica>
    {
        public const string Tabela = "tb_musica";
        public void Configure(EntityTypeBuilder<Musica> builder)
        {
            IgnorarVos(builder);

            builder.ToTable(Tabela);

            builder.HasKey(m => m.Id);

            builder.OwnsOne(m => m.Nome, n =>
            {
                n.Property(no => no.Texto).IsRequired().HasMaxLength(PadroesTamanho.MaxNome).HasColumnName("Nome");
            });

            builder.OwnsOne(m => m.Referencia, r =>
            {
                r.Property(re => re.Url).HasMaxLength(PadroesTamanho.MaxUrl).HasColumnName("Referencia");
            });

            builder.OwnsOne(m => m.Artista, a =>
            {
                a.Property(ar => ar.Texto).IsRequired().HasMaxLength(PadroesTamanho.MaxNome).HasColumnName(nameof(Musica.Artista));
            });

            builder.Property(m => m.Tom).HasMaxLength(PadroesTamanho.MaxTom);
            builder.Property(m => m.Bpm).HasMaxLength(PadroesTamanho.MaxBpm);
            builder.Property(m => m.Classificacao).HasMaxLength(PadroesTamanho.MaxMusicaClassificacao);

            builder.HasOne(m => m.Ministerio)
                    .WithMany(m => m.Musicas)
                    .IsRequired();
        }

        /// <summary>
        /// Ignora todos os VOs da entidade, deve ser o primeiro elemento a ser mapeado
        /// </summary>
        private static void IgnorarVos(EntityTypeBuilder<Musica> builder)
        {
            builder.Ignore(m => m.Nome);
            builder.Ignore(m => m.Referencia);
        }
    }
}
