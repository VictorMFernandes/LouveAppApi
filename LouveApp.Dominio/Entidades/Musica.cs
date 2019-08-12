using LouveApp.Compartilhado.Entidades;
using LouveApp.Dominio.Entidades.Juncao;
using LouveApp.Dominio.ValueObjects;
using System.Collections.Generic;
using FluentValidator;
using FluentValidator.Validation;
using LouveApp.Compartilhado.Padroes;

namespace LouveApp.Dominio.Entidades
{
    public sealed class Musica : Entidade
    {
        #region Propriedades

        public Nome Nome { get; private set; }
        public Link Referencia { get; private set; }
        public string MinisterioId { get; private set; }
        public Ministerio Ministerio { get; private set; }
        public ICollection<EscalaMusica> Escalas { get; private set; }
        public Nome Artista { get; private set; }
        public string Tom { get; private set; }
        public int? Bpm { get; private set; }
        public string Classificacao { get; private set; }

        #endregion

        #region Construtores

        private Musica() { }

        public Musica(Nome nome, Link referencia, Nome artista
            , string tom, int? bpm, string classificacao)
        {
            Nome = nome;
            Referencia = referencia;
            Artista = artista?? new Nome(string.Empty);
            Tom = tom;
            Bpm = bpm;
            Classificacao = classificacao;

            Validar();
        }

        public Musica(string id, Nome nome, Link referencia, Nome artista
            , string tom, int? bpm, string classificacao)
            : this(nome, referencia, artista, tom, bpm, classificacao)
        {
            Id = id;
        }

        #endregion

        #region Métodos de Sobrescrita

        public override string ToString()
        {
            return Nome.ToString();
        }

        protected override void InicializarColecoes()
        {
            Escalas = new List<EscalaMusica>();
        }

        protected override void Validar()
        {
            if (Referencia != null)
                AddNotifications(Referencia);

            if (Nome != null)
                AddNotifications(Nome);
            else 
                AddNotification(new Notification(nameof(Nome)
                    , PadroesMensagens.PropriedadeNaoPodeSerNula));

            if (!string.IsNullOrEmpty(Artista?.ToString()))
                AddNotifications(Artista);

            var contrato = new ValidationContract()
                .HasMaxLen(Tom
                    , PadroesTamanho.MaxTom
                    , nameof(Tom)
                    , string.Format(PadroesMensagens.TomMaxTamanho, PadroesTamanho.MaxTom))
                
                .HasMaxLen(Classificacao
                    , PadroesTamanho.MaxMusicaClassificacao
                    , nameof(Classificacao)
                    , string.Format(PadroesMensagens.ClassificacaoMaxTamanho, PadroesTamanho.MaxMusicaClassificacao));

            if (Bpm != null)
            {
                contrato.HasMaxLen(Bpm.ToString()
                    , PadroesTamanho.MaxBpm
                    , nameof(Bpm)
                    , string.Format(PadroesMensagens.BpmMaxTamanho, PadroesTamanho.MaxBpm));
            }

            AddNotifications(contrato);
        }

        #endregion
    }
}
