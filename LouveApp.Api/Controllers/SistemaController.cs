using Microsoft.AspNetCore.Mvc;
using LouveApp.Compartilhado.Transacoes;
using LouveApp.Compartilhado.Entidades;
using LouveApp.Dominio.Sistema;
using Microsoft.AspNetCore.Authorization;

namespace LouveApp.Api.Controllers
{
    public class SistemaController : ControladorApi
    {
        public SistemaController(IUow uow)
            : base(uow)
        {
        }

        /// <summary>
        /// Pega as configurações do sistema.
        /// </summary>
        /// <response code="200">Retorna as principais configurações do sistema.</response>
        [HttpGet("v1/[controller]")]
        [AllowAnonymous]
        public IActionResult PegarSistema()
        {
            return Ok(new { Configuracoes.EmDesenvolvimento });
        }
    }
}
