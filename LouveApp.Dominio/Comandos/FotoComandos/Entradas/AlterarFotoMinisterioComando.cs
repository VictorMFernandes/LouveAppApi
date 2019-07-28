using LouveApp.Compartilhado.Comandos;
using FluentValidator;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace LouveApp.Dominio.Comandos.FotoComandos.Entradas
{
    public class AlterarFotoMinisterioComando : IComando
    {
        #region Propriedades

        internal string UsuarioLogadoId { get; private set; }
        internal string MinisterioId { get; private set; }
        public IFormFile ArquivoFoto { get; set; }

        #endregion

        #region IComando

        public bool FoiValidado { get; private set; }

        public bool Validar() => FoiValidado = true;

        public IReadOnlyCollection<Notification> PegarNotificacoes() => new List<Notification>();

        #endregion

        public void PegarIds(string usuarioLogadoId, string ministerioId)
        {
            UsuarioLogadoId = usuarioLogadoId;
            MinisterioId = ministerioId;
        }
    }
}
