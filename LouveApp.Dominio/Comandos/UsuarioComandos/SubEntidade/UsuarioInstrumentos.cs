using System.Collections.Generic;
using System.Linq;

namespace LouveApp.Dominio.Comandos.UsuarioComandos.SubEntidade
{
    public class UsuarioInstrumentos
    {
        #region Propriedades Api

        public string UsuarioId { get; set; }

        public IEnumerable<string> InstrumentosIds { get; set; }

        #endregion

        #region Construtores

        public UsuarioInstrumentos(string usuarioId, IEnumerable<string> instrumentosIds)
        {
            UsuarioId = usuarioId;
            InstrumentosIds = instrumentosIds.Distinct();
        }

        #endregion
    }
}
