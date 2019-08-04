using System.Collections.Generic;
using FluentValidator;
using LouveApp.Compartilhado.Comandos;
using LouveApp.Dominio.ValueObjects;

namespace LouveApp.Dominio.Comandos.MusicaComandos.Entradas
{
    public class RegistrarMusicaComando : IComando
    {
        #region Propriedades Api

        public string Nome { get; set; }
        public string Referencia { get; set; }
        internal string UsuarioLogadoId { get; private set; }
        internal string MinisterioId { get; private set; }

        #endregion

        #region Construtores

        public RegistrarMusicaComando(string nome, string referencia)
        {
            Nome = nome;
            Referencia = referencia;
        }

        #endregion

        #region IComando

        public bool FoiValidado { get; private set; }

        public bool Validar()
        {
            NomeVo = new Nome(Nome);
            ReferenciaVo = new Link(Referencia);

            FoiValidado = true;

            return NomeVo.Valid && ReferenciaVo.Valid;
        }

        public IReadOnlyCollection<Notification> PegarNotificacoes()
        {
            var resultado = new List<Notification>();

            resultado.AddRange(NomeVo.Notifications);
            resultado.AddRange(ReferenciaVo.Notifications);

            return resultado;
        }

        #endregion

        #region Value Objects

        internal Nome NomeVo { get; set; }
        internal Link ReferenciaVo { get; set; }

        #endregion

        public void PegarIds(string usuarioLogadoId, string ministerioId)
        {
            UsuarioLogadoId = usuarioLogadoId;
            MinisterioId = ministerioId;
        }
    }
}
