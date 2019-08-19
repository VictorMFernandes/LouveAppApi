using LouveApp.Compartilhado.Comandos;

namespace LouveApp.Dominio.Comandos.MusicaComandos.Saidas
{
    public class PegarMusicaComandoResultado : IComandoResultado
    {
        #region Propriedades

        public string Id { get; set; }
        public string Nome { get; set; }
        public string Artista { get; set; }
        public string Letra { get; set; }
        public string Cifra { get; set; }
        public string Video { get; set; }

        #endregion
    }
}
