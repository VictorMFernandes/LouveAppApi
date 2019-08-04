﻿using System.Collections.Generic;
using LouveApp.Dominio.Gerenciadores;
using LouveApp.Infra.BancoDeDados.Transacoes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LouveApp.Dominio.Repositorios;
using LouveApp.Dominio.Comandos.MusicaComandos.Entradas;
using LouveApp.Dominio.Comandos.MusicaComandos.Saidas;

namespace LouveApp.Api.Controllers
{
    public class MusicasController : ControladorBase
    {
        private readonly MusicaGerenciador _gerenciador;
        private readonly IMusicaRepositorio _musicaRepo;

        public MusicasController(MusicaGerenciador gerenciador
            , IMusicaRepositorio musicaRepo, IUow uow)
            : base(uow)
        {
            _gerenciador = gerenciador;
            _musicaRepo = musicaRepo;
        }

        /// <summary>
        /// Registra uma música a um ministério do sistema.
        /// </summary>
        /// <param name="ministerioId">Id do ministério que receberá a música.</param>
        /// <param name="comando">Comando para registrar a música.</param>
        /// <response code="200">Retorna as principais propriedades da música que acabou de ser registrada.</response>
        [ProducesResponseType(typeof(RegistrarMusicaComandoResultado), 200)]
        [HttpPost]
        [Route("v1/Ministerios/{ministerioId}/[controller]")]
        public async Task<IActionResult> RegistrarMusica(string ministerioId, [FromBody]RegistrarMusicaComando comando)
        {
            comando.PegarIds(UsuarioLogadoId, ministerioId);

            var resultado = await _gerenciador.Executar(comando);
            return await Resposta(resultado, _gerenciador.Notifications);
        }

        /// <summary>
        /// Pega as músicas de um ministério.
        /// </summary>
        /// <response code="200">Retorna lista de músicas do ministério.</response>
        [ProducesResponseType(typeof(IEnumerable<PegarMusicaComandoResultado>), 200)]
        [HttpGet]
        [Route("v1/Ministerios/{ministerioId}/[controller]")]
        public async Task<IActionResult> PegarMusicas(string ministerioId)
        {
            return RespostaDeConsulta(await _musicaRepo.PegarPorMinisterio(ministerioId));
        }

        /// <summary>
        /// Exclui uma música do sistema.
        /// </summary>
        /// <param name="ministerioId">Id do ministério que terá uma música excluída.</param>
        /// <param name="musicaId">Id da música que será excluída.</param>
        /// <response code="200">Música excluída com sucesso.</response>
        [HttpDelete]
        [Route("v1/Ministerios/{ministerioId}/[controller]/{musicaId}")]
        public async Task<IActionResult> ExcluirMusica(string ministerioId, string musicaId)
        {
            var comando = new ExcluirMusicaComando(UsuarioLogadoId, ministerioId, musicaId);

            var resultado = await _gerenciador.Executar(comando);
            return await Resposta(resultado, _gerenciador.Notifications);
        }
    }
}