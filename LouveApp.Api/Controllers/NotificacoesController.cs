using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LouveApp.Dominio.Comandos.NotificacaoComandos.Entradas;
using LouveApp.Dominio.Comandos.UsuarioComandos.Entradas;
using LouveApp.Dominio.Gerenciadores;
using LouveApp.Dominio.Servicos;
using LouveApp.Infra.BancoDeDados.Transacoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LouveApp.Api.Controllers
{
    public class NotificacoesController : ControladorBase
    {
        private readonly MinisterioGerenciador _ministerioGerenciador;
        private readonly EscalaGerenciador _escalaGerenciador;

        public NotificacoesController(MinisterioGerenciador ministerioGerenciador
            , EscalaGerenciador escalaGerenciador, IUow uow)
            : base(uow)
        {
            _ministerioGerenciador = ministerioGerenciador;
            _escalaGerenciador = escalaGerenciador;
        }

        /// <summary>
        /// Envia notificações sobre uma mensagem enviada no chat de um ministério.
        /// </summary>
        /// <param name="ministerioId">Id do ministério.</param>
        /// <param name="comando">Comando para enviar as notificações.</param>
        /// <response code="200">Mensagens enviadas com sucesso.</response>
        [ProducesResponseType(200)]
        [HttpPost("v1/Ministerios/{ministerioId}/[controller]/Chat")]
        [AllowAnonymous]
        public async Task<IActionResult> NotificarChatMinisterio(string ministerioId, [FromBody]NotificarChatMinisterioComando comando)
        {
            comando.PegarIds(UsuarioLogadoId, ministerioId);

            var resultado = await _ministerioGerenciador.Executar(comando);
            return await Resposta(resultado, _ministerioGerenciador.Notifications);
        }

    }
}
