using LouveApp.Dominio.Repositorios;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using LouveApp.Dominio.Sistema;
using LouveApp.Infra.BancoDeDados.Mapeamentos;
using Microsoft.Data.Sqlite;
using LouveApp.Dominio.Comandos.UsuarioComandos.Saidas;
using LouveApp.Infra.BancoDeDados.Mapeamentos.Juncao;
using LouveApp.Dominio.Comandos.EscalaComandos.Saidas;
using LouveApp.Dominio.Comandos.MusicaComandos.Saidas;
using LouveApp.Dominio.Comandos.MinisterioComandos.Saidas;

namespace LouveApp.Infra.BancoDeDados.Repositorios
{
    public class EscalaRepositorio : IEscalaRepositorio
    {
        public async Task<IEnumerable<PegarEscalaComandoResultado>> PegarPorMinisterio(string ministerioId)
        {
            var query = $"SELECT Id, Data FROM {EscalaMap.Tabela} "+
                        $"WHERE MinisterioId = @{nameof(ministerioId)}";

            using (var conn = new SqliteConnection(Configuracoes.ConnString))
            {
                conn.Open();
                var resultado = (await conn.QueryAsync<PegarEscalaComandoResultado>(query, new { ministerioId })).ToList();

                foreach (var escala in resultado)
                {
                    query = $"SELECT u.Id, u.Nome, u.Email, u.FotoUrl, u.DtCriacao FROM {UsuarioMap.Tabela} AS u "+
                            $"INNER JOIN {UsuarioEscalaMap.Tabela} AS ue ON ue.UsuarioId = u.Id " +
                            $"WHERE ue.EscalaId = '{escala.Id}'";

                    escala.Usuarios = await conn.QueryAsync<PegarUsuarioComandoResultado>(query);

                    query = $"SELECT m.Id, m.Nome, m.Referencia FROM {MusicaMap.Tabela} AS m " +
                            $"INNER JOIN {EscalaMusicaMap.Tabela} AS em ON em.MusicaId = m.Id "+
                            $"WHERE em.EscalaId = '{escala.Id}'";

                    escala.Musicas = await conn.QueryAsync<PegarMusicaComandoResultado>(query);
                }

                return resultado;
            }
        }

        public async Task<IEnumerable<PegarEscalaComandoResultado>> PegarPorUsuario(string usuarioId)
        {
            var query = $"SELECT e.Id, e.Data, m.Id, m.Nome, um.Administrador FROM {EscalaMap.Tabela} AS e " +
                        $"INNER JOIN {UsuarioMinisterioMap.Tabela} AS um ON um.MinisterioId = e.MinisterioId " +
                        $"INNER JOIN {MinisterioMap.Tabela} AS m ON m.Id = um.MinisterioId " +
                        $"WHERE um.UsuarioId = @{nameof(usuarioId)}";

            using (var conn = new SqliteConnection(Configuracoes.ConnString))
            {
                conn.Open();
                var resultado = (await conn.QueryAsync<PegarEscalaComandoResultado
                    , PegarMinisterioComandoResultado
                    , PegarEscalaComandoResultado>(
                    query
                    , (e, mi) =>
                    {
                        e.Ministerio = mi;

                        return e;
                    }
                    , splitOn: "Id",  param:new {usuarioId})).ToList();

                foreach (var escala in resultado)
                {
                    query = $"SELECT u.Id, u.Nome, u.Email, u.FotoUrl, u.DtCriacao FROM {UsuarioMap.Tabela} AS u " +
                            $"INNER JOIN {UsuarioEscalaMap.Tabela} AS ue ON ue.UsuarioId = u.Id " +
                            $"WHERE ue.EscalaId = '{escala.Id}'";

                    escala.Usuarios = await conn.QueryAsync<PegarUsuarioComandoResultado>(query);

                    query = $"SELECT m.Id, m.Nome, m.Referencia FROM {MusicaMap.Tabela} AS m " +
                            $"INNER JOIN {EscalaMusicaMap.Tabela} AS em ON em.MusicaId = m.Id " +
                            $"WHERE em.EscalaId = '{escala.Id}'";

                    escala.Musicas = await conn.QueryAsync<PegarMusicaComandoResultado>(query);
                }

                return resultado;
            }
        }
    }
}
