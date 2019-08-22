using System.Linq;
using LouveApp.Dominio.Entidades;
using LouveApp.Dominio.Repositorios;
using LouveApp.Infra.BancoDeDados.Contexto;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Dapper;
using LouveApp.Dominio.Comandos.AutenticacaoComandos.Saidas;
using LouveApp.Dominio.Comandos.InstrumentoComandos.Saidas;
using LouveApp.Dominio.Comandos.MinisterioComandos.Saidas;
using LouveApp.Dominio.Comandos.UsuarioComandos.Saidas;
using LouveApp.Dominio.Sistema;
using LouveApp.Infra.BancoDeDados.Mapeamentos;
using LouveApp.Infra.BancoDeDados.Mapeamentos.Juncao;
using Microsoft.Data.Sqlite;
using System;

namespace LouveApp.Infra.BancoDeDados.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContexto _contexto;

        public UsuarioRepositorio(BancoContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<Usuario> PegarPorId(string id)
        {
            return await _contexto
                        .Usuarios
                        .Include(u => u.Instrumentos)
                        .ThenInclude(ui => ui.Instrumento)
                        .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PegarUsuarioComandoResultado> PegarPorIdSemRastrear(string id)
        {
            var query = $"SELECT Id, Nome, Email, FotoUrl, DtCriacao FROM {UsuarioMap.Tabela} " +
                        $"WHERE Id = @{nameof(id)}";

            using (var conn = new SqliteConnection(Configuracoes.ConnString))
            {
                conn.Open();

                var resultado = await conn.QueryFirstOrDefaultAsync<PegarUsuarioComandoResultado>(query, new { id });

                if (resultado == null) return null;

                query = $"SELECT i.Id, i.Nome FROM {InstrumentoMap.Tabela} AS i " +
                        $"INNER JOIN {UsuarioInstrumentoMap.Tabela} AS ui ON ui.InstrumentoId = i.Id " +
                        $"WHERE ui.UsuarioId = '{resultado.Id}'";

                resultado.Instrumentos = await conn.QueryAsync<PegarInstrumentosComandoResultado>(query);

                return resultado;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="senhaEncriptada"></param>
        /// <returns></returns>
        public async Task<AutenticarUsuarioComandoResultado> PegarAutenticado(string login, string senhaEncriptada)
        {
            var query = $"SELECT Id, Nome, Email, FotoUrl, Senha FROM {UsuarioMap.Tabela} " +
                        $"WHERE Login = @{nameof(login)} AND Senha = @{nameof(senhaEncriptada)}";

            using (var conn = new SqliteConnection(Configuracoes.ConnString))
            {
                conn.Open();
                var resultado = await conn.QueryFirstOrDefaultAsync<AutenticarUsuarioComandoResultado>(query, new
                {
                    login,
                    senhaEncriptada
                });

                if (resultado == null) return null;

                // Atualiza data da última atividade
                query = $"UPDATE {UsuarioMap.Tabela} SET DtUltimaAtividade = '{DateTime.Now}' " +
                        $"WHERE Id = '{resultado.Id}'";
                await conn.ExecuteAsync(query);

                query = $"SELECT m.Id, m.Nome, m.FotoUrl, um.Administrador FROM {MinisterioMap.Tabela} AS m " +
                        $"INNER JOIN {UsuarioMinisterioMap.Tabela} AS um ON m.Id = um.MinisterioId " +
                        $"WHERE um.UsuarioId = '{resultado.Id}'";

                resultado.Ministerios = await conn.QueryAsync<PegarMinisterioComandoResultado>(query);

                return resultado;
            }
        }

        public void Criar(Usuario usuario)
        {
            _contexto.Usuarios.Add(usuario);
        }

        public void Atualizar(Usuario usuario)
        {
            _contexto.Entry(usuario).State = EntityState.Modified;
        }

        public async Task<bool> IdExiste(string id)
        {
            var query = $"SELECT 1 FROM {UsuarioMap.Tabela} " +
                        $"WHERE Id = @{nameof(id)}";

            using (var conn = new SqliteConnection(Configuracoes.ConnString))
            {
                conn.Open();
                return (await conn.QueryAsync<object>(query, new { id })).Any();
            }
        }

        public async Task<bool> EmailExiste(string email)
        {
            var query = $"SELECT 1 FROM {UsuarioMap.Tabela} " +
                        $"WHERE Email = @{nameof(email)}";

            using (var conn = new SqliteConnection(Configuracoes.ConnString))
            {
                conn.Open();
                return (await conn.QueryAsync<object>(query, new { email })).Any();
            }
        }
    }
}
