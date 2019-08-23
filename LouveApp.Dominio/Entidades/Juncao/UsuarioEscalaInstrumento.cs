namespace LouveApp.Dominio.Entidades.Juncao
{
    public class UsuarioEscalaInstrumento
    {
        public string UsuarioEscalaId { get; private set; }
        public string InstrumentoId { get; private set; }

        public UsuarioEscalaInstrumento()
        {
            
        }

        public UsuarioEscalaInstrumento(string instrumentoId)
        {
            InstrumentoId = instrumentoId;
        }
    }
}
