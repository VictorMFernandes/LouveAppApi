using LouveApp.Dominio.Entidades;
using System.Threading.Tasks;
using LouveApp.Dominio.Comandos.AutenticacaoComandos.Saidas;
using LouveApp.Dominio.Comandos.UsuarioComandos.Saidas;

namespace LouveApp.Dominio.Repositorios
{
    public interface IUsuarioRepositorio
    {
        /// <summary>
        /// Pega um usuário do banco pelo id.
        /// Inclui instrumentos e dispositivos à entidade.
        /// </summary>
        /// <param name="id">Id que servirá de filtro para a busca.</param>
        /// <returns>Retorna um usuário com as propriedades listadas.</returns>
        Task<Usuario> PegarPorId(string id);
        Task<PegarUsuarioComandoResultado> PegarPorIdSemRastrear(string id);
        Task<AutenticarUsuarioComandoResultado> PegarAutenticado(string login, string senhaEncriptada);

        void Criar(Usuario usuario);
        void Atualizar(Usuario usuario);

        Task<bool> IdExiste(string id);
        Task<bool> EmailExiste(string email);
    }
}