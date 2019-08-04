using System;
using System.Collections.Generic;
using FluentValidator;
using LouveApp.Compartilhado.Comandos;
using LouveApp.Dominio.Entidades;

namespace LouveApp.Dominio.Comandos.EscalaComandos.Entradas
{
    public class RegistrarEscalaComando : IComando
    {
        #region Propriedades Api

        public DateTime Data { get; set; }
        public IEnumerable<string> UsuariosIds { get; set; }
        public IEnumerable<string> MusicasIds { get; set; }
        internal string UsuarioLogadoId { get; private set; }
        internal string MinisterioId { get; private set; }

        #endregion

        #region Construtores

        public RegistrarEscalaComando(DateTime data, IEnumerable<string> usuariosIds, IEnumerable<string> musicasIds)
        {
            Data = data;
            UsuariosIds = usuariosIds;
            MusicasIds = musicasIds;
        }

        #endregion

        #region IComando

        public bool FoiValidado { get; private set; }

        public bool Validar()
        {
            FoiValidado = true;

            return true;
        }

        public IReadOnlyCollection<Notification> PegarNotificacoes() => new List<Notification>();

        #endregion

        public void PegarIds(string usuarioLogadoId, string ministerioId)
        {
            UsuarioLogadoId = usuarioLogadoId;
            MinisterioId = ministerioId;
        }
    }
}
