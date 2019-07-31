using LouveApp.Dominio.Entidades;
using System.Threading.Tasks;
using LouveApp.Dominio.Comandos.AutenticacaoComandos.Saidas;

namespace LouveApp.Dominio.Repositorios
{
    public interface IUsuarioRepositorio
    {
        Task<Usuario> PegarPorId(string id);
        Task<AutenticarUsuarioComandoResultado> PegarAutenticado(string login, string senhaEncriptada);

        void Criar(Usuario usuario);
        void Atualizar(Usuario usuario);

        Task<bool> IdExiste(string id);
        Task<bool> EmailExiste(string email);
    }
}
