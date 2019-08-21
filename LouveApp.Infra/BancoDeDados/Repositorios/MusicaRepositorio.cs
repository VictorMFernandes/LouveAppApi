using LouveApp.Dominio.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using LouveApp.Dominio.Sistema;
using LouveApp.Infra.BancoDeDados.Mapeamentos;
using Microsoft.Data.Sqlite;
using LouveApp.Dominio.Comandos.MusicaComandos.Saidas;
using LouveApp.Dominio.Entidades;
using LouveApp.Infra.BancoDeDados.Contexto;
using Microsoft.EntityFrameworkCore;
using LouveApp.Infra.BancoDeDados.Mapeamentos.Juncao;
using System.Linq;

namespace LouveApp.Infra.BancoDeDados.Repositorios
{
    public class MusicaRepositorio : IMusicaRepositorio
    {
        private readonly BancoContexto _contexto;

        public MusicaRepositorio(BancoContexto contexto)
        {
            _contexto = contexto;
        }

        public void Atualizar(Musica musica)
        {
            _contexto.Entry(musica).State = EntityState.Modified;
        }

        public async Task<Musica> PegarPorId(string musicaId)
        {
            return await _contexto
                        .Musicas
                        .FirstOrDefaultAsync(x => x.Id == musicaId);
        }

        public async Task<IEnumerable<PegarMusicaComandoResultado>> PegarPorMinisterio(string ministerioId)
        {
            var query = $"SELECT Id, Nome, Artista, Letra, Cifra, Video FROM {MusicaMap.Tabela} " +
                        $"WHERE MinisterioId = @{nameof(ministerioId)}";

            using (var conn = new SqliteConnection(Configuracoes.ConnString))
            {
                conn.Open();
                return await conn.QueryAsync<PegarMusicaComandoResultado>(query, new { ministerioId });
            }
        }

        public async Task<bool> UsuarioEhAdministrador(string usuarioId, string musicaId)
        {
            var query = $"SELECT 1 FROM {UsuarioMinisterioMap.Tabela} AS um " +
                        $"JOIN {MusicaMap.Tabela} AS m ON m.MinisterioId = um.MinisterioId " +
                        $"WHERE um.UsuarioId = @{nameof(usuarioId)} " +
                        $"AND m.Id = @{nameof(musicaId)} " +
                        $"AND um.Administrador = true";

            using (var conn = new SqliteConnection(Configuracoes.ConnString))
            {
                conn.Open();
                return (await conn.QueryAsync<object>(query, new { usuarioId, musicaId })).Any();
            }
        }

        public async Task<IEnumerable<PegarMusicaComandoResultado>> PegarPorNomeEArtista(string ministerioId, string nome, string artista)
        {
            var query = $"SELECT Id, Nome, Artista, Letra, Cifra, Video FROM {MusicaMap.Tabela} " +
                        $"WHERE MinisterioId = @{nameof(ministerioId)} " +
                        $"AND Nome = @{nameof(nome)} AND Artista = @{nameof(artista)}";

            using (var conn = new SqliteConnection(Configuracoes.ConnString))
            {
                conn.Open();
                return await conn.QueryAsync<PegarMusicaComandoResultado>(query, new { ministerioId, nome, artista });
            }
        }
    }
}
