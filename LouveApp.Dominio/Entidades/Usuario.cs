using FluentValidator;
using LouveApp.Compartilhado.Entidades;
using LouveApp.Dominio.Entidades.Juncao;
using LouveApp.Dominio.Sistema.Padroes;
using LouveApp.Dominio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public ICollection<UsuarioEscala> Escalas { get; private set; }
        public ICollection<Dispositivo> Dispositivos { get; private set; }
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
            Escalas = new HashSet<UsuarioEscala>();
            Dispositivos = new HashSet<Dispositivo>();
        }

        protected override void Validar()
        {
            if (Nome != null)
                AddNotifications(Nome);
            else
                AddNotification(new Notification(nameof(Nome)
                    , PadroesMensagens.PropriedadeNaoPodeSerNula));

            AddNotifications(Email, Autenticacao);
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

            Validar();
        }

        public void AtualizarFoto(Foto foto)
        {
            Foto = foto;
            DtUltimaAtividade = DateTime.Now;
        }

        private void AtualizarInstrumentos(IEnumerable<string> instrumentosIds)
        {
            if (instrumentosIds == null) return;

            Instrumentos.Clear();

            foreach (var instrumentoId in instrumentosIds)
            {
                Instrumentos.Add(new UsuarioInstrumento(instrumentoId));
            }
        }

        public bool AdicionarDispositivo(Dispositivo dispositivo)
        {
            DtUltimaAtividade = DateTime.Now;

            if (Dispositivos.Any(d => d.Token == dispositivo.Token))
                return false;

            Dispositivos.Add(dispositivo);

            Validar();
            return true;
        }

        public bool RemoverDispositivo(string token)
        {
            DtUltimaAtividade = DateTime.Now;

            var dispositivo = Dispositivos.FirstOrDefault(d => d.Token == token);

            if (dispositivo == null)
                return false;

            Dispositivos.Remove(dispositivo);

            Validar();
            return true;
        }
    }
}
