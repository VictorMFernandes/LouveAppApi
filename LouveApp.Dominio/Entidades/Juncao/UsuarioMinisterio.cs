using System;

namespace LouveApp.Dominio.Entidades.Juncao
{
    public sealed class UsuarioMinisterio : EntidadeJuncaoComUsuario
    {
        #region Propriedades

        public string MinisterioId { get; private set; }
        public Ministerio Ministerio { get; private set; }
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
