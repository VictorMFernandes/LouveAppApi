using LouveApp.Dominio.Comandos.EscalaComandos.Saidas;
using System.Collections.Generic;
using System.Threading.Tasks;
using LouveApp.Compartilhado.PaginacaoFiltragem;
using LouveApp.Dominio.Comandos.EscalaComandos.Entradas;

namespace LouveApp.Dominio.Repositorios
{
    public interface IEscalaRepositorio
    {
        Task<PegarEscalaComMusicasComandoResultado> PegarPorId(string escalaId, string usuarioId);
        Task<ListaPaginada<PegarEscalaComandoResultado>> PegarPorMinisterio(
            string ministerioId, string usuarioId, EscalaFiltro filtro);
        Task<IEnumerable<PegarEscalaComandoResultado>> PegarPorUsuario(string usuarioId);
    }
}
