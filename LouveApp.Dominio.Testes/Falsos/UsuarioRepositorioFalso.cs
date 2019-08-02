using System.Threading.Tasks;
using LouveApp.Dominio.Comandos.AutenticacaoComandos.Saidas;
using LouveApp.Dominio.Comandos.UsuarioComandos.Saidas;
using LouveApp.Dominio.Entidades;
using LouveApp.Dominio.Repositorios;

namespace LouveApp.Dominio.Testes.Falsos
{
    internal class UsuarioRepositorioFalso : IUsuarioRepositorio
    {
        public void Atualizar(Usuario usuario) { }

        public void Criar(Usuario usuario) { }

        public Task<bool> IdExiste(string id) => Task.Run(() => true);

        public Task<bool> EmailExiste(string email) => Task.Run(() => false);

        public Task<Usuario> PegarPorId(string id) => null;
        public Task<PegarUsuarioComandoResultado> PegarPorIdSemRastrear(string id) => null;

        public Task<AutenticarUsuarioComandoResultado> PegarAutenticado(string login, string senhaEncriptada) => null;
    }
}
