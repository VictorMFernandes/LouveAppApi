using LouveApp.Compartilhado.Extensoes;
using LouveApp.Dal.Contexto;
using LouveApp.Dominio.Entidades;
using LouveApp.Dominio.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LouveApp.Dominio.Testes.Entidades
{
    [TestClass]
    public class MusicaTestes : IEntidadeTestes
    {
        #region Construtores

        public void InicializaColecoesAoConstruir()
        {
            var musica = SemeadorBd.CriarMusica1();

            foreach (var prop in musica.PegarColecoes())
            {
                Assert.IsNotNull(prop.GetValue(musica));
            }
        }

        [TestMethod]
        public void InvalidaMusicaQuandoNomeInvalido()
        {
            var nomeInvalido = new Nome(string.Empty);
            var musica = new Musica(nomeInvalido, null, null, null
                , null, string.Empty, null, string.Empty);

            Assert.IsTrue(musica.Invalid);
        }

        [TestMethod]
        public void InvalidaMusicaQuandoLetraInvalida()
        {
            var nome = new Nome("Nome Válido");
            var letraInvalida = new Link(string.Empty);
            var musica = new Musica(nome, letraInvalida, null, null, null, string.Empty
            , null, string.Empty);

            Assert.IsTrue(musica.Invalid);
        }

        [TestMethod]
        public void InvalidaMusicaQuandoCifraInvalida()
        {
            var nome = new Nome("Nome Válido");
            var cifraInvalida = new Link(string.Empty);
            var musica = new Musica(nome, null, cifraInvalida, null, null, string.Empty
                , null, string.Empty);

            Assert.IsTrue(musica.Invalid);
        }

        [TestMethod]
        public void InvalidaMusicaQuandoVideoInvalido()
        {
            var nome = new Nome("Nome Válido");
            var videoInvalida = new Link(string.Empty);
            var musica = new Musica(nome, null, null, videoInvalida, null, string.Empty
                , null, string.Empty);

            Assert.IsTrue(musica.Invalid);
        }

        [TestMethod]
        public void ConstroiMusicaQuandoReferenciasNulas()
        {
            var nome = new Nome("Nome Válido");
            var musica = new Musica(nome, null, null, null, null, string.Empty
            , null, string.Empty);

            Assert.IsTrue(musica.Valid);
        }

        [TestMethod]
        public void InvalidaMusicaQuandoNomeNull()
        {
            var musica = new Musica(null, null, null, null, null, string.Empty
                , null, string.Empty);

            Assert.IsTrue(musica.Invalid);
        }

        #endregion
    }
}
