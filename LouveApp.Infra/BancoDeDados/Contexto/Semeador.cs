using LouveApp.Dominio.Entidades;
using LouveApp.Dominio.Repositorios;
using LouveApp.Dominio.Servicos;
using LouveApp.Dominio.ValueObjects;
using LouveApp.Infra.BancoDeDados.Transacoes;
using LouveApp.Compartilhado.Padroes;

namespace LouveApp.Infra.BancoDeDados.Contexto
{
    public class SemeadorBd : ISemeadorBd
    {
        private readonly IUsuarioRepositorio _usuarioRepo;
        private readonly IMinisterioRepositorio _ministerioRepo;
        private readonly IUow _uow;

        public SemeadorBd(IUsuarioRepositorio usuarioRepo
            , IMinisterioRepositorio ministerioRepo, IUow uow)
        {
            _usuarioRepo = usuarioRepo;
            _ministerioRepo = ministerioRepo;
            _uow = uow;
        }

        public async void SemearBancoDeDados()
        {
            if (await _usuarioRepo.IdExiste(PadroesString.UsuarioId))
                return;

            var nome = new Nome(PadroesString.UsuarioNome);
            var email = new Email(PadroesString.UsuarioEmail);
            var autenticacao = new Autenticacao(PadroesString.UsuarioLogin
                                                , PadroesString.UsuarioSenha
                                                , PadroesString.UsuarioSenha);

            var usuario = new Usuario(PadroesString.UsuarioId, nome, email, autenticacao);
            _usuarioRepo.Criar(usuario);

            var nomeMinisterio = new Nome(PadroesString.MinisterioNome);
            _ministerioRepo.Criar(new Ministerio(PadroesString.MinisterioId, nomeMinisterio, usuario));

            await _uow.Salvar();
        }
    }
}
