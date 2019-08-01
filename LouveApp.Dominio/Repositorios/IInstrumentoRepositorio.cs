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
        void Criar(Instrumento instrumento);
        void CriarVarios(IEnumerable<Instrumento> instrumentos);
        Task<int> Contar();
    }
}
