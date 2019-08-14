using LouveApp.Dominio.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using LouveApp.Dominio.Sistema;
using LouveApp.Infra.BancoDeDados.Mapeamentos;
using Microsoft.Data.Sqlite;
using LouveApp.Dominio.Comandos.MusicaComandos.Saidas;

namespace LouveApp.Infra.BancoDeDados.Repositorios
{
    public class MusicaRepositorio : IMusicaRepositorio
    {
        public async Task<IEnumerable<PegarMusicaComandoResultado>> PegarPorMinisterio(string ministerioId)
        {
            var query = $"SELECT Id, Nome, Letra, Cifra, Video FROM {MusicaMap.Tabela} " +
                        $"WHERE MinisterioId = @{nameof(ministerioId)}";

            using (var conn = new SqliteConnection(Configuracoes.ConnString))
            {
                conn.Open();
                return await conn.QueryAsync<PegarMusicaComandoResultado>(query, new { ministerioId });
            }
        }
    }
}
