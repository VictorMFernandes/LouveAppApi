using LouveApp.Dominio.Entidades;
using LouveApp.Infra.BancoDeDados.Mapeamentos;
using FluentValidator;
using LouveApp.Infra.BancoDeDados.Mapeamentos.Juncao;
using Microsoft.EntityFrameworkCore;

namespace LouveApp.Infra.BancoDeDados.Contexto
{
    public class BancoContexto : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Ministerio> Ministerios { get; set; }
        public DbSet<Instrumento> Instrumentos { get; set; }
        public DbSet<Escala> Escalas { get; set; }

        public BancoContexto(DbContextOptions options) : base(options)
        {
            // TODO remover proxyCreation
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Notification>();
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new UsuarioMinisterioMap());
            modelBuilder.ApplyConfiguration(new UsuarioInstrumentoMap());
            modelBuilder.ApplyConfiguration(new UsuarioEscalaMap());

            modelBuilder.ApplyConfiguration(new MinisterioMap());
            modelBuilder.ApplyConfiguration(new InstrumentoMap());

            modelBuilder.ApplyConfiguration(new EscalaMap());
            modelBuilder.ApplyConfiguration(new EscalaMusicaMap());

            modelBuilder.ApplyConfiguration(new MusicaMap());
        }
    }
}
