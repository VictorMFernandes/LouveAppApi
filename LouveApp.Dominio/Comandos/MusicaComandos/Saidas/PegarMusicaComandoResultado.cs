using LouveApp.Compartilhado.Comandos;

namespace LouveApp.Dominio.Comandos.MusicaComandos.Saidas
{
    public class PegarMusicaComandoResultado : IComandoResultado
    {
        #region Propriedades

        public string Id { get; set; }
        public string Nome { get; set; }
        public string Referencia { get; set; }

        #endregion
    }
}
