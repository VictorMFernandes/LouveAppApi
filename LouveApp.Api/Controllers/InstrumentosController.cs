using System.Collections.Generic;
using LouveApp.Infra.BancoDeDados.Transacoes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LouveApp.Dominio.Repositorios;
using LouveApp.Dominio.Comandos.InstrumentoComandos.Saidas;

namespace LouveApp.Api.Controllers
{
    public class InstrumentosController : ControladorBase
    {
        private readonly IInstrumentoRepositorio _instrumentoRepo;

        public InstrumentosController(IInstrumentoRepositorio instrumentoRepo, IUow uow)
            : base(uow)
        {
            _instrumentoRepo = instrumentoRepo;
        }

        /// <summary>
        /// Retorna todos os instrumentos cadastrados no sistema.
        /// </summary>
        /// <response code="200">Lista dos instrumentos.</response>
        [ProducesResponseType(typeof(IEnumerable<PegarInstrumentosComandoResultado>), 200)]
        [HttpGet]
        [Route("v1/[controller]")]
        public async Task<IActionResult> PegarMinisterios()
        {
            var ministerios = await _instrumentoRepo.PegarTodos();
            return Ok(ministerios);
        }
    }
}
