namespace LouveApp.Dominio.Entidades.Juncao
{
    public class UsuarioEscala : EntidadeJuncaoComUsuario
    {
        #region Propriedades

        public string EscalaId { get; private set; }
        private Escala _escala;
        public Escala Escala
        {
            get => _escala;
            private set
            {
                if (value != null) EscalaId = value.Id;
                _escala = value;
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

        private UsuarioEscala() { }

        public UsuarioEscala(string usuarioId)
        {
            UsuarioId = usuarioId;
        }

        #endregion
    }
}
