using LouveApp.Dominio.Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using LouveApp.Compartilhado.Extensoes;
using LouveApp.Dominio.Comandos.UsuarioComandos.SubEntidade;

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
            var usuarioInstrumentos = new List<UsuarioInstrumentos>
            {
                new UsuarioInstrumentos("idvalido1", new List<string>
                {
                    "InstrumentoIdValido1", "InstrumentoIdValido2"
                }),
                new UsuarioInstrumentos("idvalido2", new List<string>
                {
                    "InstrumentoIdValido3", "InstrumentoIdValido2"
                })
            };

            var escala = new Escala(DateTime.Now, usuarioInstrumentos, null);

            Assert.AreEqual(usuarioInstrumentos.Count, escala.Usuarios.Count);
        }

        [TestMethod]
        public void NaoAdicionaUsuariosDuplicados()
        {
            var usuarioInstrumentos = new List<UsuarioInstrumentos>
            {
                new UsuarioInstrumentos("idIgual", new List<string>
                {
                    "InstrumentoIdValido1", "InstrumentoIdValido2"
                }),
                new UsuarioInstrumentos("idIgual", new List<string>
                {
                    "InstrumentoIdValido3", "InstrumentoIdValido4"
                }),
                new UsuarioInstrumentos("idValido3", new List<string>
                {
                    "InstrumentoIdValido5", "InstrumentoIdValido1"
                })
            };

            var escala = new Escala(DateTime.Now, usuarioInstrumentos, null);

            Assert.AreEqual(usuarioInstrumentos.Count - 1, escala.Usuarios.Count);
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
