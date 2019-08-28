using System.Linq;
using LouveApp.Dominio.Entidades;
using LouveApp.Dominio.Repositorios;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Dapper;
using LouveApp.Dominio.Comandos.AutenticacaoComandos.Saidas;
using LouveApp.Dominio.Comandos.InstrumentoComandos.Saidas;
using LouveApp.Dominio.Comandos.MinisterioComandos.Saidas;
using LouveApp.Dominio.Comandos.UsuarioComandos.Saidas;
using LouveApp.Dal.Mapeamentos;
using LouveApp.Dal.Mapeamentos.Juncao;
using System;
using LouveApp.Dal.Contexto;
using System.Data;

namespace LouveApp.Dal.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContexto _contexto;
        private readonly IDbConnection _conexao;

        public UsuarioRepositorio(BancoContexto contexto, IDbConnection conexao)
        {
            _contexto = contexto;
            _conexao = conexao;
        }

        public async Task<Usuario> PegarPorId(string id)
        {
            return await _contexto
                        .Usuarios
                        .Include(u => u.Instrumentos)
                        .ThenInclude(ui => ui.Instrumento)
                        .Include(u => u.Dispositivos)
                        .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PegarUsuarioComandoResultado> PegarPorIdSemRastrear(string id)
        {
            var query = $"SELECT Id, Nome, Email, FotoUrl, DtCriacao FROM {UsuarioMap.Tabela} " +
                        $"WHERE Id = @{nameof(id)}";

            using (var conn = _conexao)
            {
                _conexao.Open();

                var resultado = await _conexao.QueryFirstOrDefaultAsync<PegarUsuarioComandoResultado>(query, new { id });

                if (resultado == null) return null;

                query = $"SELECT i.Id, i.Nome FROM {InstrumentoMap.Tabela} AS i " +
                        $"INNER JOIN {UsuarioInstrumentoMap.Tabela} AS ui ON ui.InstrumentoId = i.Id " +
                        $"WHERE ui.UsuarioId = '{resultado.Id}'";

                resultado.Instrumentos = await _conexao.QueryAsync<PegarInstrumentosComandoResultado>(query);

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

            using (var conn = _conexao)
            {
                _conexao.Open();

                var resultado = await _conexao.QueryFirstOrDefaultAsync<AutenticarUsuarioComandoResultado>(query, new
                {
                    login,
                    senhaEncriptada
                });

                if (resultado == null) return null;

                // Atualiza data da última atividade
                query = $"UPDATE {UsuarioMap.Tabela} SET DtUltimaAtividade = @dtUltimaAtividade " +
                        $"WHERE Id = '{resultado.Id}'";
                await _conexao.ExecuteAsync(query, new { dtUltimaAtividade = DateTime.Now });

                query = $"SELECT m.Id, m.Nome, m.FotoUrl, um.Administrador FROM {MinisterioMap.Tabela} AS m " +
                        $"INNER JOIN {UsuarioMinisterioMap.Tabela} AS um ON m.Id = um.MinisterioId " +
                        $"WHERE um.UsuarioId = '{resultado.Id}'";

                resultado.Ministerios = await _conexao.QueryAsync<PegarMinisterioComandoResultado>(query);

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

            return (await _conexao.QueryAsync<object>(query, new { id })).Any();
        }

        public async Task<bool> EmailExiste(string email)
        {
            var query = $"SELECT 1 FROM {UsuarioMap.Tabela} " +
                        $"WHERE Email = @{nameof(email)}";

            return (await _conexao.QueryAsync<object>(query, new { email })).Any();
        }
    }
}
