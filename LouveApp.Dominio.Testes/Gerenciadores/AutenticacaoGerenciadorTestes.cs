using LouveApp.Compartilhado.Padroes;
using LouveApp.Dominio.Comandos.AutenticacaoComandos.Entradas;
using LouveApp.Dominio.Gerenciadores;
using LouveApp.Dominio.Testes.Falsos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace LouveApp.Dominio.Testes.Gerenciadores
{
    [TestClass]
    public class AutenticacaoGerenciadorTestes : IGerenciadorTestes
    {
        private readonly AutenticacaoGerenciador _gerenciador;

        private readonly AutenticarUsuarioComando _autenticarUsuarioComandoValido;
        private readonly AutenticarUsuarioComando _autenticarUsuarioComandoInvalido;

        public AutenticacaoGerenciadorTestes()
        {
            _gerenciador = new AutenticacaoGerenciador(new UsuarioRepositorioFalso(), new ConfigurationFalso());

            _autenticarUsuarioComandoValido = new AutenticarUsuarioComando(PadroesString.UsuarioEmail1, PadroesString.SenhaValida);
            _autenticarUsuarioComandoInvalido = new AutenticarUsuarioComando(string.Empty, "senha");
        }

        [TestMethod]
        public void ValidarComandosAoExecutalos()
        {
            _ = _gerenciador.Executar(_autenticarUsuarioComandoValido);

            Assert.IsTrue(_autenticarUsuarioComandoValido.FoiValidado);
        }

        [TestMethod]
        public async Task RetornarNullQuandoComandoInvalido()
        {
            var resultado = await _gerenciador.Executar(_autenticarUsuarioComandoInvalido);

            Assert.IsNull(resultado);
        }
    }
}
