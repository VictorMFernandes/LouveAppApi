using System.Threading.Tasks;
using LouveApp.Dominio.Comandos.UsuarioComandos.Entradas;
using LouveApp.Dominio.Gerenciadores;
using LouveApp.Dominio.Sistema.Exemplos;
using LouveApp.Dominio.Testes.Falsos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
                , new MinisterioRepositorioFalso(), new InstrumentoRepositorioFalso(), new EmailServicoFalso(), new PushNotificationServicoFalso());

            _registrarUsuarioComandoValido = ExemplosComando.RegistrarUsuario;
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
