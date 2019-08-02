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
        private readonly IInstrumentoRepositorio _instrumentoRepo;
        private readonly IUow _uow;

        public SemeadorBd(IUsuarioRepositorio usuarioRepo
            , IMinisterioRepositorio ministerioRepo
            , IInstrumentoRepositorio instrumentoRepo, IUow uow)
        {
            _usuarioRepo = usuarioRepo;
            _ministerioRepo = ministerioRepo;
            _instrumentoRepo = instrumentoRepo;
            _uow = uow;
        }

        public async void SemearBancoDeDados()
        {
            if (await _usuarioRepo.IdExiste(PadroesString.UsuarioId))
                return;

            var usuario = CriarUsuario();

            _usuarioRepo.Criar(usuario);

            var nomeMinisterio = new Nome(PadroesString.MinisterioNome);
            _ministerioRepo.Criar(new Ministerio(PadroesString.MinisterioId, nomeMinisterio, usuario));

            SemearInstrumentos();

            await _uow.Salvar();
        }

        
        private async void SemearInstrumentos()
        {
            Instrumento[] instrumentos =
            {
                new Instrumento(new Nome("Violão")),
                new Instrumento(new Nome("Guitarra")),
                new Instrumento(new Nome("Baixo Elétrico")),
                new Instrumento(new Nome("Teclado")),
                new Instrumento(new Nome("Violino")),
                new Instrumento(new Nome("Bateria")),
                new Instrumento(new Nome("Saxofone")),
                new Instrumento(new Nome("Vocal"))
            };

            if (instrumentos.Length == await _instrumentoRepo.Contar()) return;

            _instrumentoRepo.CriarVarios(instrumentos);
        }

        public static Usuario CriarUsuario()
        {
            var nome = new Nome(PadroesString.UsuarioNome);
            var email = new Email(PadroesString.UsuarioEmail);
            var autenticacao = new Autenticacao(PadroesString.UsuarioLogin
                , PadroesString.UsuarioSenha
                , PadroesString.UsuarioSenha);

            return new Usuario(PadroesString.UsuarioId, nome, email, autenticacao);
        }
    }
}
