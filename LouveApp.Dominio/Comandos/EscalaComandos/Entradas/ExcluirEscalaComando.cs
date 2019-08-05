using System.Collections.Generic;
using System.Linq;
using FluentValidator;
using LouveApp.Compartilhado.Comandos;

namespace LouveApp.Dominio.Comandos.EscalaComandos.Entradas
{
    public class ExcluirEscalaComando : IComando
    {
        #region Propriedades Api

        internal string UsuarioLogadoId { get; }
        internal string MinisterioId { get; }
        internal string EscalaId { get; }

        #endregion

        #region Construtores

        public ExcluirEscalaComando(string usuarioLogadoId, string ministerioId, string escalaId)
        {
            UsuarioLogadoId = usuarioLogadoId;
            MinisterioId = ministerioId;
            EscalaId = escalaId;
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
