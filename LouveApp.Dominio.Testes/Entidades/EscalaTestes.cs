using LouveApp.Dominio.Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LouveApp.Compartilhado.Extensoes;

namespace LouveApp.Dominio.Testes.Entidades
{
    [TestClass]
    public class EscalaTestes : IEntidadeTestes
    {
        #region Construtores

        public void InicializaColecoesAoConstruir()
        {
            var escala = new Escala(DateTime.Now, null, null);

            foreach (var prop in escala.PegarColecoes())
            {
                Assert.IsNotNull(prop.GetValue(escala));
            }
        }

        #endregion

        #region DefinirUsuarios

        [TestMethod]
        public void DefineUsuariosQuandoPassadoIdsValidos()
        {
            var usuariosIds = new[] { "idvalido1", "idvalido2", "idvalido3" };

            var escala = new Escala(DateTime.Now, usuariosIds, null);

            Assert.AreEqual(usuariosIds.Length, escala.Usuarios.Count);
        }

        [TestMethod]
        public void NaoAdicionaUsuariosDuplicados()
        {
            var usuariosIds = new[] { "idvalidoIgual", "idvalidoIgual", "idvalido3" };

            var escala = new Escala(DateTime.Now, usuariosIds, null);

            Assert.AreEqual(usuariosIds.Length - 1, escala.Usuarios.Count);
        }

        #endregion

        #region DefinirMusicas

        [TestMethod]
        public void DefineMusicasQuandoPassadoIdsValidos()
        {
            var musicasIds = new[] { "idvalido1", "idvalido2", "idvalido3" };

            var escala = new Escala(DateTime.Now, null, musicasIds);

            Assert.AreEqual(musicasIds.Length, escala.Musicas.Count);
        }

        [TestMethod]
        public void NaoAdicionaMusicasDuplicadas()
        {
            var musicasIds = new[] { "idvalidoIgual", "idvalidoIgual", "idvalido3" };

            var escala = new Escala(DateTime.Now, null, musicasIds);

            Assert.AreEqual(musicasIds.Length - 1, escala.Musicas.Count);
        }

        #endregion
    }
}
