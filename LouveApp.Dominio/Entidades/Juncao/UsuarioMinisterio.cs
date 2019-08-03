using LouveApp.Compartilhado.Entidades;
using System;

namespace LouveApp.Dominio.Entidades.Juncao
{
    public sealed class UsuarioMinisterio : EntidadeJuncao
    {
        #region Propriedades

        public string UsuarioId { get; private set; }
        private Usuario _usuario;
        public Usuario Usuario
        {
            get => _usuario;
            private set
            {
                if (value != null) UsuarioId = value.Id;
                _usuario = value;
            }
        }
        public string MinisterioId { get; private set; }
        private Ministerio _ministerio;
        public Ministerio Ministerio
        {
            get => _ministerio;
            private set
            {
                if (value != null) MinisterioId = value.Id;
                _ministerio = value;
            }
        }
        public bool Administrador { get; private set; }
        public DateTime DtIngresso { get; private set; }

        #endregion

        #region Construtores

        private UsuarioMinisterio() { }

        public UsuarioMinisterio(Usuario usuario, bool administrador)
        {
            Usuario = usuario;
            Administrador = administrador;
            DtIngresso = DateTime.Now;
        }

        #endregion
    }
}
