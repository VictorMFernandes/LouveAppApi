using Dapper;
using LouveApp.Dal.Contexto;
using LouveApp.Dal.Mapeamentos;
using LouveApp.Dal.Mapeamentos.Juncao;
using LouveApp.Dominio.Comandos.MusicaComandos.Saidas;
using LouveApp.Dominio.Entidades;
using LouveApp.Dominio.Repositorios;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LouveApp.Dal.Repositorios
{
    public class MusicaRepositorio : IMusicaRepositorio
    {
        private readonly BancoContexto _contexto;
        private readonly IDbConnection _conexao;

        public MusicaRepositorio(BancoContexto contexto, IDbConnection conexao)
        {
            _contexto = contexto;
            _conexao = conexao;
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

            return await _conexao.QueryAsync<PegarMusicaComandoResultado>(query, new { ministerioId });
        }

        public async Task<bool> UsuarioEhAdministrador(string usuarioId, string musicaId)
        {
            var query = $"SELECT 1 FROM {UsuarioMinisterioMap.Tabela} AS um " +
                        $"JOIN {MusicaMap.Tabela} AS m ON m.MinisterioId = um.MinisterioId " +
                        $"WHERE um.UsuarioId = @{nameof(usuarioId)} " +
                        $"AND m.Id = @{nameof(musicaId)} " +
                        $"AND um.Administrador = true";

            return (await _conexao.QueryAsync<object>(query, new { usuarioId, musicaId })).Any();
        }

        public async Task<IEnumerable<PegarMusicaComandoResultado>> PegarPorNomeEArtista(string ministerioId, string nome, string artista)
        {
            var query = $"SELECT Id, Nome, Artista, Letra, Cifra, Video FROM {MusicaMap.Tabela} " +
                        $"WHERE MinisterioId = @{nameof(ministerioId)} " +
                        $"AND Nome = @{nameof(nome)} AND Artista = @{nameof(artista)}";

            return await _conexao.QueryAsync<PegarMusicaComandoResultado>(query, new { ministerioId, nome, artista });
        }
    }
}
