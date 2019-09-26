using System.Collections.Generic;
using FluentValidator;
using LouveApp.Compartilhado.Comandos;

namespace LouveApp.Dominio.Comandos.UsuarioComandos.Entradas
{
    public class RemoverDispositivoComando : IComando
    {
        #region Propriedades Api

        public string Token { get; set; }
        internal string UsuarioLogadoId { get; private set; }

        #endregion

        #region Construtores

        public RemoverDispositivoComando(string token)
        {
            Token = token;
        }

        #endregion

        #region IComando

        public bool FoiValidado { get; private set; }

        public bool Validar()
        {
            FoiValidado = true;

            return true;
        }

        public IReadOnlyCollection<Notification> PegarNotificacoes()
        {
            var resultado = new List<Notification>();

            return resultado;
        }

        #endregion

        public void PegarUsuarioLogadoId(string usuarioLogadoId)
        {
            UsuarioLogadoId = usuarioLogadoId;
        }
    }
}
