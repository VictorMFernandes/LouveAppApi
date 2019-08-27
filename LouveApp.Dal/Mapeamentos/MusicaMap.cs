using LouveApp.Compartilhado.Padroes;
using LouveApp.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LouveApp.Dal.Mapeamentos
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

            builder.OwnsOne(m => m.Letra, r =>
            {
                r.Property(re => re.Url).HasMaxLength(PadroesTamanho.MaxUrl).HasColumnName("Letra");
            });

            builder.OwnsOne(m => m.Cifra, r =>
            {
                r.Property(re => re.Url).HasMaxLength(PadroesTamanho.MaxUrl).HasColumnName("Cifra");
            });

            builder.OwnsOne(m => m.Video, r =>
            {
                r.Property(re => re.Url).HasMaxLength(PadroesTamanho.MaxUrl).HasColumnName("Video");
            });

            builder.OwnsOne(m => m.Artista, a =>
            {
                a.Property(ar => ar.Texto).IsRequired().HasMaxLength(PadroesTamanho.MaxNome).HasColumnName(nameof(Musica.Artista));
            });

            builder.Property(m => m.Tom).HasMaxLength(PadroesTamanho.MaxTom);
            builder.Property(m => m.Bpm).HasMaxLength(PadroesTamanho.MaxBpm);
            builder.Property(m => m.Classificacao).HasMaxLength(PadroesTamanho.MaxMusicaClassificacao);
        }

        /// <summary>
        /// Ignora todos os VOs da entidade, deve ser o primeiro elemento a ser mapeado
        /// </summary>
        private static void IgnorarVos(EntityTypeBuilder<Musica> builder)
        {
            builder.Ignore(m => m.Nome);
            builder.Ignore(m => m.Letra);
            builder.Ignore(m => m.Cifra);
            builder.Ignore(m => m.Video);
        }
    }
}
