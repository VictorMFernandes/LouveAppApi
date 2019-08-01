using LouveApp.Compartilhado.Entidades;

namespace LouveApp.Dominio.Entidades.Juncao
{
    public sealed class UsuarioInstrumento : EntidadeJuncao
    {
        #region Propridades

        public string UsuarioId { get; private set; }
        public Usuario Usuario { get; set; }
        public string InstrumentoId { get; private set; }
        public Instrumento Instrumento { get; private set; }

        #endregion

        #region Construtores

        private UsuarioInstrumento() { }

        public UsuarioInstrumento(string instrumentoId)
        {
            InstrumentoId = instrumentoId;
        }

        #endregion
    }
}
