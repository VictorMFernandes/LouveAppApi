﻿using Dapper;
using LouveApp.Dal.Contexto;
using LouveApp.Dal.Mapeamentos;
using LouveApp.Dal.Mapeamentos.Juncao;
using LouveApp.Dominio.Comandos.InstrumentoComandos.Saidas;
using LouveApp.Dominio.Comandos.MinisterioComandos.Saidas;
using LouveApp.Dominio.Comandos.UsuarioComandos.Saidas;
using LouveApp.Dominio.Entidades;
using LouveApp.Dominio.Repositorios;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace LouveApp.Dal.Repositorios
{
    public class MinisterioRepositorio : IMinisterioRepositorio
    {
        private readonly BancoContexto _contexto;
        private readonly IDbConnection _conexao;

        public MinisterioRepositorio(BancoContexto contexto, IDbConnection conexao)
        {
            _contexto = contexto;
            _conexao = conexao;
        }

        public async Task<Ministerio> PegarPorId(string ministerioId)
        {
            return await _contexto
                        .Ministerios
                        .Include(m => m.Usuarios)
                        .ThenInclude(um => um.Usuario)
                        .Include(m => m.Escalas)
                        .FirstOrDefaultAsync(x => x.Id == ministerioId);
        }

        public async Task<Ministerio> PegarPorIdComMusicas(string ministerioId)
        {
            return await _contexto
                        .Ministerios
                        .Include(m => m.Usuarios)
                        .ThenInclude(um => um.Usuario)
                        .Include(m => m.Musicas)
                        .FirstOrDefaultAsync(x => x.Id == ministerioId);
        }

        public async Task<Ministerio> PegarPorIdComUsuariosEDispositivos(string ministerioId)
        {
            return await _contexto
                        .Ministerios
                        .Include(m => m.Usuarios)
                        .ThenInclude(um => um.Usuario)
                        .ThenInclude(u => u.Dispositivos)
                        .FirstOrDefaultAsync(x => x.Id == ministerioId);
        }

        public async Task<Ministerio> PegarPorLinkConvite(string linkConvite)
        {
            return await _contexto
                .Ministerios
                .Include(m => m.Usuarios)
                .ThenInclude(um => um.Usuario)
                .FirstOrDefaultAsync(x => x.LinkConvite == linkConvite || x.LinkConviteAtivado);
        }

        public async Task<IEnumerable<PegarMinisterioComandoResultado>> PegarPorUsuario(string id)
        {
            var query = $"SELECT m.Id, m.Nome, um.Administrador FROM {MinisterioMap.Tabela} AS m " +
                        $"INNER JOIN {UsuarioMinisterioMap.Tabela} AS um ON m.Id = um.MinisterioId " +
                        $"WHERE um.UsuarioId = @{nameof(id)}";

            return await _conexao.QueryAsync<PegarMinisterioComandoResultado>(query, new { id });
        }

        public void Criar(Ministerio ministerio)
        {
            _contexto.Ministerios.Add(ministerio);
        }

        public void Atualizar(Ministerio ministerio)
        {
            _contexto.Entry(ministerio).State = EntityState.Modified;
        }

        public void Remover(Ministerio ministerio)
        {
            _contexto.Ministerios.Remove(ministerio);
        }

        public async Task<bool> EAdministrador(string usuarioId, string ministerioId)
        {
            var query = $"SELECT Administrador FROM {UsuarioMinisterioMap.Tabela} " +
                        $"WHERE UsuarioId = @{nameof(usuarioId)} AND MinisterioId = @{nameof(ministerioId)}";

            return await _conexao.ExecuteScalarAsync<bool>(query, new { usuarioId, ministerioId });
        }

        public async Task<int> Contar()
        {
            return await _conexao.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM {MinisterioMap.Tabela}");
        }

        public async Task<IEnumerable<PegarUsuarioComandoResultado>> PegarUsuarios(string ministerioId)
        {
            var query = $"SELECT u.Id, u.Nome, u.Email, u.FotoUrl, u.DtCriacao FROM {UsuarioMap.Tabela} AS u " +
                        $"INNER JOIN {UsuarioMinisterioMap.Tabela} AS um ON um.UsuarioId = u.Id " +
                        $"INNER JOIN {MinisterioMap.Tabela} AS m ON um.MinisterioId = m.Id " +
                        $"WHERE m.Id = @{nameof(ministerioId)}";

            using (var conn = _conexao)
            {
                conn.Open();

                var resultado = await conn.QueryAsync<PegarUsuarioComandoResultado>(query, new { ministerioId });

                if (resultado == null) return null;

                foreach (var usuario in resultado)
                {
                    query = $"SELECT i.Id, i.Nome FROM {InstrumentoMap.Tabela} AS i " +
                            $"INNER JOIN {UsuarioInstrumentoMap.Tabela} AS ui ON ui.InstrumentoId = i.Id " +
                            $"WHERE ui.UsuarioId = '{usuario.Id}'";

                    usuario.Instrumentos = await conn.QueryAsync<PegarInstrumentosComandoResultado>(query);
                }

                return resultado;
            }
        }
    }
}
