using LouveApp.Compartilhado.Entidades;
using LouveApp.Dominio.Entidades.Juncao;
using LouveApp.Dominio.ValueObjects;
using System.Collections.Generic;

namespace LouveApp.Dominio.Entidades
{
    public class Instrumento : Entidade
    {
        #region Propriedades

        public Nome Nome { get; private set; }
        public ICollection<UsuarioInstrumento> Usuarios { get; private set; }

        #endregion

        #region Construtores

        private Instrumento() { }

        public Instrumento(Nome nome)
        {
            Nome = nome;

            Validar();
        }

        public Instrumento(string id, Nome nome)
            : this(nome)
        {
            Id = id;
        }

        #endregion

        #region Métodos de Sobrescrita

        protected override void InicializarColecoes()
        {
            Usuarios = new List<UsuarioInstrumento>();
        }

        protected override void Validar()
        {
            AddNotifications(Nome);
        }

        public override string ToString()
        {
            return Nome.ToString();
        }

        #endregion
    }
}
