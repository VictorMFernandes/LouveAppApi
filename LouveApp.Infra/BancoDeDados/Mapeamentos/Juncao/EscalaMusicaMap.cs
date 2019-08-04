using LouveApp.Dominio.Entidades.Juncao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LouveApp.Infra.BancoDeDados.Mapeamentos.Juncao
{
    internal class EscalaMusicaMap : IEntityTypeConfiguration<EscalaMusica>
    {
        public const string Tabela = "tb_escala_musica";
        public void Configure(EntityTypeBuilder<EscalaMusica> builder)
        {
            builder.ToTable(Tabela);

            builder.HasKey(u => new { u.EscalaId, u.MusicaId });
        }
    }
}
