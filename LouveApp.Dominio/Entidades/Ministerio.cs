using LouveApp.Compartilhado.Entidades;
using LouveApp.Dominio.Entidades.Juncao;
using LouveApp.Dominio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidator;
using LouveApp.Compartilhado.Padroes;

namespace LouveApp.Dominio.Entidades
{
    public sealed class Ministerio : Entidade
    {
        #region Propriedades

        public Nome Nome { get; private set; }
        public Foto Foto { get; private set; }
        public string LinkConvite { get; private set; }
        public bool LinkConviteAtivado { get; private set; }
        public ICollection<UsuarioMinisterio> Usuarios { get; private set; }
        public ICollection<Escala> Escalas { get; private set; }
        public DateTime DtCriacao { get; }

        #endregion

        #region Construtores

        private Ministerio()
        {
        }

        public Ministerio(Nome nome, Usuario administrador)
        {
            Nome = nome;
            Foto = new Foto(string.Empty, string.Empty);
            DtCriacao = DateTime.Now;

            AdicionarAdministrador(administrador);

            Validar();
        }

        public Ministerio(string id, Nome nome, Usuario administrador)
            : this(nome, administrador)
        {
            Id = id;
        }

        #endregion

        #region Métodos de Sobrescrita

        protected override void InicializarColecoes()
        {
            Usuarios = new List<UsuarioMinisterio>();
            Escalas = new List<Escala>();
        }

        protected override void Validar()
        {
            if (Usuarios.GroupBy(um => um.Usuario.Id).Any(g => g.Count() > 1))
            {
                AddNotification(new Notification(nameof(Usuarios), PadroesMensagens.UsuariosDuplicadosMinisterio));
            }

            if (!Usuarios.Any(um => um.Administrador))
            {
                AddNotification(new Notification(nameof(Usuarios), PadroesMensagens.MinisterioDeveTerAdministrador));
            }

            AddNotifications(Nome);
        }

        public override string ToString()
        {
            return Nome.ToString();
        }

        #endregion

        public void AtualizarFoto(Foto foto)
        {
            Foto = foto;
        }

        public void AdicionarUsuario(Usuario usuario, bool administrador = false)
        {
            AddNotifications(usuario);

            Usuarios.Add(new UsuarioMinisterio(usuario, administrador));

            Validar();
        }

        public void AdicionarAdministrador(Usuario usuario)
        {
            AdicionarUsuario(usuario, true);
        }

        public bool Administrador(string usuarioId)
        {
            var usuarioMin = Usuarios.FirstOrDefault(um => um.Usuario.Id == usuarioId);

            return (usuarioMin?.Administrador == true);
        }

        /// <summary>
        /// Ativa e cria o link convite parar entrar no ministério.
        /// </summary>
        /// <param name="usuarioId">Id do usuário com permissão para ativar o link.</param>
        /// <returns>True se a ativação foi bem sucedida.</returns>
        public bool AtivarLinkConvite(string usuarioId)
        {
            if (!Administrador(usuarioId))
                return false;

            LinkConvite = Guid.NewGuid().ToString("N");
            LinkConviteAtivado = true;

            return LinkConviteAtivado;
        }

        /// <summary>
        /// Desativa qualquer link convite do ministério.
        /// </summary>
        /// <remarks>Apaga qualquer link anteriormente gerado.</remarks>
        /// <param name="usuarioId">Id do usuário com permissão para desativar o link.</param>
        /// <returns>True se a desativação foi bem sucedida.</returns>
        public bool DesativarLinkConvite(string usuarioId)
        {
            if (!Administrador(usuarioId))
                return false;

            LinkConvite = string.Empty;
            LinkConviteAtivado = false;

            return !LinkConviteAtivado;
        }

        public bool AdicionarEscala(string usuarioId, Escala escala)
        {
            if (!Administrador(usuarioId)) return false;

            AddNotifications(escala);

            Escalas.Add(escala);

            return true;
        }

        public bool RemoverEscala(string usuarioId, string escalaId)
        {
            if (!Administrador(usuarioId)) return false;

            var escala = Escalas.FirstOrDefault(e => e.Id == escalaId);
            if (escala == null) return false;

            Escalas.Remove(escala);
            return true;
        }
    }
}
