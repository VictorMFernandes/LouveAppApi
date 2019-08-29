using LouveApp.Compartilhado.Comandos;
using LouveApp.Compartilhado.Comandos.Genericos;
using LouveApp.Dominio.Comandos.MusicaComandos.Entradas;
using LouveApp.Dominio.Comandos.MusicaComandos.Saidas;
using LouveApp.Dominio.Repositorios;
using LouveApp.Dominio.Sistema.Padroes;
using System.Threading.Tasks;

namespace LouveApp.Dominio.Gerenciadores
{
    public class MusicaGerenciador : Gerenciador
        , IComandoGerenciador<RegistrarMusicaComando>
        , IComandoGerenciador<ExcluirMusicaComando>
        , IComandoGerenciador<AtualizarMusicaComando>
    {
        private readonly IMinisterioRepositorio _ministerioRepo;
        private readonly IMusicaRepositorio _musicaRepo;

        public MusicaGerenciador(IMinisterioRepositorio ministerioRepo
            , IMusicaRepositorio musicaRepo)
        {
            _ministerioRepo = ministerioRepo;
            _musicaRepo = musicaRepo;
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

            var musica = comando.Musica;

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

        public async Task<IComandoResultado> Executar(AtualizarMusicaComando comando)
        {
            if (!ValidarComando(comando))
                return null;

            // Caso o usuário não tenha autorização
            var musica = await _musicaRepo.PegarPorIdComUsuariosDoMinisterio(comando.MusicaId);

            if (musica == null)
                return new NaoEncontradoResultado(PadroesMensagens.MusicaNaoEncontrada);

            if (!musica.EhAdministrador(comando.UsuarioLogadoId))
            {
                return new NaoAutorizadoResultado(PadroesMensagens.UsuarioSemPermissao);
            }

            // Atualiza a música
            musica.Atualizar(comando.NomeVo, comando.LetraVo, comando.CifraVo
                , comando.VideoVo, comando.ArtistaVo, comando.Tom, comando.Bpm
                , comando.Classificacao);

            // Valida
            AddNotifications(musica);

            if (Invalid)
                return null;

            // Persistir o usuário
            _musicaRepo.Atualizar(musica);

            return new AtualizarMusicaComandoResultado(musica);
        }
    }
}
