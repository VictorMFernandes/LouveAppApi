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

                    query = $"SELECT COUNT(*) FROM {MusicaMap.Tabela} AS m " +
                            $"INNER JOIN {EscalaMusicaMap.Tabela} AS em ON em.MusicaId = m.Id "+
                            $"WHERE em.EscalaId = '{escala.Id}'";

                    escala.QtdMusicas = await conn.ExecuteScalarAsync<int>(query);
                }

                return resultado;
            }
        }

        public async Task<IEnumerable<PegarEscalaComandoResultado>> PegarPorUsuario(string usuarioId)
        {
            var query = $"SELECT * FROM {EscalaMap.Tabela} AS e " +
                        $"INNER JOIN {UsuarioEscalaMap.Tabela} AS ue ON ue.EscalaId = e.Id " +
                        $"INNER JOIN {UsuarioMinisterioMap.Tabela} AS um ON(um.MinisterioId = e.MinisterioId and um.UsuarioId = ue.UsuarioId) " +
                        $"INNER JOIN {MinisterioMap.Tabela} AS m ON m.Id = um.MinisterioId " +
                        $"WHERE ue.UsuarioId = @{nameof(usuarioId)} " +
                        "ORDER BY e.Data";

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
                            $"WHERE ue.EscalaId = '{escala.Id}'; " +
                            $"SELECT COUNT(*) FROM {MusicaMap.Tabela} AS m " +
                            $"INNER JOIN {EscalaMusicaMap.Tabela} AS em ON em.MusicaId = m.Id " +
                            $"WHERE em.EscalaId = '{escala.Id}'";

                    using (var res = await conn.QueryMultipleAsync(query))
                    {
                        escala.Usuarios = await res.ReadAsync<PegarUsuarioComandoResultado>();
                        escala.QtdMusicas = (await res.ReadAsync<long>()).FirstOrDefault();
                    }
                }

                return resultado;
            }
        }
    }
}
