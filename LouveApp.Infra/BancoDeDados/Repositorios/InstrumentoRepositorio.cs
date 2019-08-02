using Dapper;
using LouveApp.Dominio.Comandos.InstrumentoComandos.Saidas;
using LouveApp.Dominio.Entidades;
using LouveApp.Dominio.Repositorios;
using LouveApp.Dominio.Sistema;
using LouveApp.Infra.BancoDeDados.Contexto;
using LouveApp.Infra.BancoDeDados.Mapeamentos;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LouveApp.Infra.BancoDeDados.Repositorios
{
    public class InstrumentoRepositorio : IInstrumentoRepositorio
    {
        private readonly BancoContexto _contexto;

        public InstrumentoRepositorio(BancoContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<PegarInstrumentosComandoResultado> PegarPorId(string id)
        {
            var query = $"SELECT Id, Nome FROM {InstrumentoMap.Tabela} " +
                        "WHERE Id = @id";

            using (var conn = new SqliteConnection(Configuracoes.ConnString))
            {
                conn.Open();
                return await conn.QueryFirstOrDefaultAsync<PegarInstrumentosComandoResultado>(query, new { id });
            }
        }

        public async Task<IEnumerable<PegarInstrumentosComandoResultado>> PegarTodos()
        {
            var query = $"SELECT Id, Nome FROM {InstrumentoMap.Tabela}";

            using (var conn = new SqliteConnection(Configuracoes.ConnString))
            {
                conn.Open();
                return await conn.QueryAsync<PegarInstrumentosComandoResultado>(query);
            }
        }

        public void Criar(Instrumento instrumento)
        {
            _contexto.Instrumentos.Add(instrumento);
        }

        public void CriarVarios(IEnumerable<Instrumento> instrumentos)
        {
            _contexto.Instrumentos.AddRange(instrumentos);
        }

        public async Task<int> Contar()
        {
            using (var conn = new SqliteConnection(Configuracoes.ConnString))
            {
                conn.Open();
                return await conn.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM {InstrumentoMap.Tabela}");
            }
        }

        public async Task<IEnumerable<PegarInstrumentosComandoResultado>> PegarVariosPorId(IEnumerable<string> ids)
        {
            var idsLista = ids.ToList();

            if (!idsLista.Any())
                return null;

            var query = $"SELECT Id, Nome FROM {InstrumentoMap.Tabela} " +
                        "WHERE Id = @id";

            var resultado = new List<PegarInstrumentosComandoResultado>();

            using (var conn = new SqliteConnection(Configuracoes.ConnString))
            {
                conn.Open();

                foreach (var id in idsLista)
                {
                    var instrumento = await conn.QueryFirstOrDefaultAsync<PegarInstrumentosComandoResultado>(query, new { id });

                    if (instrumento == null)
                        return null;

                    resultado.Add(instrumento);
                }

                return resultado;
            }
        }
    }
}
