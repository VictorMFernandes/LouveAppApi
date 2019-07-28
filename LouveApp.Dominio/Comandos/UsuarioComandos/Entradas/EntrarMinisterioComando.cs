using System.Collections.Generic;
using System.Linq;
using FluentValidator;
using LouveApp.Compartilhado.Comandos;

namespace LouveApp.Dominio.Comandos.UsuarioComandos.Entradas
{
    public class EntrarMinisterioComando : IComando
    {
        #region Propriedades Api

        internal string UsuarioLogadoId { get; }
        internal string LinkConvite { get; }

        #endregion

        #region Construtores

        public EntrarMinisterioComando(string usuarioLogadoId, string linkConvite)
        {
            UsuarioLogadoId = usuarioLogadoId;
            LinkConvite = linkConvite;
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
