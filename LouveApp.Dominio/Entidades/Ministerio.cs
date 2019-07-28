using LouveApp.Compartilhado.Entidades;
using LouveApp.Dominio.Entidades.Juncao;
using LouveApp.Dominio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using LouveApp.Compartilhado.Extensoes;

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

        public void AtualizarFoto(Foto foto)
        {
            Foto = foto;
        }

        public void AdicionarUsuario(Usuario usuario)
        {
            Usuarios.Add(new UsuarioMinisterio(usuario, false));
        }

        public void AdicionarAdministrador(Usuario usuario)
        {
            Usuarios.Add(new UsuarioMinisterio(usuario, true));
        }

        public bool AutorizadoTrocarFoto(string usuarioId)
        {
            var usuarioMin = Usuarios.FirstOrDefault(um => um.Usuario.Id == usuarioId);
            return (usuarioMin?.Administrador == true);
        }

        public bool AtivarLinkConvite(string usuarioId)
        {
            var usuarioMin = Usuarios.FirstOrDefault(um => um.Usuario.Id == usuarioId);

            if (usuarioMin?.Administrador != true)
                return false;

            LinkConvite = Guid.NewGuid().EmBase64();
            LinkConviteAtivado = true;

            return LinkConviteAtivado;
        }

        public bool DesativarLinkConvite(string usuarioId)
        {
            var usuarioMin = Usuarios.FirstOrDefault(um => um.Usuario.Id == usuarioId);

            if (usuarioMin?.Administrador != true)
                return false;

            LinkConvite = string.Empty;
            LinkConviteAtivado = false;

            return !LinkConviteAtivado;
        }
    }
}
