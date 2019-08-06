using System.Collections.Generic;
using LouveApp.Compartilhado.Comandos;
using LouveApp.Dominio.Comandos.InstrumentoComandos.Saidas;

namespace LouveApp.Dominio.Comandos.UsuarioComandos.Saidas
{
    public class AtualizarUsuarioComandoResultado : IComandoResultado
    {
        #region Propriedades Api

        public string Nome { get; }
        public IEnumerable<PegarInstrumentosComandoResultado> Instrumentos { get; }

        #endregion

        #region Construtores

        public AtualizarUsuarioComandoResultado(string nome, IEnumerable<PegarInstrumentosComandoResultado> instrumentos)
        {
            Nome = nome;
            Instrumentos = instrumentos;
        }

        #endregion
    }
}
