using LouveApp.Dominio.Entidades;
using System.Threading.Tasks;

namespace LouveApp.Dominio.Repositorios
{
    public interface IUsuarioRepositorio
    {
        Task<Usuario> PegarPorId(string id);
        Task<Usuario> PegarPorLogin(string login);

        void Criar(Usuario usuario);
        void Atualizar(Usuario usuario);

        Task<bool> IdExiste(string id);
        Task<bool> EmailExiste(string email);
    }
}
