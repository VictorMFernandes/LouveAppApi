using System.Collections.Generic;
using LouveApp.Dominio.Gerenciadores;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LouveApp.Dominio.Comandos.MinisterioComandos.Entradas;
using LouveApp.Dominio.Comandos.MinisterioComandos.Saidas;
using LouveApp.Dominio.Repositorios;
using LouveApp.Dominio.Comandos.UsuarioComandos.Saidas;
using LouveApp.Compartilhado.Transacoes;
using LouveApp.Compartilhado.Entidades;

namespace LouveApp.Api.Controllers
{
    public class MinisteriosController : ControladorApi
    {
        private readonly MinisterioGerenciador _gerenciador;
        private readonly IMinisterioRepositorio _ministerioRepo;

        public MinisteriosController(MinisterioGerenciador gerenciador
            , IMinisterioRepositorio ministerioRepo, IUow uow)
            : base(uow)
        {
            _gerenciador = gerenciador;
            _ministerioRepo = ministerioRepo;
        }

        /// <summary>
        /// Registra um ministério no sistema.
        /// </summary>
        /// <param name="comando">Comando para registrar ministério no sistema</param>
        /// <response code="200">Retorna as principais propriedades do ministério que acabou de ser registrado.</response>
        [ProducesResponseType(typeof(RegistrarMinisterioComandoResultado), 200)]
        [HttpPost("v1/[controller]")]
        public async Task<IActionResult> RegistrarMinisterio([FromBody]RegistrarMinisterioComando comando)
        {
            comando.PegarIdUsuarioLogadoEFoto(UsuarioLogadoId);

            var resultado = await _gerenciador.Executar(comando);
            return await Resposta(resultado, _gerenciador.Notifications);
        }

        /// <summary>
        /// Gera e ativa um link para ser compartilhado aos que desejam entrar no ministério.
        /// </summary>
        /// <param name="ministerioId">Id do ministério que terá o link ativado para compartilhamento.</param>
        /// <response code="200">Retorna o link de compartilhamento gerado.</response>
        [ProducesResponseType(typeof(AtivarLinkComandoResultado), 200)]
        [HttpPatch("v1/[controller]/{ministerioId}/AtivarLinkCompartilhamento")]
        public async Task<IActionResult> AtivarLinkCompartilhamento(string ministerioId)
        {
            var comando = new AtivarLinkComando(UsuarioLogadoId, ministerioId);

            var resultado = await _gerenciador.Executar(comando);
            return await Resposta(resultado, _gerenciador.Notifications);
        }

        /// <summary>
        /// Desativa o link convite do ministério.
        /// </summary>
        /// <param name="ministerioId">Id do ministério que terá o link desativado.</param>
        /// <response code="200">Link desativado com sucesso.</response>
        [HttpPatch("v1/[controller]/{ministerioId}/DesativarLinkConvite")]
        public async Task<IActionResult> DesativarLinkConvite(string ministerioId)
        {
            var comando = new DesativarLinkComando(UsuarioLogadoId, ministerioId);

            var resultado = await _gerenciador.Executar(comando);
            return await Resposta(resultado, _gerenciador.Notifications);
        }

        /// <summary>
        /// Retorna ministérios do usuário logado.
        /// </summary>
        /// <response code="200">Retorna lista de ministérios do usuário logado.</response>
        [ProducesResponseType(typeof(IEnumerable<PegarMinisterioComandoResultado>), 200)]
        [HttpGet("v1/[controller]")]
        public async Task<IActionResult> PegarMinisterios()
        {
            return RespostaDeConsulta(await _ministerioRepo.PegarPorUsuario(UsuarioLogadoId));
        }

        /// <summary>
        /// Exclui um ministério do sistema.
        /// </summary>
        /// <param name="ministerioId">Id do ministério que será excluído.</param>
        /// <response code="200">Ministério excluído com sucesso.</response>
        [HttpDelete("v1/[controller]/{ministerioId}")]
        public async Task<IActionResult> ExcluirMinisterio(string ministerioId)
        {
            var comando = new ExcluirMinisterioComando(UsuarioLogadoId, ministerioId);

            var resultado = await _gerenciador.Executar(comando);
            return await Resposta(resultado, _gerenciador.Notifications);
        }

        /// <summary>
        /// Pega os usuários vinculados a um ministério.
        /// </summary>
        /// <response code="200">Retorna lista de usuários do ministério.</response>
        [ProducesResponseType(typeof(IEnumerable<PegarUsuarioComandoResultado>), 200)]
        [HttpGet("v1/[controller]/{ministerioId}/PegarUsuarios")]
        public async Task<IActionResult> PegarUsuarios(string ministerioId)
        {
            return RespostaDeConsulta(await _ministerioRepo.PegarUsuarios(ministerioId));
        }
    }
}
