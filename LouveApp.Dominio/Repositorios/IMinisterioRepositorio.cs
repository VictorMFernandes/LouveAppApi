using LouveApp.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;
using LouveApp.Dominio.Comandos.MinisterioComandos.Saidas;

namespace LouveApp.Dominio.Repositorios
{
    public interface IMinisterioRepositorio
    {
        Task<Ministerio> PegarPorId(string id);
        Task<Ministerio> PegarPorLinkConvite(string linkConvite);
        Task<IEnumerable<PegarMinisteriosComandoResultado>> PegarPorUsuario(string id);

        void Criar(Ministerio ministerio);

        void Atualizar(Ministerio ministerio);

        void Remover(string id);

        Task<bool> EAdministrador(string usuarioId, string ministerioId);
    }
}
