using System;

namespace LouveApp.Dominio.Entidades.Juncao
{
    public sealed class UsuarioMinisterio : EntidadeJuncaoComUsuario
    {
        #region Propriedades

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
