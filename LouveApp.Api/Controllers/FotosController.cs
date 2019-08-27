using LouveApp.Dominio.Comandos.FotoComandos.Entradas;
using LouveApp.Dominio.Comandos.FotoComandos.Saidas;
using LouveApp.Dominio.Gerenciadores;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LouveApp.Compartilhado.Entidades;
using LouveApp.Compartilhado.Transacoes;

namespace LouveApp.Api.Controllers
{
    public class FotosController : ControladorApi
    {
        private readonly FotoGerenciador _gerenciador;

        public FotosController(FotoGerenciador gerenciador, IUow uow)
            : base(uow)
        {
            _gerenciador = gerenciador;
        }

        /// <summary>
        /// Altera a foto de um usuário do sistema.
        /// </summary>
        /// <param name="usuarioId">Id do usuário que terá sua foto alterada.</param>
        /// <param name="comando">Comando para alterar a foto de um usuário.</param>
        /// <response code="200">Retorna Url da foto que foi criada.</response>
        [ProducesResponseType(typeof(AlterarFotoComandoResultado), 200)]
        [HttpPut]
        [Route("v1/Usuarios/{usuarioId}/[controller]")]
        public async Task<IActionResult> AlterarFotoUsuario(string usuarioId, [FromForm]AlterarFotoUsuarioComando comando)
        {
            comando.PegarIdsUsuarios(UsuarioLogadoId, usuarioId);

            var resultado = await _gerenciador.Executar(comando);
            return await Resposta(resultado, _gerenciador.Notifications);
        }

        /// <summary>
        /// Altera a foto de um ministério do sistema.
        /// </summary>
        /// <param name="ministerioId">Id do ministério que terá sua foto alterada.</param>
        /// <param name="comando">Comando para alterar a foto de um ministério.</param>
        /// <response code="200">Retorna Url da foto que foi criada.</response>
        [ProducesResponseType(typeof(AlterarFotoComandoResultado), 200)]
        [HttpPut]
        [Route("v1/Ministerios/{ministerioId}/[controller]")]
        public async Task<IActionResult> AlterarFotoMinisterio(string ministerioId, [FromForm]AlterarFotoMinisterioComando comando)
        {
            comando.PegarIds(UsuarioLogadoId, ministerioId);

            var resultado = await _gerenciador.Executar(comando);
            return await Resposta(resultado, _gerenciador.Notifications);
        }
    }
}
