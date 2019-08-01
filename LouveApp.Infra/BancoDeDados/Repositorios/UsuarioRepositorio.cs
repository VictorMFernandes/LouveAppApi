using LouveApp.Dominio.Entidades;
using LouveApp.Dominio.Repositorios;
using LouveApp.Infra.BancoDeDados.Contexto;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Dapper;
using LouveApp.Dominio.Comandos.AutenticacaoComandos.Saidas;
using LouveApp.Dominio.Comandos.MinisterioComandos.Saidas;
using LouveApp.Dominio.Sistema;
using LouveApp.Infra.BancoDeDados.Mapeamentos;
using LouveApp.Infra.BancoDeDados.Mapeamentos.Juncao;
using Microsoft.Data.Sqlite;

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

        public async Task<AutenticarUsuarioComandoResultado> PegarAutenticado(string login, string senhaEncriptada)
        {
            var query = $"SELECT Id, Nome, Email, FotoUrl, Senha FROM {UsuarioMap.Tabela} " +
                        $"WHERE Login = '{login}' AND Senha = '{senhaEncriptada}'";

            using (var conn = new SqliteConnection(Configuracoes.ConnString))
            {
                conn.Open();
                var resultado = await conn.QueryFirstOrDefaultAsync<AutenticarUsuarioComandoResultado>(query);

                if (resultado == null) return null;

                query = $"SELECT m.Id, m.Nome, m.FotoUrl, um.Administrador FROM {MinisterioMap.Tabela} AS m " +
                        $"INNER JOIN {UsuarioMinisterioMap.Tabela} AS um ON m.Id = um.MinisterioId " +
                        $"WHERE um.UsuarioId = '{resultado.Id}'";

                resultado.Ministerios = await conn.QueryAsync<PegarMinisteriosComandoResultado>(query);

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
            return await _contexto.Usuarios
                                    .AsNoTracking()
                                    .AnyAsync(x => x.Id == id);
        }

        public async Task<bool> EmailExiste(string email)
        {
            return await _contexto.Usuarios
                                    .AsNoTracking()
                                    .AnyAsync(x => x.Email.Endereco == email);
        }
    }
}
