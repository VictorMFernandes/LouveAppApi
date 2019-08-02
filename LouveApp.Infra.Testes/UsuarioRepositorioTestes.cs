using System.Threading.Tasks;
using LouveApp.Compartilhado.Padroes;
using LouveApp.Dominio.Sistema;
using LouveApp.Infra.BancoDeDados.Contexto;
using LouveApp.Infra.BancoDeDados.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LouveApp.Infra.Testes
{
    [TestClass]
    public class UsuarioRepositorioTestes
    {
        [TestMethod]
        public async Task RetornaTrueQuandoEmailExiste()
        {
            const string connString = "Teste.db";

            var options = new DbContextOptionsBuilder<BancoContexto>()
                .UseSqlite(connString)
                .Options;

            Configuracoes.ConnString = connString;

            using (var context = new BancoContexto(options))
            {
                //context.Database.EnsureCreated();

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
