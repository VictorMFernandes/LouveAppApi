using LouveApp.Compartilhado.Comandos;
using LouveApp.Compartilhado.Comandos.Genericos;
using LouveApp.Compartilhado.Padroes;
using LouveApp.Dominio.Comandos.MusicaComandos.Entradas;
using LouveApp.Dominio.Comandos.MusicaComandos.Saidas;
using LouveApp.Dominio.Entidades;
using LouveApp.Dominio.Repositorios;
using System.Threading.Tasks;

namespace LouveApp.Dominio.Gerenciadores
{
    public class MusicaGerenciador : Gerenciador
        , IComandoGerenciador<RegistrarMusicaComando>
        , IComandoGerenciador<ExcluirMusicaComando>
    {
        private readonly IMinisterioRepositorio _ministerioRepo;

        public MusicaGerenciador(IMinisterioRepositorio ministerioRepo)
        {
            _ministerioRepo = ministerioRepo;
        }

        public async Task<IComandoResultado> Executar(RegistrarMusicaComando comando)
        {
            if (!ValidarComando(comando))
                return null;

            var ministerio = await _ministerioRepo.PegarPorId(comando.MinisterioId);

            if (ministerio == null)
            {
                return new NaoEncontradoResultado(PadroesMensagens.MinisterioNaoEncontrado);
            }

            var musica = new Musica(comando.NomeVo, comando.ReferenciaVo);

            // Realiza a ação
            var acaoRealizada = ministerio.AdicionarMusica(comando.UsuarioLogadoId, musica);

            if (!acaoRealizada)
            {
                return new NaoAutorizadoResultado(PadroesMensagens.UsuarioSemPermissao);
            }

            _ministerioRepo.Atualizar(ministerio);

            return new RegistrarMusicaComandoResultado(musica.Id, musica.ToString());
        }

        public async Task<IComandoResultado> Executar(ExcluirMusicaComando comando)
        {
            if (!ValidarComando(comando))
                return null;

            var ministerio = await _ministerioRepo.PegarPorIdComMusicas(comando.MinisterioId);

            //Checa se tem permissão
            if (!ministerio.Administrador(comando.UsuarioLogadoId))
                return new NaoAutorizadoResultado(PadroesMensagens.UsuarioSemPermissao);

            // Realiza a ação
            if (!ministerio.RemoverMusica(comando.UsuarioLogadoId, comando.MusicaId))
                return new NaoEncontradoResultado(PadroesMensagens.MusicaNaoEncontrada);

            _ministerioRepo.Atualizar(ministerio);

            return null;
        }
    }
}
