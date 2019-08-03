using LouveApp.Dominio.Entidades.Juncao;
using LouveApp.Dominio.ValueObjects;
using LouveApp.Infra.BancoDeDados.Contexto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace LouveApp.Dominio.Testes.Entidades
{
    [TestClass]
    public class UsuarioTestes
    {
        #region Atualizar

        [TestMethod]
        public void AtualizaNomeAoAtualizarUsuario()
        {
            var usuario = SemeadorBd.CriarUsuario();

            var nomeOriginal = usuario.Nome;
            usuario.Atualizar(new Nome("Novo Nome"), null);

            Assert.AreNotEqual(nomeOriginal, usuario.Nome);
        }

        [TestMethod]
        public void NaoAtualizaNomeQuandoNomeForNull()
        {
            var usuario = SemeadorBd.CriarUsuario();

            var nomeOriginal = usuario.Nome;
            usuario.Atualizar(null, null);

            Assert.AreEqual(nomeOriginal, usuario.Nome);
        }

        [TestMethod]
        public void InvalidaUsuarioQuandoNomeAtualizadoForInvalido()
        {
            var usuario = SemeadorBd.CriarUsuario();

            usuario.Atualizar(new Nome(string.Empty), null);

            Assert.IsTrue(usuario.Invalid);
        }


        [TestMethod]
        public void AtualizaInstrumentoAoAtualizarUsuario()
        {
            var usuario = SemeadorBd.CriarUsuario();

            var listaInstrumentosIds = new string[] { "idvalido", "idvalido1" };
            usuario.Atualizar(null, listaInstrumentosIds);

            Assert.AreEqual(listaInstrumentosIds.Length, usuario.Instrumentos.Count);
        }

        [TestMethod]
        public void NaoAtualizaInstrumentosQuandoInstrumentosForNull()
        {
            var usuario = SemeadorBd.CriarUsuario();
            usuario.Instrumentos.Add(new UsuarioInstrumento("idvalido"));

            usuario.Atualizar(null, null);

            Assert.AreEqual("idvalido", usuario.Instrumentos.First().InstrumentoId);
        }

        #endregion
    }
}
