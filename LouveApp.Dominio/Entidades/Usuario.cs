using LouveApp.Compartilhado.Entidades;
using LouveApp.Dominio.Entidades.Juncao;
using LouveApp.Dominio.ValueObjects;
using System;
using System.Collections.Generic;

namespace LouveApp.Dominio.Entidades
{
    public sealed class Usuario : Entidade
    {
        #region Propriedades

        public Nome Nome { get; private set; }
        public Email Email { get; private set; }
        public Autenticacao Autenticacao { get; private set; }
        public Foto Foto { get; private set; }
        public ICollection<UsuarioMinisterio> Ministerios { get; private set; }
        public ICollection<UsuarioInstrumento> Instrumentos { get; private set; }
        public DateTime DtCriacao { get; private set; }
        public DateTime DtUltimaAtividade { get; private set; }

        #endregion

        #region Construtores

        private Usuario() { }

        public Usuario(Nome nome, Email email, Autenticacao autenticacao)
        {
            Nome = nome;
            Email = email;
            Autenticacao = autenticacao;
            Foto = new Foto(string.Empty, string.Empty);
            DtCriacao = DateTime.Now;
            DtUltimaAtividade = DateTime.Now;

            Validar();
        }

        public Usuario(string id, Nome nome, Email email, Autenticacao autenticacao)
            : this(nome, email, autenticacao)
        {
            Id = id;
        }

        #endregion

        #region Métodos de Sobrescrita

        protected override void InicializarColecoes()
        {
            Ministerios = new List<UsuarioMinisterio>();
            Instrumentos = new HashSet<UsuarioInstrumento>();
        }

        protected override void Validar()
        {
            AddNotifications(Nome, Email, Autenticacao);
        }

        public override string ToString()
        {
            return Nome.ToString();
        }

        #endregion

        public void Atualizar(Nome nome, IEnumerable<string> instrumentosIds)
        {
            Nome = nome?? Nome;

            AtualizarInstrumentos(instrumentosIds);
            DtUltimaAtividade = DateTime.Now;
        }

        public void AtualizarFoto(Foto foto)
        {
            Foto = foto;
            DtUltimaAtividade = DateTime.Now;
        }

        private void AtualizarInstrumentos(IEnumerable<string> instrumentosIds)
        {
            Instrumentos.Clear();

            foreach (var instrumentoId in instrumentosIds)
            {
                Instrumentos.Add(new UsuarioInstrumento(instrumentoId));
            }
        }
    }
}
