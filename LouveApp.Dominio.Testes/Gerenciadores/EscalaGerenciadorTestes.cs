using LouveApp.Dominio.Comandos.EscalaComandos.Entradas;
using LouveApp.Dominio.Gerenciadores;
using LouveApp.Dominio.Testes.Falsos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LouveApp.Dominio.Testes.Gerenciadores
{
    [TestClass]
    public class EscalaGerenciadorTestes
    {
        private readonly EscalaGerenciador _gerenciador;

        private readonly RegistrarEscalaComando _registrarEscalaComandoValido;

        public EscalaGerenciadorTestes()
        {
            _gerenciador = new EscalaGerenciador(new MinisterioRepositorioFalso()
                , new DispositivoRepositorioFalso(), new PushNotificationServicoFalso());

            _registrarEscalaComandoValido = new RegistrarEscalaComando(DateTime.Now, null, null);
        }

        [TestMethod]
        public void ValidarComandosAoExecutalos()
        {
            _ = _gerenciador.Executar(_registrarEscalaComandoValido);

            Assert.IsTrue(_registrarEscalaComandoValido.FoiValidado);
        }
    }
}
