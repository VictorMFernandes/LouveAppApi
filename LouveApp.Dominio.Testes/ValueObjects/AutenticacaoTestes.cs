using LouveApp.Compartilhado.Padroes;
using LouveApp.Dominio.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LouveApp.Dominio.Testes.ValueObjects
{
    [TestClass]
    public class AutenticacaoTestes
    {
        [TestMethod]
        public void InvalidarQuandoSenhasDiferentes()
        {
            var autenticacao = new Autenticacao("login", PadroesString.SenhaValida, "senhaDiferente");

            Assert.IsTrue(autenticacao.Invalid);
            Assert.AreNotEqual(0, autenticacao.Notifications.Count);
        }

        [TestMethod]
        public void ValidarQuandoSenhasIguais()
        {
            var autenticacao = new Autenticacao("meuLogin", PadroesString.SenhaValida, PadroesString.SenhaValida);

            Assert.IsTrue(autenticacao.Valid);
            Assert.AreEqual(0, autenticacao.Notifications.Count);
        }
    }
}
