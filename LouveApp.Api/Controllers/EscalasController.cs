using System.Collections.Generic;
using LouveApp.Dominio.Gerenciadores;
using LouveApp.Infra.BancoDeDados.Transacoes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LouveApp.Dominio.Comandos.MinisterioComandos.Entradas;
using LouveApp.Dominio.Comandos.MinisterioComandos.Saidas;
using LouveApp.Dominio.Repositorios;
using LouveApp.Dominio.Comandos.EscalaComandos.Saidas;
using LouveApp.Dominio.Comandos.EscalaComandos.Entradas;
using LouveApp.Dominio.Comandos.UsuarioComandos.Saidas;

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
        public async Task<IActionResult> RegistrarMinisterio(string ministerioId, [FromBody]RegistrarEscalaComando comando)
        {
            comando.PegarIds(UsuarioLogadoId, ministerioId);

            var resultado = await _gerenciador.Executar(comando);
            return await Resposta(resultado, _gerenciador.Notifications);
        }

        /// <summary>
        /// Pega as escalas de um ministério.
        /// </summary>
        /// <response code="200">Retorna lista de escalas do ministério.</response>
        [ProducesResponseType(typeof(IEnumerable<PegarEscalaComandoResultado>), 200)]
        [HttpGet]
        [Route("v1/Ministerios/{ministerioId}/[controller]")]
        public async Task<IActionResult> PegarUsuarios(string ministerioId)
        {
            return RespostaDeConsulta(await _escalaRepo.PegarPorMinisterio(ministerioId));
        }

        /// <summary>
        /// Exclui uma escala do sistema.
        /// </summary>
        /// <param name="ministerioId">Id do ministério que terá uma escala excluída.</param>
        /// <param name="escalaId">Id da escala que será excluída.</param>
        /// <response code="200">Escala excluída com sucesso.</response>
        [HttpDelete]
        [Route("v1/Ministerios/{ministerioId}/[controller]/{escalaId}")]
        public async Task<IActionResult> ExcluirMinisterio(string ministerioId, string escalaId)
        {
            var comando = new ExcluirEscalaComando(UsuarioLogadoId, ministerioId, escalaId);

            var resultado = await _gerenciador.Executar(comando);
            return await Resposta(resultado, _gerenciador.Notifications);
        }
    }
}
