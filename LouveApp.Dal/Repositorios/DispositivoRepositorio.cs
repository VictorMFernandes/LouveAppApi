using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using LouveApp.Dominio.Repositorios;
using LouveApp.Dominio.Sistema;
using LouveApp.Dal.Mapeamentos;
using Microsoft.Data.Sqlite;

namespace LouveApp.Dal.Repositorios
{
    public class DispositivoRepositorio : IDispositivoRepositorio
    {
        private readonly IDbConnection _conexao;

        public DispositivoRepositorio(IDbConnection conexao)
        {
            _conexao = conexao;
        }

        public async Task<IEnumerable<string>> PegarDispositivosTokensPorUsuarioId(List<string> usuariosIds)
        {
            var queryUsuariosIds = string.Empty;

            for (var i = 0; i < usuariosIds.Count; i++)
            {
                queryUsuariosIds += $"'{usuariosIds[i]}'";

                if (i + 1 < usuariosIds.Count)
                {
                    queryUsuariosIds += " OR UsuarioId = ";
                }
            }

            var query = $"SELECT Token FROM {DispositivoMap.Tabela} " +
                        $"WHERE UsuarioId = {queryUsuariosIds}";

            return await _conexao.QueryAsync<string>(query);
        }
    }
}
