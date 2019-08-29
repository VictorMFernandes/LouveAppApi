using LouveApp.Compartilhado.Comandos;
using FluentValidator;
using System.Collections.Generic;
using System.Linq;
using LouveApp.Dominio.ValueObjects;

namespace LouveApp.Dominio.Comandos.MusicaComandos.Entradas
{
    public class AtualizarMusicaComando : IComando
    {
        #region Propriedades Api

        public string Nome { get; set; }
        public string Letra { get; set; }
        public string Cifra { get; set; }
        public string Video { get; set; }
        public string Artista { get; set; }
        public string Tom { get; set; }
        public int? Bpm { get; set; }
        public string Classificacao { get; set; }
        internal string UsuarioLogadoId { get; private set; }
        internal string MusicaId { get; private set; }

        #endregion

        #region Construtores

        public AtualizarMusicaComando(string nome, string letra
            , string cifra, string video, string artista, string tom
            , int? bpm, string classificacao)
        {
            Nome = nome;
            Letra = letra;
            Cifra = cifra;
            Video = video;
            Artista = artista;
            Tom = tom;
            Bpm = bpm;
            Classificacao = classificacao;
        }

        #endregion

        #region IComando

        public bool FoiValidado { get; set; }

        public bool Validar()
        {
            FoiValidado = true;

            NomeVo = new Nome(Nome);
            LetraVo = new Link(Letra);
            CifraVo = new Link(Cifra);
            VideoVo = new Link(Video);
            ArtistaVo = new Nome(Artista);

            _notificacoes.AddRange(NomeVo.Notifications);
            _notificacoes.AddRange(LetraVo.Notifications);
            _notificacoes.AddRange(CifraVo.Notifications);
            _notificacoes.AddRange(VideoVo.Notifications);
            _notificacoes.AddRange(ArtistaVo.Notifications);

            return !_notificacoes.Any();
        }

        private readonly List<Notification> _notificacoes = new List<Notification>();

        public IReadOnlyCollection<Notification> PegarNotificacoes() => _notificacoes;

        #endregion

        #region Value Objects

        internal Nome NomeVo;
        internal Link LetraVo;
        internal Link CifraVo;
        internal Link VideoVo;
        internal Nome ArtistaVo;

        #endregion

        public void PegarUsuarioLogadoId(string usuarioLogadoId, string musicaId)
        {
            UsuarioLogadoId = usuarioLogadoId;
            MusicaId = musicaId;
        }
    }
}
