using LouveApp.Compartilhado.Entidades;

namespace LouveApp.Dominio.Entidades.Juncao
{
    public sealed class UsuarioInstrumento : EntidadeJuncao
    {
        #region Propridades

        public string UsuarioId { get; private set; }
        private Usuario _usuario;
        public Usuario Usuario
        {
            get => _usuario;
            private set
            {
                if (value != null) UsuarioId = value.Id;
                _usuario = value;
            }
        }
        public string InstrumentoId { get; private set; }
        private Instrumento _instrumento;
        public Instrumento Instrumento
        {
            get => _instrumento;
            private set
            {
                if (value != null) InstrumentoId = value.Id;
                _instrumento = value;
            }
        }

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
