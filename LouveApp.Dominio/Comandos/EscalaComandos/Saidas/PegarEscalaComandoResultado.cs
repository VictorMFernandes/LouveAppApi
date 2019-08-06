using System;
using System.Collections.Generic;
using LouveApp.Compartilhado.Comandos;
using LouveApp.Dominio.Comandos.MinisterioComandos.Saidas;
using LouveApp.Dominio.Comandos.UsuarioComandos.Saidas;

namespace LouveApp.Dominio.Comandos.EscalaComandos.Saidas
{
    public class PegarEscalaComandoResultado : IComandoResultado
    {
        #region Propriedades
        public string Id { get; set; }
        public DateTime Data { get; set; }
        public PegarMinisterioComandoResultado Ministerio { get; set; }
        public IEnumerable<PegarUsuarioComandoResultado> Usuarios { get; set; }
        public long QtdMusicas { get; set; }

        #endregion
    }
}
