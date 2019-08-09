using System;
using System.Collections.Generic;
using LouveApp.Dominio.Gerenciadores;
using LouveApp.Infra.BancoDeDados.Transacoes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LouveApp.Compartilhado.Comandos.Genericos;
using LouveApp.Compartilhado.PaginacaoFiltragem;
using LouveApp.Dominio.Repositorios;
using LouveApp.Dominio.Comandos.EscalaComandos.Saidas;
using LouveApp.Dominio.Comandos.EscalaComandos.Entradas;
using LouveApp.Dominio.Entidades;

namespace LouveApp.Api.Controllers
{
    public class EscalasController : ControladorBase
    {
        private readonly EscalaGerenciador _gerenciador;
        private readonly IEscalaRepositorio _escalaRepo;

        public EscalasController(EscalaGerenciador gerenciador
            , IEscalaRepositorio escalaRepo, IUow uow)
            : base(uow)
        {
            _gerenciador = gerenciador;
            _escalaRepo = escalaRepo;
        }

        /// <summary>
        /// Registra uma escala a um ministério do sistema.
        /// </summary>
        /// <param name="ministerioId">Id do ministério que receberá a escala.</param>
        /// <param name="comando">Comando para registrar a escala.</param>
        /// <response code="200">Retorna as principais propriedades da escala que acabou de ser registrada.</response>
        [ProducesResponseType(typeof(RegistrarEscalaComandoResultado), 200)]
        [HttpPost]
        [Route("v1/Ministerios/{ministerioId}/[controller]")]
        public async Task<IActionResult> RegistrarEscala(string ministerioId, [FromBody]RegistrarEscalaComando comando)
        {
            comando.PegarIds(UsuarioLogadoId, ministerioId);

            var resultado = await _gerenciador.Executar(comando);
            return await Resposta(resultado, _gerenciador.Notifications);
        }

        /// <summary>
        /// Pega as escalas de um ministério.
        /// </summary>
        /// <response code="200">Retorna lista de escalas do ministério.</response>
        [ProducesResponseType(typeof(ColecaoPaginadaResultado<PegarEscalaComandoResultado>), 200)]
        [HttpGet("v1/Ministerios/{ministerioId}/[controller]", Name = nameof(PegarEscalasPorMinisterio))]
        public async Task<IActionResult> PegarEscalasPorMinisterio(string ministerioId, [FromQuery]EscalaFiltro filtro)
        {
            var escalas = await _escalaRepo.PegarPorMinisterio(ministerioId, UsuarioLogadoId, filtro);

            string uriProx = null, uriAnte = null;

            if (!escalas.EstaNaUltimaPagina())
            {
                filtro.Pagina++;
                uriProx = Url.Link(nameof(PegarEscalasPorMinisterio), filtro);
                filtro.Pagina--;
            }

            if (!escalas.EstaNaPrimeiraPagina())
            {
                filtro.Pagina--;
                uriAnte = Url.Link(nameof(PegarEscalasPorMinisterio), filtro);
            }

            var resultado = new ColecaoPaginadaResultado<PegarEscalaComandoResultado>(escalas, uriProx, uriAnte);

            return RespostaDeConsulta(resultado);
        }

        /// <summary>
        /// Pega as escalas de um usuário.
        /// </summary>
        /// <response code="200">Retorna lista de escalas do usuário.</response>
        [ProducesResponseType(typeof(IEnumerable<PegarEscalaComandoResultado>), 200)]
        [HttpGet]
        [Route("v1/Usuarios/{usuarioId}/[controller]")]
        public async Task<IActionResult> PegarEscalasPorUsuario(string usuarioId)
        {
            return RespostaDeConsulta(await _escalaRepo.PegarPorUsuario(usuarioId));
        }

        /// <summary>
        /// Exclui uma escala do sistema.
        /// </summary>
        /// <param name="ministerioId">Id do ministério que terá uma escala excluída.</param>
        /// <param name="escalaId">Id da escala que será excluída.</param>
        /// <response code="200">Escala excluída com sucesso.</response>
        [HttpDelete]
        [Route("v1/Ministerios/{ministerioId}/[controller]/{escalaId}")]
        public async Task<IActionResult> ExcluirEscala(string ministerioId, string escalaId)
        {
            var comando = new ExcluirEscalaComando(UsuarioLogadoId, ministerioId, escalaId);

            var resultado = await _gerenciador.Executar(comando);
            return await Resposta(resultado, _gerenciador.Notifications);
        }

        /// <summary>
        /// Pega uma escala por Id.
        /// </summary>
        /// <remarks>Só é possível pegar uma escala que o usuário logado faça parte.</remarks>
        /// <response code="200">Retorna a escala.</response>
        [ProducesResponseType(typeof(PegarEscalaComMusicasComandoResultado), 200)]
        [HttpGet]
        [Route("v1/Usuarios/{usuarioId}/[controller]/{escalaId}")]
        public async Task<IActionResult> PegarEscalaPorId(string usuarioId, string escalaId)
        {
            return RespostaDeConsulta(await _escalaRepo.PegarPorId(escalaId, usuarioId));
        }
    }
}
