using System.Collections.Generic;
using System.Linq;
using FluentValidator;
using LouveApp.Compartilhado.Comandos;
using LouveApp.Dominio.Entidades;
using LouveApp.Dominio.ValueObjects;

namespace LouveApp.Dominio.Comandos.MusicaComandos.Entradas
{
    public class RegistrarMusicaComando : IComando
    {
        #region Propriedades Api

        public string Nome { get; set; }
        public string Referencia { get; set; }
        internal string UsuarioLogadoId { get; private set; }
        internal string MinisterioId { get; private set; }
        public string Artista { get; set; }
        public string Tom { get; set; }
        public int? Bpm { get; set; }
        public string Classificacao { get; set; }

        #endregion

        #region Construtores

        public RegistrarMusicaComando(string nome, string referencia, string artista
            , string tom, int bpm, string classificacao)
        {
            Nome = nome;
            Referencia = referencia;
            Artista = artista;
            Tom = tom;
            Bpm = bpm;
            Classificacao = classificacao;
        }

        #endregion

        #region IComando

        public bool FoiValidado { get; private set; }

        public bool Validar()
        {
            var nome = new Nome(Nome);
            var referencia = new Link(Referencia);
            var artista = new Nome(Artista);

            Musica = new Musica(nome, referencia, artista, Tom, Bpm, Classificacao);
            _notificacoes.AddRange(Musica.Notifications);
            FoiValidado = true;

            return !_notificacoes.Any();
        }

        private readonly List<Notification> _notificacoes = new List<Notification>();

        public IReadOnlyCollection<Notification> PegarNotificacoes() => _notificacoes;

        #endregion

        #region Entidade

        internal Musica Musica { get; private set; }

        #endregion

        public void PegarIds(string usuarioLogadoId, string ministerioId)
        {
            UsuarioLogadoId = usuarioLogadoId;
            MinisterioId = ministerioId;
        }
    }
}
