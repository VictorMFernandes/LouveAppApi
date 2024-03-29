﻿using LouveApp.Compartilhado.Comandos;
using LouveApp.Compartilhado.Comandos.Genericos;
using LouveApp.Dominio.Comandos.EscalaComandos.Entradas;
using LouveApp.Dominio.Comandos.EscalaComandos.Saidas;
using LouveApp.Dominio.Repositorios;
using LouveApp.Dominio.Servicos;
using LouveApp.Dominio.Sistema.Padroes;
using System.Linq;
using System.Threading.Tasks;

namespace LouveApp.Dominio.Gerenciadores
{
    public class EscalaGerenciador : Gerenciador
        , IComandoGerenciador<RegistrarEscalaComando>
        , IComandoGerenciador<ExcluirEscalaComando>
    {
        private readonly IMinisterioRepositorio _ministerioRepo;
        private readonly IDispositivoRepositorio _dispositivoRepo;
        private readonly IPushNotificationServico _pushNotificationServico;

        public EscalaGerenciador(IMinisterioRepositorio ministerioRepo
            , IDispositivoRepositorio dispositivoRepo
            , IPushNotificationServico pushNotificationServico)
        {
            _ministerioRepo = ministerioRepo;
            _dispositivoRepo = dispositivoRepo;
            _pushNotificationServico = pushNotificationServico;
        }

        public async Task<IComandoResultado> Executar(RegistrarEscalaComando comando)
        {
            if (!ValidarComando(comando))
                return null;

            var ministerio = await _ministerioRepo.PegarPorIdComMusicas(comando.MinisterioId);

            if (ministerio == null)
            {
                return new NaoEncontradoResultado(PadroesMensagens.MinisterioNaoEncontrado);
            }

            // Realiza a ação
            var escala = ministerio.CriarEscala(comando.UsuarioLogadoId
                , comando.Data, comando.UsuariosInstrumentos, comando.MusicasIds);

            if (escala == null)
            {
                return new NaoAutorizadoResultado(PadroesMensagens.UsuarioSemPermissao);
            }

            _ministerioRepo.Atualizar(ministerio);

            var usuariosIds = escala.Usuarios.Select(ue => ue.UsuarioId);

            var dispositivosTokens = await _dispositivoRepo
                                            .PegarDispositivosTokensPorUsuarioId(usuariosIds.ToList());

            _pushNotificationServico.NotificarIngressoEmEscala(dispositivosTokens.ToList()
                , escala.Data.ToString(), ministerio);

            return new RegistrarEscalaComandoResultado(escala.Id, escala.Data, escala.Usuarios.Count);
        }

        public async Task<IComandoResultado> Executar(ExcluirEscalaComando comando)
        {
            if (!ValidarComando(comando))
                return null;

            var ministerio = await _ministerioRepo.PegarPorId(comando.MinisterioId);

            //Checa se tem permissão
            if (!ministerio.Administrador(comando.UsuarioLogadoId))
                return new NaoAutorizadoResultado(PadroesMensagens.UsuarioSemPermissao);

            // Realiza a ação
            if (!ministerio.RemoverEscala(comando.UsuarioLogadoId, comando.EscalaId))
                return new NaoEncontradoResultado(PadroesMensagens.EscalaNaoEncontrada);

            _ministerioRepo.Atualizar(ministerio);

            return null;
        }
    }
}
