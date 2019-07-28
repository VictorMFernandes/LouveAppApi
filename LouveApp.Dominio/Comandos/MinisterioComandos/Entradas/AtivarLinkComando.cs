using System.Collections.Generic;
using System.Linq;
using FluentValidator;
using LouveApp.Compartilhado.Comandos;

namespace LouveApp.Dominio.Comandos.MinisterioComandos.Entradas
{
    public class AtivarLinkComando : IComando
    {
        #region Propriedades Api

        internal string UsuarioLogadoId { get; private set; }
        internal string MinisterioId { get; private set; }

        #endregion

        #region Construtores

        public AtivarLinkComando(string usuarioLogadoId, string ministerioId)
        {
            UsuarioLogadoId = usuarioLogadoId;
            MinisterioId = ministerioId;
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
