using Dapper;
using LouveApp.Dal.Contexto;
using LouveApp.Dal.Mapeamentos;
using LouveApp.Dominio.Comandos.InstrumentoComandos.Saidas;
using LouveApp.Dominio.Entidades;
using LouveApp.Dominio.Repositorios;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LouveApp.Dal.Repositorios
{
    public class InstrumentoRepositorio : IInstrumentoRepositorio
    {
        private readonly BancoContexto _contexto;
        private readonly IDbConnection _conexao;

        public InstrumentoRepositorio(BancoContexto contexto, IDbConnection conexao)
        {
            _contexto = contexto;
            _conexao = conexao;
        }

        public async Task<PegarInstrumentosComandoResultado> PegarPorId(string id)
        {
            var query = $"SELECT Id, Nome FROM {InstrumentoMap.Tabela} " +
                        $"WHERE Id = @{nameof(id)}";

            return await _conexao.QueryFirstOrDefaultAsync<PegarInstrumentosComandoResultado>(query, new { id });
        }

        public async Task<IEnumerable<PegarInstrumentosComandoResultado>> PegarTodos()
        {
            var query = $"SELECT Id, Nome FROM {InstrumentoMap.Tabela}";

            return await _conexao.QueryAsync<PegarInstrumentosComandoResultado>(query);
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
            return await _conexao.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM {InstrumentoMap.Tabela}");
        }

        public async Task<IEnumerable<PegarInstrumentosComandoResultado>> PegarVariosPorId(IEnumerable<string> ids)
        {
            var idsLista = ids.ToList();

            if (!idsLista.Any()) return new List<PegarInstrumentosComandoResultado>();

            if (!idsLista.Any())
                return null;

            var query = $"SELECT Id, Nome FROM {InstrumentoMap.Tabela} " +
                        "WHERE Id = @id";

            var resultado = new List<PegarInstrumentosComandoResultado>();

            using (var conn = _conexao)
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
