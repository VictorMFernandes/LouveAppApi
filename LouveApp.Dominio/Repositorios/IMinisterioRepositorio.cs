using LouveApp.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;
using LouveApp.Dominio.Comandos.MinisterioComandos.Saidas;
using LouveApp.Dominio.Comandos.UsuarioComandos.Saidas;

namespace LouveApp.Dominio.Repositorios
{
    public interface IMinisterioRepositorio
    {
        Task<Ministerio> PegarPorId(string ministerioId);
        Task<Ministerio> PegarPorIdComMusicas(string ministerioId);
        Task<Ministerio> PegarPorLinkConvite(string linkConvite);
        Task<IEnumerable<PegarMinisterioComandoResultado>> PegarPorUsuario(string usuarioId);
        void Criar(Ministerio ministerio);
        void Atualizar(Ministerio ministerio);
        void Remover(Ministerio ministerio);
        Task<bool> EAdministrador(string usuarioId, string ministerioId);
        Task<int> Contar();
        Task<IEnumerable<PegarUsuarioComandoResultado>> PegarUsuarios(string ministerioId);
    }
}
