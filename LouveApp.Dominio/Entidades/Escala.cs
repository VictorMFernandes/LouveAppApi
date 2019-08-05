using LouveApp.Compartilhado.Entidades;
using LouveApp.Dominio.Entidades.Juncao;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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

            DefinirUsuarios(usuariosIds);
            DefinirMusicas(musicasIds);

            Validar();
        }

        #endregion

        #region Métodos de Sobrescrita

        public override string ToString()
        {
            return Data.ToString(CultureInfo.InvariantCulture);
        }

        protected override void InicializarColecoes()
        {
            Usuarios = new List<UsuarioEscala>();
            Musicas = new List<EscalaMusica>();
        }

        protected override void Validar() { }

        #endregion

        private void DefinirUsuarios(IEnumerable<string> usuariosIds)
        {
            if (usuariosIds == null) return;

            Usuarios.Clear();

            foreach (var usuarioId in usuariosIds.Distinct())
            {
                Usuarios.Add(new UsuarioEscala(usuarioId));
            }
        }

        private void DefinirMusicas(IEnumerable<string> musicasIds)
        {
            if (musicasIds == null) return;

            Musicas.Clear();

            foreach (var musicaId in musicasIds.Distinct())
            {
                Musicas.Add(new EscalaMusica(musicaId));
            }
        }
    }
}
