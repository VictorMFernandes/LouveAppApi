using LouveApp.Dominio.Entidades;
using LouveApp.Dominio.Repositorios;
using LouveApp.Infra.BancoDeDados.Contexto;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LouveApp.Infra.BancoDeDados.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContexto _contexto;

        public UsuarioRepositorio(BancoContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<Usuario> PegarPorId(string id)
        {
            return await _contexto
                        .Usuarios
                        .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Usuario> PegarPorLogin(string login)
        {
            return await _contexto
                        .Usuarios
                        .FirstOrDefaultAsync(x => x.Autenticacao.Login == login);
        }

        public void Criar(Usuario usuario)
        {
            _contexto.Usuarios.Add(usuario);
        }
        public void Atualizar(Usuario usuario)
        {
            _contexto.Entry(usuario).State = EntityState.Modified;
        }

        public async Task<bool> IdExiste(string id)
        {
            return await _contexto.Usuarios
                                    .AsNoTracking()
                                    .AnyAsync(x => x.Id == id);
        }

        public async Task<bool> EmailExiste(string email)
        {
            return await _contexto.Usuarios
                                    .AsNoTracking()
                                    .AnyAsync(x => x.Email.Endereco == email);
        }
    }
}
