using LouveApp.Dominio.Comandos.MinisterioComandos.Entradas;
using LouveApp.Dominio.Gerenciadores;
using LouveApp.Dominio.Sistema.Exemplos;
using LouveApp.Dominio.Testes.Falsos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LouveApp.Dominio.Testes.Gerenciadores
{
    [TestClass]
    public class MinisterioGerenciadorTestes
    {
        private readonly MinisterioGerenciador _gerenciador;

        private readonly RegistrarMinisterioComando _registrarMinisterioComandoValido;
        private readonly RegistrarMinisterioComando _registrarMinisterioComandoInvalido;

        public MinisterioGerenciadorTestes()
        {
            _gerenciador = new MinisterioGerenciador(new MinisterioRepositorioFalso()
                , new UsuarioRepositorioFalso(), new PushNotificationServicoFalso());

            _registrarMinisterioComandoValido = ExemplosComando.RegistrarMinisterio;
            _registrarMinisterioComandoInvalido = new RegistrarMinisterioComando(string.Empty);
        }

        [TestMethod]
        public void ValidarComandosAoExecutalos()
        {
            _ = _gerenciador.Executar(_registrarMinisterioComandoValido);

            Assert.IsTrue(_registrarMinisterioComandoValido.FoiValidado);
        }

        [TestMethod]
        public async void RetornarNullQuandoComandoInvalido()
        {
            var resultado = await _gerenciador.Executar(_registrarMinisterioComandoInvalido);

            Assert.IsNull(resultado);
        }
    }
}
