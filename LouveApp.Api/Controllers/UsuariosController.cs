﻿using LouveApp.Dominio.Comandos.UsuarioComandos.Entradas;
using LouveApp.Dominio.Comandos.UsuarioComandos.Saidas;
using LouveApp.Dominio.Gerenciadores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LouveApp.Dominio.Repositorios;
using LouveApp.Compartilhado.Transacoes;
using LouveApp.Compartilhado.Entidades;

namespace LouveApp.Api.Controllers
{
    public class UsuariosController : ControladorApi
    {
        private readonly UsuarioGerenciador _gerenciador;
        private readonly IUsuarioRepositorio _usuarioRepo;

        public UsuariosController(UsuarioGerenciador gerenciador, IUsuarioRepositorio usuarioRepo, IUow uow)
            : base(uow)
        {
            _gerenciador = gerenciador;
            _usuarioRepo = usuarioRepo;
        }

        /// <summary>
        /// Registra um usuário no sistema.
        /// </summary>
        /// <param name="comando">Comando para registrar usuário no sistema</param>
        /// <remarks>E-mail deve ser único.</remarks>
        /// <response code="200">Retorna as principais propriedades do usuário que acabou de ser registrado.</response>
        [ProducesResponseType(typeof(RegistrarUsuarioComandoResultado), 200)]
        [HttpPost("v1/[controller]")]
        [AllowAnonymous]
        public async Task<IActionResult> RegistrarUsuario([FromBody]RegistrarUsuarioComando comando)
        {
            var resultado = await _gerenciador.Executar(comando);
            return await Resposta(resultado, _gerenciador.Notifications);
        }

        /// <summary>
        /// Atualiza um usuário do sistema.
        /// </summary>
        /// <param name="comando">Comando para atualizar usuário no sistema</param>
        /// <remarks>Ids de instrumentos devem ser únicos.</remarks>
        /// <response code="200">Retorna as principais propriedades do usuário que acabou de ser atualizado.</response>
        [ProducesResponseType(typeof(AtualizarUsuarioComandoResultado), 200)]
        [HttpPut("v1/[controller]")]
        public async Task<IActionResult> AtualizarUsuario([FromBody]AtualizarUsuarioComando comando)
        {
            comando.PegarUsuarioLogadoId(UsuarioLogadoId);

            var resultado = await _gerenciador.Executar(comando);
            return await Resposta(resultado, _gerenciador.Notifications);
        }

        /// <summary>
        /// Pega os dados do usuário que está logado.
        /// </summary>
        /// <response code="200">Retorna as principais propriedades do usuário logado no sistema.</response>
        [ProducesResponseType(typeof(AtualizarUsuarioComandoResultado), 200)]
        [HttpGet("v1/[controller]")]
        public async Task<IActionResult> PegarUsuario()
        {
            return RespostaDeConsulta(await _usuarioRepo.PegarPorIdSemRastrear(UsuarioLogadoId));
        }

        /// <summary>
        /// Entra em um ministério a partir de um link de convite.
        /// </summary>
        /// <param name="linkConvite">Link convite para entrar em um ministério.</param>
        /// <response code="200">Retorna as principais propriedades do ministério que o usuário entrou.</response>
        [ProducesResponseType(typeof(EntrarMinisterioComandoResultado), 200)]
        [HttpPost("v1/[controller]/EntrarMinisterio/{linkConvite}")]
        [AllowAnonymous]
        public async Task<IActionResult> EntrarMinisterio(string linkConvite)
        {
            var comando = new EntrarMinisterioComando(UsuarioLogadoId, linkConvite);

            var resultado = await _gerenciador.Executar(comando);
            return await Resposta(resultado, _gerenciador.Notifications);
        }

        /// <summary>
        /// Adiciona um dispositivo a um usuário.
        /// </summary>
        /// <remarks>Dispositivos de mesmo token só serão adicionados uma vez.</remarks>
        /// <param name="comando">Comando para adicionar dispositivo ao usuário.</param>
        /// <response code="200">Retorna as principais propriedades do usuário que acabou de ser atualizado.</response>
        [ProducesResponseType(typeof(AtualizarUsuarioComandoResultado), 200)]
        [HttpPut("v1/[controller]/AdicionarDispositivo")]
        public async Task<IActionResult> AdicionarDispositivo([FromBody]AdicionarDispositivoComando comando)
        {
            comando.PegarUsuarioLogadoId(UsuarioLogadoId);

            var resultado = await _gerenciador.Executar(comando);
            return await Resposta(resultado, _gerenciador.Notifications);
        }
    }
}
