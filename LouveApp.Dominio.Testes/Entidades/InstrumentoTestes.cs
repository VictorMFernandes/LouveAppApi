using LouveApp.Dominio.Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LouveApp.Compartilhado.Extensoes;
using LouveApp.Dominio.ValueObjects;

namespace LouveApp.Dominio.Testes.Entidades
{
    [TestClass]
    public class InstrumentoTestes : IEntidadeTestes
    {
        #region Construtores

        [TestMethod]
        public void InvalidaInstrumentoQuandoNomeInvalido()
        {
            var nomeInvalido = new Nome(string.Empty);
            var instrumento = new Instrumento(nomeInvalido);

            Assert.IsTrue(instrumento.Invalid);
        }

        [TestMethod]
        public void InicializaColecoesAoConstruir()
        {
            var nome = new Nome("Nome Válido");
            var instrumento = new Instrumento(nome);

            foreach (var prop in instrumento.PegarColecoes())
            {
                Assert.IsNotNull(prop.GetValue(instrumento));
            }
        }

        #endregion
    }
}
