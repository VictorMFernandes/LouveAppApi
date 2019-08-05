using LouveApp.Compartilhado.Comandos;

namespace LouveApp.Dominio.Comandos.InstrumentoComandos.Saidas
{
    public class PegarInstrumentosComandoResultado : IComandoResultado
    {
        #region Propriedades

        public string Id { get; set; }
        public string Nome { get; set; }

        #endregion
    }
}
