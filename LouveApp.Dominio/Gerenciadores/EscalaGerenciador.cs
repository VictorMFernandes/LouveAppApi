using LouveApp.Compartilhado.Comandos;
using LouveApp.Compartilhado.Comandos.Genericos;
using LouveApp.Compartilhado.Padroes;
using LouveApp.Dominio.Comandos.EscalaComandos.Entradas;
using LouveApp.Dominio.Comandos.EscalaComandos.Saidas;
using LouveApp.Dominio.Entidades;
using LouveApp.Dominio.Repositorios;
using System.Threading.Tasks;

namespace LouveApp.Dominio.Gerenciadores
{
    public class EscalaGerenciador : Gerenciador
        , IComandoGerenciador<RegistrarEscalaComando>
        , IComandoGerenciador<ExcluirEscalaComando>
    {
        private readonly IMinisterioRepositorio _ministerioRepo;

        public EscalaGerenciador(IMinisterioRepositorio ministerioRepo)
        {
            _ministerioRepo = ministerioRepo;
        }

        public async Task<IComandoResultado> Executar(RegistrarEscalaComando comando)
        {
            if (!ValidarComando(comando))
                return null;

            var ministerio = await _ministerioRepo.PegarPorId(comando.MinisterioId);

            if (ministerio == null)
            {
                return new NaoEncontradoResultado(PadroesMensagens.MinisterioNaoEncontrado);
            }

            var escala = new Escala(comando.Data, comando.UsuariosIds);

            // Realiza a ação
            var acaoRealizada = ministerio.AdicionarEscala(comando.UsuarioLogadoId, escala);

            if (!acaoRealizada)
            {
                return new NaoAutorizadoResultado(PadroesMensagens.UsuarioSemPermissao);
            }

            _ministerioRepo.Atualizar(ministerio);

            return new RegistrarEscalaComandoResultado(escala.Id, escala.Data, escala.Usuarios.Count);
        }

        public async Task<IComandoResultado> Executar(ExcluirEscalaComando comando)
        {
            if (!ValidarComando(comando))
                return null;

            var ministerio = await _ministerioRepo.PegarPorId(comando.MinisterioId);

            //Checa se tem permissão
            if (!ministerio.Administrador(comando.UsuarioLogadoId))
                return new NaoAutorizadoResultado(PadroesMensagens.UsuarioSemPermissao);

            // Realiza a ação
            if (!ministerio.RemoverEscala(comando.UsuarioLogadoId, comando.EscalaId))
                return new NaoEncontradoResultado(PadroesMensagens.EscalaNaoEncontrada);

            _ministerioRepo.Atualizar(ministerio);

            return null;
        }
    }
}
