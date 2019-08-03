using LouveApp.Compartilhado.Entidades;
using LouveApp.Dominio.Entidades.Juncao;
using System;
using System.Collections.Generic;

namespace LouveApp.Dominio.Entidades
{
    public sealed class Escala : Entidade
    {
        #region Propriedades

        public DateTime Data { get; private set; }
        public string MinisterioId { get; private set; }
        public Ministerio Ministerio { get; private set; }
        public ICollection<UsuarioEscala> Usuarios { get; private set; }

        #endregion

        #region Construtores

        private Escala() { }

        public Escala(DateTime data, IEnumerable<string> usuariosIds)
        {
            Data = data;

            AdicionarUsuarios(usuariosIds);

            Validar();
        }

        #endregion

        #region Métodos de Sobrescrita

        public override string ToString()
        {
            return Data.ToString();
        }

        protected override void InicializarColecoes()
        {
            Usuarios = new List<UsuarioEscala>();
        }

        protected override void Validar() { }

        #endregion

        private void AdicionarUsuarios(IEnumerable<string> usuariosIds)
        {
            Usuarios.Clear();

            foreach (var usuarioId in usuariosIds)
            {
                Usuarios.Add(new UsuarioEscala(usuarioId));
            }
        }
    }
}
