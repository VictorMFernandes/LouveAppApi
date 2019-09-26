using LouveApp.Dominio.Comandos.UsuarioComandos.Entradas;
using LouveApp.Dominio.Gerenciadores;
using LouveApp.Dominio.Testes.Falsos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace LouveApp.Dominio.Testes.Gerenciadores
{
    [TestClass]
    public class UsuarioGerenciadorTestes : IGerenciadorTestes
    {
        private readonly UsuarioGerenciador _gerenciador;

        private readonly RegistrarUsuarioComando _registrarUsuarioComandoValido;
        private readonly RegistrarUsuarioComando _registrarUsuarioComandoInvalido;

        public UsuarioGerenciadorTestes()
        {
            _gerenciador = new UsuarioGerenciador(new UsuarioRepositorioFalso()
                , new MinisterioRepositorioFalso(), new DispositivoRepositorioFalso(), new InstrumentoRepositorioFalso(), new EmailServicoFalso(), new PushNotificationServicoFalso());

            _registrarUsuarioComandoValido = new RegistrarUsuarioComando("Primeiro Sobrenome", "email@email.com", "senha", "senha");
            _registrarUsuarioComandoInvalido = new RegistrarUsuarioComando(
                "Primeiro Sobrenome", "email", "senha", "senha");
        }

        [TestMethod]
        public void ValidarComandosAoExecutalos()
        {
            _ = _gerenciador.Executar(_registrarUsuarioComandoValido);

            Assert.IsTrue(_registrarUsuarioComandoValido.FoiValidado);
        }

        [TestMethod]
        public async Task RetornarNullQuandoComandoInvalido()
        {
            var resultado = await _gerenciador.Executar(_registrarUsuarioComandoInvalido);

            Assert.IsNull(resultado);
        }
    }
}
