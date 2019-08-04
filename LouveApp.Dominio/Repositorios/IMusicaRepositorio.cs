using LouveApp.Dominio.Comandos.MusicaComandos.Saidas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LouveApp.Dominio.Repositorios
{
    public interface IMusicaRepositorio
    {
        Task<IEnumerable<PegarMusicaComandoResultado>> PegarPorMinisterio(string ministerioId);
    }
}
