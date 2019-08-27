using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentValidator;
using LouveApp.Compartilhado.Comandos.Genericos;
using LouveApp.Compartilhado.Transacoes;
using Microsoft.AspNetCore.Mvc;

namespace LouveApp.Compartilhado.Entidades
{
    [ApiController]
    public abstract class ControladorApi : Controller
    {
        protected string UsuarioLogadoId => User.FindFirst(ClaimTypes.NameIdentifier).Value;

        private readonly IUow _uow;

        protected ControladorApi(IUow uow)
        {
            _uow = uow;
        }

        protected IActionResult RespostaDeConsulta(object resultado)
        {
            return Ok(new
            {
                Sucesso = true,
                Resultado = resultado
            });
        }

        protected async Task<IActionResult> Resposta(object resultado
            , IEnumerable<Notification> notificacoes)
        {
            if (resultado is IComandoResultadoGenerico resultadoPadrao)
            {
                return StatusCode(
                    (int)resultadoPadrao.CodigoHttp
                    , new
                    {
                        resultadoPadrao.Sucesso,
                        Resultado = (object)resultadoPadrao.Resultado
                    });
            }

            var notificacoesEnumeradas = notificacoes as Notification[] ?? notificacoes.ToArray();

            if (!notificacoesEnumeradas.Any())
            {
                await _uow.Salvar();
                return Ok(new
                {
                    Sucesso = true,
                    Resultado = resultado
                });
            }
            else
            {
                return BadRequest(new
                {
                    Sucesso = false,
                    Erros = notificacoesEnumeradas
                });
            }
        }
    }
}
