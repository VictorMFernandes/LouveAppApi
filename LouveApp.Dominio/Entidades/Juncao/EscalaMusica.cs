namespace LouveApp.Dominio.Entidades.Juncao
{
    public sealed class EscalaMusica
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

        public string MusicaId { get; private set; }
        private Musica _musica;
        public Musica Musica
        {
            get => _musica;
            private set
            {
                if (value != null) MusicaId = value.Id;
                _musica = value;
            }
        }

        #endregion

        #region Construtores

        private EscalaMusica() { }

        public EscalaMusica(string musicaId)
        {
            MusicaId = musicaId;
        }

        #endregion
    }
}
