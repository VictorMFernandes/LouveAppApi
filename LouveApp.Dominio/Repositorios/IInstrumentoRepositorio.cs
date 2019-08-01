using LouveApp.Dominio.Comandos.InstrumentoComandos.Saidas;
using LouveApp.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LouveApp.Dominio.Repositorios
{
    public interface IInstrumentoRepositorio
    {
        Task<Instrumento> PegarPorId(string id);
        Task<IEnumerable<PegarInstrumentosComandoResultado>> PegarTodos();
        void CriarVarios(IEnumerable<Instrumento> instrumentos);
        Task<int> Contar();
        Task<IEnumerable<PegarInstrumentosComandoResultado>> PegarVariosPorId(IEnumerable<string> ids);
    }
}
