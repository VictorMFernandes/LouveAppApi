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
        public ICollection<EscalaMusica> Musicas { get; private set; }

        #endregion

        #region Construtores

        private Escala() { }

        public Escala(DateTime data, IEnumerable<string> usuariosIds, IEnumerable<string> musicasIds)
        {
            Data = data;

            AdicionarUsuarios(usuariosIds);
            AdicionarMusicas(musicasIds);

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
            Musicas = new List<EscalaMusica>();
        }

        protected override void Validar() { }

        #endregion

        private void AdicionarUsuarios(IEnumerable<string> usuariosIds)
        {
            if (usuariosIds == null) return;

            Usuarios.Clear();

            foreach (var usuarioId in usuariosIds)
            {
                Usuarios.Add(new UsuarioEscala(usuarioId));
            }
        }

        private void AdicionarMusicas(IEnumerable<string> musicasIds)
        {
            if (musicasIds == null) return;

            Musicas.Clear();

            foreach (var musicaId in musicasIds)
            {
                Musicas.Add(new EscalaMusica(musicaId));
            }
        }
    }
}
