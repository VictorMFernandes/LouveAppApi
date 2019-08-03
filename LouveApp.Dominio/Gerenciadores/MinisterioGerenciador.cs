using System.Threading.Tasks;
using LouveApp.Compartilhado.Comandos;
using LouveApp.Compartilhado.Comandos.Genericos;
using LouveApp.Compartilhado.Padroes;
using LouveApp.Dominio.Comandos.MinisterioComandos.Entradas;
using LouveApp.Dominio.Comandos.MinisterioComandos.Saidas;
using LouveApp.Dominio.Entidades;
using LouveApp.Dominio.Repositorios;

namespace LouveApp.Dominio.Gerenciadores
{
    public class MinisterioGerenciador : Gerenciador
        , IComandoGerenciador<RegistrarMinisterioComando>
        , IComandoGerenciador<AtivarLinkComando>
        , IComandoGerenciador<DesativarLinkComando>
        , IComandoGerenciador<ExcluirMinisterioComando>
    {
        private readonly IMinisterioRepositorio _ministerioRepo;
        private readonly IUsuarioRepositorio _usuarioRepo;

        public MinisterioGerenciador(IMinisterioRepositorio ministerioRepo, IUsuarioRepositorio usuarioRepo)
        {
            _ministerioRepo = ministerioRepo;
            _usuarioRepo = usuarioRepo;
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
    }
}
