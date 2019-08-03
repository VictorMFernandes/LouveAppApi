using LouveApp.Dominio.Entidades;
using LouveApp.Dominio.Repositorios;
using LouveApp.Infra.BancoDeDados.Contexto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using LouveApp.Dominio.Comandos.MinisterioComandos.Saidas;
using LouveApp.Dominio.Sistema;
using LouveApp.Infra.BancoDeDados.Mapeamentos;
using LouveApp.Infra.BancoDeDados.Mapeamentos.Juncao;
using Microsoft.Data.Sqlite;

namespace LouveApp.Infra.BancoDeDados.Repositorios
{
    public class MinisterioRepositorio : IMinisterioRepositorio
    {
        private readonly BancoContexto _contexto;

        public MinisterioRepositorio(BancoContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<Ministerio> PegarPorId(string id)
        {
            return await _contexto
                        .Ministerios
                        .Include(m => m.Usuarios)
                        .ThenInclude(um => um.Usuario)
                        .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Ministerio> PegarPorLinkConvite(string linkConvite)
        {
            return await _contexto
                .Ministerios
                .Include(m => m.Usuarios)
                .FirstOrDefaultAsync(x => x.LinkConvite == linkConvite || x.LinkConviteAtivado);
        }

        public async Task<IEnumerable<PegarMinisteriosComandoResultado>> PegarPorUsuario(string id)
        {
            var query = $"SELECT m.Id, m.Nome, um.Administrador FROM {MinisterioMap.Tabela} AS m " +
                        $"INNER JOIN {UsuarioMinisterioMap.Tabela} AS um ON m.Id = um.MinisterioId " +
                        $"WHERE um.UsuarioId = @{nameof(id)}";

            using (var conn = new SqliteConnection(Configuracoes.ConnString))
            {
                conn.Open();
                return await conn.QueryAsync<PegarMinisteriosComandoResultado>(query, new { id });
            }
        }

        public void Criar(Ministerio ministerio)
        {
            _contexto.Ministerios.Add(ministerio);
        }

        public void Atualizar(Ministerio ministerio)
        {
            _contexto.Entry(ministerio).State = EntityState.Modified;
        }

        public async Task Remover(string id)
        {
            var query = $"DELETE FROM {MinisterioMap.Tabela} " +
                        $"WHERE Id = @{nameof(id)}";

            using (var conn = new SqliteConnection(Configuracoes.ConnString))
            {
                conn.Open();
                await conn.ExecuteAsync(query, new { id });
            }
        }

        public async Task<bool> EAdministrador(string usuarioId, string ministerioId)
        {
            var query = $"SELECT Administrador FROM {UsuarioMinisterioMap.Tabela} " +
                        $"WHERE UsuarioId = @{nameof(usuarioId)} AND MinisterioId = @{nameof(ministerioId)}";

            using (var conn = new SqliteConnection(Configuracoes.ConnString))
            {
                conn.Open();
                return await conn.ExecuteScalarAsync<bool>(query, new { usuarioId, ministerioId });
            }
        }

        public async Task<int> Contar()
        {
            using (var conn = new SqliteConnection(Configuracoes.ConnString))
            {
                conn.Open();
                return await conn.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM {MinisterioMap.Tabela}");
            }
        }
    }
}
