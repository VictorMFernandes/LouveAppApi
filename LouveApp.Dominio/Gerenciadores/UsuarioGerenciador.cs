using System.Linq;
using LouveApp.Compartilhado.Comandos;
using LouveApp.Dominio.Comandos.UsuarioComandos.Entradas;
using LouveApp.Dominio.Comandos.UsuarioComandos.Saidas;
using LouveApp.Dominio.Repositorios;
using LouveApp.Dominio.Servicos;
using System.Threading.Tasks;
using LouveApp.Compartilhado.Comandos.Genericos;
using LouveApp.Compartilhado.Padroes;
using LouveApp.Dominio.Entidades;

namespace LouveApp.Dominio.Gerenciadores
{
    public class UsuarioGerenciador : Gerenciador
        , IComandoGerenciador<RegistrarUsuarioComando>
        , IComandoGerenciador<AtualizarUsuarioComando>
        , IComandoGerenciador<EntrarMinisterioComando>
        , IComandoGerenciador<AdicionarDispositivoComando>
    {
        private readonly IUsuarioRepositorio _usuarioRepo;
        private readonly IMinisterioRepositorio _ministerioRepo;
        private readonly IInstrumentoRepositorio _instrumentoRepo;
        private readonly IEmailServico _emailServico;
        private readonly IPushNotificationServico _pushNotificationServico;

        public UsuarioGerenciador(IUsuarioRepositorio usuarioRepo
            , IMinisterioRepositorio ministerioRepo
            , IInstrumentoRepositorio instrumentoRepo, IEmailServico emailServico
            , IPushNotificationServico pushNotificationServico)
        {
            _usuarioRepo = usuarioRepo;
            _ministerioRepo = ministerioRepo;
            _instrumentoRepo = instrumentoRepo;
            _emailServico = emailServico;
            _pushNotificationServico = pushNotificationServico;
        }

        #region Comandos

        public async Task<IComandoResultado> Executar(RegistrarUsuarioComando comando)
        {
            if (!ValidarComando(comando))
                return null;

            // Verificar se o e-mail já existe na base
            if (await _usuarioRepo.EmailExiste(comando.Email))
                AddNotification("Email", string.Format(PadroesMensagens.EmailEmUso, comando.Email));

            // Criar a entidade
            var usuario = new Usuario(comando.NomeVo, comando.EmailVo, comando.AutenticacaoVo);

            // Validar entidade
            AddNotifications(usuario);

            if (Invalid) return null;

            // Persistir o usuário
            _usuarioRepo.Criar(usuario);

            // Enviar um e-mail de boas vindas
            _emailServico.EnviarBoasVindas(usuario.Email.Endereco, usuario.ToString());

            // Retornar o resultado para tela
            return new RegistrarUsuarioComandoResultado(usuario.Id, usuario.ToString(), usuario.Email.Endereco);
        }

        public async Task<IComandoResultado> Executar(AtualizarUsuarioComando comando)
        {
            if (!ValidarComando(comando))
                return null;

            // Recupera usuário do banco
            var usuario = await _usuarioRepo.PegarPorId(comando.UsuarioLogadoId);

            // Caso o usuário não exista
            if (usuario == null)
                return new NaoEncontradoResultado(PadroesMensagens.UsuarioNaoEncontrado);

            var instrumentos = (await _instrumentoRepo.PegarVariosPorId(comando.InstrumentosIds))?.ToList();

            if (instrumentos == null)
            {
                return new NaoEncontradoResultado(PadroesMensagens.InstrumentoNaoEncontrado);
            }

            // Atualiza o usuário
            usuario.Atualizar(comando.NomeVo, instrumentos.Select(i => i.Id));

            // Valida
            AddNotifications(usuario);

            if (Invalid)
                return null;

            // Persistir o usuário
            _usuarioRepo.Atualizar(usuario);

            return new AtualizarUsuarioComandoResultado(usuario.Nome.ToString(), instrumentos);
        }

        public async Task<IComandoResultado> Executar(EntrarMinisterioComando comando)
        {
            if (!ValidarComando(comando))
                return null;

            var usuario = await _usuarioRepo.PegarPorId(comando.UsuarioLogadoId);

            // Caso o usuário não exista
            if (usuario == null)
            {
                return new NaoEncontradoResultado(PadroesMensagens.UsuarioNaoEncontrado);
            }

            var ministerio = await _ministerioRepo.PegarPorLinkConvite(comando.LinkConvite);

            // Caso o ministério não exista
            if (ministerio == null)
            {
                return new NaoEncontradoResultado(PadroesMensagens.LinkInvalido);
            }

            ministerio.AdicionarUsuario(usuario);

            // Validar entidade
            AddNotifications(ministerio);

            if (Invalid) return null;

            _ministerioRepo.Atualizar(ministerio);

            // Notificando administradores
            var administradoresTokens = ministerio
                .PegarAdministradores()
                .SelectMany(u => u.Dispositivos)
                .Select(d => d.Token);

            _pushNotificationServico.NotificarIngressoEmMinisterio(administradoresTokens.ToList()
                , usuario.ToString(), ministerio);

            return new EntrarMinisterioComandoResultado(ministerio.Id, ministerio.ToString());
        }

        public async Task<IComandoResultado> Executar(AdicionarDispositivoComando comando)
        {
            if (!ValidarComando(comando))
                return null;

            var usuario = await _usuarioRepo.PegarPorId(comando.UsuarioLogadoId);

            // Caso o usuário não exista
            if (usuario == null)
            {
                return new NaoEncontradoResultado(PadroesMensagens.UsuarioNaoEncontrado);
            }

            var dispositivo = new Dispositivo(comando.Token, comando.NomeVo);

            if (!usuario.AdicionarDispositivo(dispositivo))
                return null;

            // TODO adicionar notificações do dispositivo, quando nome se tornar obrigatório
            // Validar entidade
            AddNotifications(usuario);

            if (Invalid) return null;

            _usuarioRepo.Atualizar(usuario);

            return null;
        }

        #endregion
    }
}
