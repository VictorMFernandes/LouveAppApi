using LouveApp.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;
using LouveApp.Dominio.Comandos.MinisterioComandos.Saidas;

namespace LouveApp.Dominio.Repositorios
{
    public interface IMinisterioRepositorio
    {
        Task<Ministerio> PegarPorId(string ministerioId);
        Task<Ministerio> PegarPorLinkConvite(string linkConvite);
        Task<IEnumerable<PegarMinisteriosComandoResultado>> PegarPorUsuario(string usuarioId);
        void Criar(Ministerio ministerio);
        void Atualizar(Ministerio ministerio);
        /// <summary>
        /// Remove um ministério do banco de dados sem a necessidade de salvar posteriormente.
        /// </summary>
        /// <param name="ministerioId">Id do ministério que será removido.</param>
        Task Remover(string ministerioId);
        Task<bool> EAdministrador(string usuarioId, string ministerioId);
        Task<int> Contar();
    }
}
