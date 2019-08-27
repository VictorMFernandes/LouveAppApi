using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LouveApp.Dominio.Repositorios;
using LouveApp.Dominio.Comandos.InstrumentoComandos.Saidas;
using LouveApp.Compartilhado.Transacoes;
using LouveApp.Compartilhado.Entidades;

namespace LouveApp.Api.Controllers
{
    public class InstrumentosController : ControladorApi
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
        public async Task<IActionResult> PegarInstrumentos()
        {
            return RespostaDeConsulta(await _instrumentoRepo.PegarTodos());
        }
    }
}
