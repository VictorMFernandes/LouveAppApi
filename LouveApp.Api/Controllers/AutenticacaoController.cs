using LouveApp.Compartilhado.Entidades;
using LouveApp.Compartilhado.Transacoes;
using LouveApp.Dominio.Comandos.AutenticacaoComandos.Entradas;
using LouveApp.Dominio.Comandos.AutenticacaoComandos.Saidas;
using LouveApp.Dominio.Gerenciadores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LouveApp.Api.Controllers
{
    public class AutenticacaoController : ControladorApi
    {
        private readonly AutenticacaoGerenciador _gerenciador;

        public AutenticacaoController(IUow uow, AutenticacaoGerenciador gerenciador) : base(uow)
        {
            _gerenciador = gerenciador;
        }

        /// <summary>
        /// Loga um usuário no sistema.
        /// </summary>
        /// <param name="comando">Comando para logar usuário no sistema</param>
        /// <response code="200">Login realizado com sucesso.</response>
        [HttpPost("v1/Logar")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AutenticarUsuarioComandoResultado), 200)]
        public async Task<IActionResult> Logar([FromBody]AutenticarUsuarioComando comando)
        {
            var resultado = await _gerenciador.Executar(comando);

            return await Resposta(resultado, _gerenciador.Notifications);
        }
    }
}
