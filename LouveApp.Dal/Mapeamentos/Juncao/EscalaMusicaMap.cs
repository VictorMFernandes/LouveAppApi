using LouveApp.Dominio.Entidades.Juncao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LouveApp.Dal.Mapeamentos.Juncao
{
    internal class EscalaMusicaMap : IEntityTypeConfiguration<EscalaMusica>
    {
        public const string Tabela = "tb_escala_musica";
        public void Configure(EntityTypeBuilder<EscalaMusica> builder)
        {
            builder.ToTable(Tabela);

            builder.HasKey(u => new { u.EscalaId, u.MusicaId }).ForSqlServerIsClustered(false);

            builder.HasOne(em => em.Escala).WithMany(e => e.Musicas);
            builder.HasOne(em => em.Musica).WithMany(e => e.Escalas);
        }
    }
}
