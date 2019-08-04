using LouveApp.Compartilhado.Entidades;
using LouveApp.Dominio.Entidades.Juncao;
using LouveApp.Dominio.ValueObjects;
using System.Collections.Generic;

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

        #endregion

        #region Construtores

        private Musica() { }

        public Musica(Nome nome, Link referencia)
        {
            Nome = nome;
            Referencia = referencia;

            Validar();
        }

        public Musica(string id, Nome nome, Link referencia)
            : this(nome, referencia)
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
            AddNotifications(Nome, Referencia);
        }

        #endregion
    }
}
