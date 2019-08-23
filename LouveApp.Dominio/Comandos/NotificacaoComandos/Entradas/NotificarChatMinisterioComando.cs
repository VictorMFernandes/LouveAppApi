using System.Collections.Generic;
using System.Linq;
using FluentValidator;
using LouveApp.Compartilhado.Comandos;

namespace LouveApp.Dominio.Comandos.NotificacaoComandos.Entradas
{
    public class NotificarChatMinisterioComando: IComando
    {
        #region Propriedades Api

        public string Mensagem { get; set; }
        internal string UsuarioLogadoId { get; private set; }
        internal string MinisterioId { get; private set; }

        #endregion

        #region Construtores

        public NotificarChatMinisterioComando(string mensagem)
        {
            Mensagem = mensagem;
        }

        #endregion

        #region IComando

        public bool FoiValidado { get; private set; }

        public bool Validar()
        {
            FoiValidado = true;

            return !Notificacoes.Any();
        }

        private static IReadOnlyCollection<Notification> Notificacoes => new List<Notification>();
        public IReadOnlyCollection<Notification> PegarNotificacoes()
        {
            return Notificacoes;
        }

        #endregion

        public void PegarIds(string usuarioLogadoId, string minsiteriId)
        {
            UsuarioLogadoId = usuarioLogadoId;
            MinisterioId = minsiteriId;
        }
    }
}
