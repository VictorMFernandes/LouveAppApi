using System.Collections.Generic;
using System.Threading.Tasks;
using LouveApp.Dominio.Comandos.InstrumentoComandos.Saidas;
using LouveApp.Dominio.Entidades;
using LouveApp.Dominio.Repositorios;

namespace LouveApp.Dominio.Testes.Falsos
{
    internal class InstrumentoRepositorioFalso : IInstrumentoRepositorio
    {
        public Task<PegarInstrumentosComandoResultado> PegarPorId(string id) => null;

        public Task<IEnumerable<PegarInstrumentosComandoResultado>> PegarTodos() => null;

        public void CriarVarios(IEnumerable<Instrumento> instrumentos) { }

        public Task<int> Contar() => Task.Run(() => 0);

        public Task<IEnumerable<PegarInstrumentosComandoResultado>> PegarVariosPorId(IEnumerable<string> ids) => null;
    }
}
