using LouveApp.Dominio.Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LouveApp.Compartilhado.Extensoes;
using LouveApp.Dominio.ValueObjects;
using LouveApp.Infra.BancoDeDados.Contexto;

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
            var musica = new Musica(nomeInvalido, null
                , null, string.Empty, null, string.Empty);

            Assert.IsTrue(musica.Invalid);
        }

        [TestMethod]
        public void InvalidaMusicaQuandoReferenciaInvalida()
        {
            var nome = new Nome("Nome Válido");
            var referenciaInvalida = new Link(string.Empty);
            var musica = new Musica(nome, referenciaInvalida, null, string.Empty
            , null, string.Empty);

            Assert.IsTrue(musica.Invalid);
        }

        [TestMethod]
        public void ConstroiMusicaQuandoReferenciaNula()
        {
            var nome = new Nome("Nome Válido");
            var musica = new Musica(nome, null, null, string.Empty
            , null, string.Empty);

            Assert.IsTrue(musica.Valid);
        }

        [TestMethod]
        public void InvalidaMusicaQuandoNomeNull()
        {
            var musica = new Musica(null, null, null, string.Empty
                , null, string.Empty);

            Assert.IsTrue(musica.Invalid);
        }

        #endregion
    }
}
