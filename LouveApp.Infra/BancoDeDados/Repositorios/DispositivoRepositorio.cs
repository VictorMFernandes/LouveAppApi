using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using LouveApp.Dominio.Repositorios;
using LouveApp.Dominio.Sistema;
using LouveApp.Infra.BancoDeDados.Mapeamentos;
using Microsoft.Data.Sqlite;

namespace LouveApp.Infra.BancoDeDados.Repositorios
{
    public class DispositivoRepositorio:IDispositivoRepositorio
    {
        public async Task<IEnumerable<string>> PegarDispositivosTokensPorUsuarioId(List<string> usuariosIds)
        {
            var queryUsuariosIds = string.Empty;

            for (var i = 0; i < usuariosIds.Count; i++)
            {
                queryUsuariosIds += $"'{usuariosIds[i]}'";

                if (i + 1 < usuariosIds.Count)
                {
                    queryUsuariosIds += " OR ";
                }
            }

            var query = $"SELECT Token FROM {DispositivoMap.Tabela} " +
                        $"WHERE UsuarioId = {queryUsuariosIds}";

            using (var conn = new SqliteConnection(Configuracoes.ConnString))
            {
                conn.Open();
                return await conn.QueryAsync<string>(query);
            }
        }
    }
}
