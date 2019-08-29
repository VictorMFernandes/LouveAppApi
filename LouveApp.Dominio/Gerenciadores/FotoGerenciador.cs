using LouveApp.Compartilhado.Comandos;
using LouveApp.Compartilhado.Comandos.Genericos;
using LouveApp.Dominio.Comandos.FotoComandos.Entradas;
using LouveApp.Dominio.Comandos.FotoComandos.Saidas;
using LouveApp.Dominio.Repositorios;
using LouveApp.Dominio.Servicos;
using LouveApp.Dominio.Sistema.Padroes;
using System.Threading.Tasks;

namespace LouveApp.Dominio.Gerenciadores
{
    public class FotoGerenciador : Gerenciador
        , IComandoGerenciador<AlterarFotoUsuarioComando>
    {
        private readonly IUsuarioRepositorio _usuarioRepo;
        private readonly IMinisterioRepositorio _ministerioRepo;
        private readonly IFotoServico _fotoServico;

        public FotoGerenciador(IUsuarioRepositorio usuarioRepo
            , IMinisterioRepositorio ministerioRepo, IFotoServico fotoServico)
        {
            _usuarioRepo = usuarioRepo;
            _ministerioRepo = ministerioRepo;
            _fotoServico = fotoServico;
        }

        #region Comandos

        public async Task<IComandoResultado> Executar(AlterarFotoUsuarioComando comando)
        {
            // Verifica a validade do comando
            if (!ValidarComando(comando))
                return null;

            // Checa se o usuário tem permissão para fazer a ação
            if (comando.UsuarioId != comando.UsuarioLogadoId)
            {
                return new NaoAutorizadoResultado("Não é possível alterar a foto de outro usuário");
            }

            // Recupera usuário do banco
            var usuario = await _usuarioRepo.PegarPorId(comando.UsuarioId);

            // Caso o usuário não exista
            if (usuario == null)
            {
                return new NaoEncontradoResultado(PadroesMensagens.UsuarioNaoEncontrado);
            }

            if (!string.IsNullOrEmpty(usuario.Foto.IdPublico)) _fotoServico.DeletarFoto(usuario.Foto.IdPublico);

            var foto = _fotoServico.UploadFoto(comando.ArquivoFoto);

            // Validar entidade
            AddNotifications(foto);

            if (Invalid)
            {
                _fotoServico.DeletarFoto(foto.IdPublico);
                return null;
            }

            // Persistir a foto
            usuario.AtualizarFoto(foto);
            _usuarioRepo.Atualizar(usuario);

            // Retornar o resultado para tela
            return new AlterarFotoComandoResultado(foto.Url);
        }

        public async Task<IComandoResultado> Executar(AlterarFotoMinisterioComando comando)
        {
            // Verifica a validade do comando
            if (!ValidarComando(comando))
                return null;

            // Recupera ministério do banco
            var ministerio = await _ministerioRepo.PegarPorId(comando.MinisterioId);

            // Caso o ministério não exista
            if (ministerio == null)
            {
                return new NaoEncontradoResultado(PadroesMensagens.MinisterioNaoEncontrado);
            }

            var autorizado = ministerio.Administrador(comando.UsuarioLogadoId);

            // Checa se o usuário está autorizado para fazer a ação
            if (!autorizado)
            {
                return new NaoAutorizadoResultado(PadroesMensagens.UsuarioSemPermissao);
            }

            if (!string.IsNullOrEmpty(ministerio.Foto.IdPublico)) _fotoServico.DeletarFoto(ministerio.Foto.IdPublico);

            var foto = _fotoServico.UploadFoto(comando.ArquivoFoto);

            // Validar entidade
            AddNotifications(foto);

            if (Invalid)
            {
                _fotoServico.DeletarFoto(foto.IdPublico);
                return null;
            }

            // Persistir a foto
            ministerio.AtualizarFoto(foto);
            _ministerioRepo.Atualizar(ministerio);

            // Retornar o resultado para tela
            return new AlterarFotoComandoResultado(foto.Url);
        }

        #endregion
    }
}
