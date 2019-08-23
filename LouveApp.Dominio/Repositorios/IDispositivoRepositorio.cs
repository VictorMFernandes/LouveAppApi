using System.Collections.Generic;
using System.Threading.Tasks;

namespace LouveApp.Dominio.Repositorios
{
    public interface IDispositivoRepositorio
    {
        Task<IEnumerable<string>> PegarDispositivosTokensPorUsuarioId(List<string> usuariosIds);
    }
}
