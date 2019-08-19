using LouveApp.Compartilhado.Comandos;
using LouveApp.Dominio.Entidades;

namespace LouveApp.Dominio.Comandos.MusicaComandos.Saidas
{
    public class AtualizarMusicaComandoResultado : IComandoResultado
    {
        #region Propriedades Api

        public string Id { get; set; }
        public string Nome { get; set; }
        public string Letra { get; set; }
        public string Cifra { get; set; }
        public string Video { get; set; }
        public string Artista { get; set; }
        public string Tom { get; set; }
        public int? Bpm { get; set; }
        public string Classificacao { get; set; }

        #endregion

        #region Construtores

        public AtualizarMusicaComandoResultado(Musica musica)
        {
            Id = musica.Id;
            Nome = musica.ToString();
            Letra = musica.Letra.ToString();
            Cifra = musica.Cifra.ToString();
            Video = musica.Video.ToString();
            Artista = musica.Artista.ToString();
            Tom = musica.Tom.ToString();
            Bpm = musica.Bpm;
            Classificacao = musica.Classificacao;
        }

        #endregion
    }
}
