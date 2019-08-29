using LouveApp.Dominio.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LouveApp.Dominio.Testes.Falsos
{
    internal class DispositivoRepositorioFalso : IDispositivoRepositorio
    {
        public Task<IEnumerable<string>> PegarDispositivosTokensPorUsuarioId(List<string> usuariosIds)
        {
            return null;
        }
    }
}
