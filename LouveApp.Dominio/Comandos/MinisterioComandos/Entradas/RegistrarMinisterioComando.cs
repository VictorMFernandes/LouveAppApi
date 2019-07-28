using System.Collections.Generic;
using FluentValidator;
using LouveApp.Compartilhado.Comandos;
using LouveApp.Dominio.ValueObjects;

namespace LouveApp.Dominio.Comandos.MinisterioComandos.Entradas
{
    public class RegistrarMinisterioComando : IComando
    {
        #region Propriedades Api

        public string Nome { get; set; }
        internal string UsuarioLogadoId { get; private set; }

        #endregion

        #region Construtores

        public RegistrarMinisterioComando(string nome)
        {
            Nome = nome;
        }

        #endregion

        #region IComando

        public bool FoiValidado { get; private set; }

        public bool Validar()
        {
            NomeVo = new Nome(Nome);

            FoiValidado = true;

            return NomeVo.Valid;
        }

        public IReadOnlyCollection<Notification> PegarNotificacoes()
        {
            var resultado = new List<Notification>();

            resultado.AddRange(NomeVo.Notifications);

            return resultado;
        }

        #endregion

        #region Value Objects

        internal Nome NomeVo { get; set; }

        #endregion

        public void PegarIdUsuarioLogadoEFoto(string usuarioLogadoId)
        {
            UsuarioLogadoId = usuarioLogadoId;
        }
    }
}
