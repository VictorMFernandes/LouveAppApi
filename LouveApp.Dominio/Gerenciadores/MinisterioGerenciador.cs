using System.Linq;
using System.Threading.Tasks;
using LouveApp.Compartilhado.Comandos;
using LouveApp.Compartilhado.Comandos.Genericos;
using LouveApp.Compartilhado.Padroes;
using LouveApp.Dominio.Comandos.MinisterioComandos.Entradas;
using LouveApp.Dominio.Comandos.MinisterioComandos.Saidas;
using LouveApp.Dominio.Comandos.NotificacaoComandos.Entradas;
using LouveApp.Dominio.Entidades;
using LouveApp.Dominio.Repositorios;
using LouveApp.Dominio.Servicos;

namespace LouveApp.Dominio.Gerenciadores
{
    public class MinisterioGerenciador : Gerenciador
        , IComandoGerenciador<RegistrarMinisterioComando>
        , IComandoGerenciador<AtivarLinkComando>
        , IComandoGerenciador<DesativarLinkComando>
        , IComandoGerenciador<ExcluirMinisterioComando>
        , IComandoGerenciador<NotificarChatMinisterioComando>
    {
        private readonly IMinisterioRepositorio _ministerioRepo;
        private readonly IUsuarioRepositorio _usuarioRepo;
        private readonly IPushNotificationServico _pushNotificationServico;

        public MinisterioGerenciador(IMinisterioRepositorio ministerioRepo
            , IUsuarioRepositorio usuarioRepo
            , IPushNotificationServico pushNotificationServico)
        {
            _ministerioRepo = ministerioRepo;
            _usuarioRepo = usuarioRepo;
            _pushNotificationServico = pushNotificationServico;
        }

        public async Task<IComandoResultado> Executar(RegistrarMinisterioComando comando)
        {
            if (!ValidarComando(comando))
                return null;

            // Recupera usuário do banco
            var usuario = await _usuarioRepo.PegarPorId(comando.UsuarioLogadoId);

            // Caso o usuário não exista
            if (usuario == null)
            {
                return new NaoEncontradoResultado(PadroesMensagens.UsuarioNaoEncontrado);
            }

            // Criar a entidade
            var ministerio = new Ministerio(comando.NomeVo, usuario);
            
            // Validar entidade
            AddNotifications(ministerio);

            if (Invalid) return null;

            // Persistir o usuário
            _ministerioRepo.Criar(ministerio);

            // Retornar o resultado para tela
            return new RegistrarMinisterioComandoResultado(ministerio.Id, ministerio.ToString());
        }

        public async Task<IComandoResultado> Executar(AtivarLinkComando comando)
        {
            if (!ValidarComando(comando))
                return null;

            var ministerio = await _ministerioRepo.PegarPorId(comando.MinisterioId);

            // Caso o usuário não exista
            if (ministerio == null)
            {
                return new NaoEncontradoResultado(PadroesMensagens.MinisterioNaoEncontrado);
            }

            // Realiza a ação
            var acaoRealizada = ministerio.AtivarLinkConvite(comando.UsuarioLogadoId);

            if (!acaoRealizada)
            {
                return new NaoAutorizadoResultado(PadroesMensagens.UsuarioSemPermissao);
            }

            _ministerioRepo.Atualizar(ministerio);

            return new AtivarLinkComandoResultado(ministerio.LinkConvite);
        }

        public async Task<IComandoResultado> Executar(DesativarLinkComando comando)
        {
            if (!ValidarComando(comando))
                return null;

            var ministerio = await _ministerioRepo.PegarPorId(comando.MinisterioId);

            // Caso o usuário não exista
            if (ministerio == null)
            {
                return new NaoEncontradoResultado(PadroesMensagens.MinisterioNaoEncontrado);
            }

            // Realiza a ação
            var acaoRealizada = ministerio.DesativarLinkConvite(comando.UsuarioLogadoId);

            if (!acaoRealizada)
            {
                return new NaoAutorizadoResultado(PadroesMensagens.UsuarioSemPermissao);
            }

            _ministerioRepo.Atualizar(ministerio);

            return null;
        }

        public async Task<IComandoResultado> Executar(ExcluirMinisterioComando comando)
        {
            if (!ValidarComando(comando))
                return null;

            var ministerio = await _ministerioRepo.PegarPorId(comando.MinisterioId);

            //Checa se tem permissão
            if (!ministerio.Administrador(comando.UsuarioLogadoId))
                return new NaoAutorizadoResultado(PadroesMensagens.UsuarioSemPermissao);

            // Realiza a ação
            _ministerioRepo.Remover(ministerio);

            return null;
        }

        public async Task<IComandoResultado> Executar(NotificarChatMinisterioComando comando)
        {
            if (!ValidarComando(comando))
                return null;

            var ministerio = await _ministerioRepo.PegarPorIdComUsuariosEDispositivos(comando.MinisterioId);

            // Caso o usuário não exista
            if (ministerio == null)
            {
                return new NaoEncontradoResultado(PadroesMensagens.MinisterioNaoEncontrado);
            }

            var remetente = ministerio.Usuarios
                .FirstOrDefault(um => um.UsuarioId == comando.UsuarioLogadoId);

            if (remetente == null)
            {
                return new NaoAutorizadoResultado(string.Format(PadroesMensagens.UsuarioNaoVinculado, ministerio));
            }

            var destinatariosTokens = ministerio
                .Usuarios
                .Where(um => um.UsuarioId != remetente.UsuarioId)
                .SelectMany(um => um.Usuario.Dispositivos)
                .Select(d => d.Token);

            _pushNotificationServico.NotificarChatMinisterio(destinatariosTokens.ToList()
                , comando.Mensagem, remetente.Usuario, ministerio.ToString());

            return null;
        }
    }
}
