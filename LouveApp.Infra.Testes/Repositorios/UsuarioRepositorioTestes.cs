using LouveApp.Compartilhado.Padroes;
using LouveApp.Dominio.Repositorios;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LouveApp.Infra.Testes.Repositorios
{
    [TestClass]
    public class UsuarioRepositorioTestes
    {
        private readonly IUsuarioRepositorio _usuarioRepo;
        public UsuarioRepositorioTestes(IUsuarioRepositorio usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
        }

        [TestMethod]
        public async void RetornaTrueQuandoEmailExiste()
        {
            var retorno = await _usuarioRepo.EmailExiste(PadroesString.UsuarioEmail);
            Assert.IsTrue(retorno);
        }
    }
}
