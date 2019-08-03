using System;
using System.Collections.Generic;
using LouveApp.Compartilhado.Comandos;

namespace LouveApp.Dominio.Comandos.UsuarioComandos.Saidas
{
    public class PegarEscalaComandoResultado : IComandoResultado
    {
        #region Propriedades
        public string Id { get; set; }
        public DateTime Data { get; set; }
        public IEnumerable<PegarUsuarioComandoResultado> Usuarios { get; set; }

        #endregion
    }
}
