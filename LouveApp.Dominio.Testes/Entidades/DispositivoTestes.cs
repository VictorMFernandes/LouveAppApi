using LouveApp.Compartilhado.Extensoes;
using LouveApp.Dominio.Entidades;
using LouveApp.Dominio.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LouveApp.Dominio.Testes.Entidades
{
    [TestClass]
    public class DispositivoTestes : IEntidadeTestes
    {
        #region Construtores

        public void InicializaColecoesAoConstruir()
        {
            var escala = new Dispositivo("Token Válido", new Nome("Nome Válido"));

            foreach (var prop in escala.PegarColecoes())
            {
                Assert.IsNotNull(prop.GetValue(escala));
            }
        }

        #endregion
    }
}
