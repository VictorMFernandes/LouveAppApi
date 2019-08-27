using System.Collections.Generic;
using LouveApp.Dominio.Gerenciadores;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LouveApp.Dominio.Repositorios;
using LouveApp.Dominio.Comandos.MusicaComandos.Entradas;
using LouveApp.Dominio.Comandos.MusicaComandos.Saidas;
using LouveApp.Compartilhado.Transacoes;
using LouveApp.Compartilhado.Entidades;

namespace LouveApp.Api.Controllers
{
    public class MusicasController : ControladorApi
    {
        private readonly MusicaGerenciador _gerenciador;
        private readonly IMusicaRepositorio _musicaRepo;

        public MusicasController(MusicaGerenciador gerenciador
            , IMusicaRepositorio musicaRepo, IUow uow)
            : base(uow)
        {
            _gerenciador = gerenciador;
            _musicaRepo = musicaRepo;
        }

        /// <summary>
        /// Registra uma música a um ministério do sistema.
        /// </summary>
        /// <param name="ministerioId">Id do ministério que receberá a música.</param>
        /// <param name="comando">Comando para registrar a música.</param>
        /// <response code="200">Retorna as principais propriedades da música que acabou de ser registrada.</response>
        [ProducesResponseType(typeof(RegistrarMusicaComandoResultado), 200)]
        [HttpPost("v1/Ministerios/{ministerioId}/[controller]")]
        public async Task<IActionResult> RegistrarMusica(string ministerioId, [FromBody]RegistrarMusicaComando comando)
        {
            comando.PegarIds(UsuarioLogadoId, ministerioId);

            var resultado = await _gerenciador.Executar(comando);
            return await Resposta(resultado, _gerenciador.Notifications);
        }

        /// <summary>
        /// Pega as músicas de um ministério.
        /// </summary>
        /// <response code="200">Retorna lista de músicas do ministério.</response>
        [ProducesResponseType(typeof(IEnumerable<PegarMusicaComandoResultado>), 200)]
        [HttpGet("v1/Ministerios/{ministerioId}/[controller]")]
        public async Task<IActionResult> PegarMusicas(string ministerioId)
        {
            return RespostaDeConsulta(await _musicaRepo.PegarPorMinisterio(ministerioId));
        }

        /// <summary>
        /// Exclui uma música do sistema.
        /// </summary>
        /// <param name="ministerioId">Id do ministério que terá uma música excluída.</param>
        /// <param name="musicaId">Id da música que será excluída.</param>
        /// <response code="200">Música excluída com sucesso.</response>
        [HttpDelete("v1/Ministerios/{ministerioId}/[controller]/{musicaId}")]
        public async Task<IActionResult> ExcluirMusica(string ministerioId, string musicaId)
        {
            var comando = new ExcluirMusicaComando(UsuarioLogadoId, ministerioId, musicaId);

            var resultado = await _gerenciador.Executar(comando);
            return await Resposta(resultado, _gerenciador.Notifications);
        }

        /// <summary>
        /// Atualiza uma música de um ministério.
        /// </summary>
        /// <param name="comando">Comando para atualizar música de um ministério</param>
        /// <response code="200">Retorna as principais propriedades da música que acabou de ser atualizada.</response>
        [ProducesResponseType(typeof(AtualizarMusicaComandoResultado), 200)]
        [HttpPut("v1/Ministerios/{ministerioId}/[controller]")]
        public async Task<IActionResult> AtualizarMusica([FromBody]AtualizarMusicaComando comando)
        {
            comando.PegarUsuarioLogadoId(UsuarioLogadoId);

            var resultado = await _gerenciador.Executar(comando);
            return await Resposta(resultado, _gerenciador.Notifications);
        }

        /// <summary>
        /// Pega uma música pelo nome e pelo artista.
        /// </summary>
        /// <remarks>Os nomes devem ser exatamente os mesmos dos cadastrados em banco.</remarks>
        /// <param name="ministerioId">Id do ministério que a música pertence.</param>
        /// <param name="comando">Comando para fazer a busca da música.</param>
        /// <response code="200">Retorna a música buscada.</response>
        [HttpGet("v1/Ministerios/{ministerioId}/[controller]/PorNomeEArtista")]
        public async Task<IActionResult> PegarMusicaPorNomeEArtista(string ministerioId, PegarMusicaPorNomeEArtistaComando comando)
        {
            return RespostaDeConsulta(await _musicaRepo.PegarPorNomeEArtista(ministerioId, comando.Nome, comando.Artista));
        }
    }
}
