using Dapper;
using LouveApp.Dal.Contexto;
using LouveApp.Dal.Mapeamentos;
using LouveApp.Dominio.Comandos.MusicaComandos.Saidas;
using LouveApp.Dominio.Entidades;
using LouveApp.Dominio.Repositorios;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
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

        public async Task<IEnumerable<PegarMusicaComandoResultado>> PegarPorMinisterio(string ministerioId)
        {
            var query = $"SELECT Id, Nome, Artista, Letra, Cifra, Video FROM {MusicaMap.Tabela} " +
                        $"WHERE MinisterioId = @{nameof(ministerioId)}";

            return await _conexao.QueryAsync<PegarMusicaComandoResultado>(query, new { ministerioId });
        }

        public async Task<Musica> PegarPorIdComUsuariosDoMinisterio(string musicaId)
        {
           return await _contexto
                        .Musicas
                        .Include(m => m.Ministerio)
                        .ThenInclude(m => m.Usuarios)
                        .FirstOrDefaultAsync(x => x.Id == musicaId);
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
