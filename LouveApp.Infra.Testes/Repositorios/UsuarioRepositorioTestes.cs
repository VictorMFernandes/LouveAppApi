using LouveApp.Compartilhado.Padroes;
using LouveApp.Infra.BancoDeDados.Contexto;
using LouveApp.Infra.BancoDeDados.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LouveApp.Infra.Testes.Repositorios
{
    [TestClass]
    public class UsuarioRepositorioTestes
    {
        [TestMethod]
        public async void RetornaTrueQuandoEmailExiste()
        {
            var options = new DbContextOptionsBuilder<BancoContexto>()
                .UseInMemoryDatabase("LouveApp")
                .Options;

            using (var context = new BancoContexto(options))
            {
                context.Usuarios.Add(SemeadorBd.CriarUsuario());
                context.SaveChanges();
            }

            using (var context = new BancoContexto(options))
            {
                var repositorio = new UsuarioRepositorio(context);

                var retorno = await repositorio.EmailExiste(PadroesString.UsuarioEmail);
                Assert.IsTrue(retorno);
            }
        }
    }
}
