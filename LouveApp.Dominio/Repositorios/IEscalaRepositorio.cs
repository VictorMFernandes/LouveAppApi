using LouveApp.Dominio.Comandos.EscalaComandos.Saidas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LouveApp.Dominio.Repositorios
{
    public interface IEscalaRepositorio
    {
        Task<IEnumerable<PegarEscalaComandoResultado>> PegarPorMinisterio(string ministerioId);
        Task<IEnumerable<PegarEscalaComandoResultado>> PegarPorUsuario(string usuarioId);
    }
}
