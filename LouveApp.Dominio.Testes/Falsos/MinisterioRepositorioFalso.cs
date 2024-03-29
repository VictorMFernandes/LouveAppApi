﻿using System.Collections.Generic;
using System.Threading.Tasks;
using LouveApp.Dominio.Comandos.MinisterioComandos.Saidas;
using LouveApp.Dominio.Comandos.UsuarioComandos.Saidas;
using LouveApp.Dominio.Entidades;
using LouveApp.Dominio.Repositorios;

namespace LouveApp.Dominio.Testes.Falsos
{
    internal class MinisterioRepositorioFalso : IMinisterioRepositorio
    {
        public void Atualizar(Ministerio ministerio) { }

        public Task Remover(string ministerioId) => null;

        public Task<bool> EAdministrador(string usuarioId, string ministerioId) => Task.Run(() => false);

        public Task<IEnumerable<PegarMinisterioComandoResultado>> PegarPorUsuario(string id) => null;

        public void Criar(Ministerio ministerio) { }

        public Task<Ministerio> PegarPorId(string ministerioId) => null;
        public Task<Ministerio> PegarPorIdComMusicas(string ministerioId) => null;
        public Task<Ministerio> PegarPorIdComUsuariosEDispositivos(string ministerioId)
        {
            return null;
        }

        public Task<Ministerio> PegarPorLinkConvite(string linkConvite) => null;

        public Task<int> Contar() => Task.Run(() => 0);

        public void Remover(Ministerio ministerio) { }

        public Task<IEnumerable<PegarUsuarioComandoResultado>> PegarUsuarios(string ministerioId) => null;
    }
}
