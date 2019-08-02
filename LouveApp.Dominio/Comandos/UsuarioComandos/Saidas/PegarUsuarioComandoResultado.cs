using System.Collections.Generic;
using LouveApp.Compartilhado.Comandos;
using LouveApp.Dominio.Comandos.InstrumentoComandos.Saidas;

namespace LouveApp.Dominio.Comandos.UsuarioComandos.Saidas
{
    public class PegarUsuarioComandoResultado : IComandoResultado
    {
        #region Propriedades
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string FotoUrl { get; set; }
        public string DtCriacao { get; set; }
        public IEnumerable<PegarInstrumentosComandoResultado> Instrumentos { get; set; }

        #endregion
    }
}
