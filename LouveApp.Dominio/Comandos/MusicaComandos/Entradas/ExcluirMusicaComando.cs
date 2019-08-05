using System.Collections.Generic;
using System.Linq;
using FluentValidator;
using LouveApp.Compartilhado.Comandos;

namespace LouveApp.Dominio.Comandos.MusicaComandos.Entradas
{
    public class ExcluirMusicaComando : IComando
    {
        #region Propriedades Api

        internal string UsuarioLogadoId { get; }
        internal string MinisterioId { get; }
        internal string MusicaId { get; }

        #endregion

        #region Construtores

        public ExcluirMusicaComando(string usuarioLogadoId, string ministerioId, string musicaId)
        {
            UsuarioLogadoId = usuarioLogadoId;
            MinisterioId = ministerioId;
            MusicaId = musicaId;
        }

        #endregion

        #region IComando

        public bool FoiValidado { get; private set; }

        public bool Validar()
        {
            FoiValidado = true;

            return !_notificacoes.Any();
        }

        private readonly List<Notification> _notificacoes = new List<Notification>();
        public IReadOnlyCollection<Notification> PegarNotificacoes() => _notificacoes;

        #endregion
    }
}
