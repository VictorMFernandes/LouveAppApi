using LouveApp.Compartilhado.Padroes;
using LouveApp.Dominio.Entidades;
using LouveApp.Dominio.ValueObjects;
using LouveApp.Infra.BancoDeDados.Contexto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace LouveApp.Dominio.Testes.Entidades
{
    [TestClass]
    public class MinisterioTestes
    {
        private readonly Usuario _usuarioNovo;
        private readonly Usuario _usuarioNovoInvalido;

        public MinisterioTestes()
        {
            var nome = new Nome("Novo Nome");
            var email = new Email("novo@email.com");
            var autenticacao = new Autenticacao("novo@email.com", PadroesString.SenhaValida, PadroesString.SenhaValida);

            _usuarioNovo = new Usuario(nome, email, autenticacao);

            var nomeInvalido = new Nome(string.Empty);
            _usuarioNovoInvalido = new Usuario(nomeInvalido, email, autenticacao);
        }

        #region AdicionarUsuario

        [TestMethod]
        public void MinisterioValidoAoAdicionarUsuarioNovo()
        {
            var ministerio = SemeadorBd.CriarMinisterio1();
            var qtdOriginal = ministerio.Usuarios.Count;

            ministerio.AdicionarUsuario(_usuarioNovo);

            Assert.IsTrue(ministerio.Valid);
            Assert.AreEqual(++qtdOriginal, ministerio.Usuarios.Count);
        }

        [TestMethod]
        public void UsuarioNaoAdministradorQuandoAdicionadoComoUsuario()
        {
            var ministerio = SemeadorBd.CriarMinisterio1();

            ministerio.AdicionarUsuario(_usuarioNovo);

            Assert.IsFalse(ministerio.Usuarios.First(um => um.UsuarioId == _usuarioNovo.Id).Administrador);
        }

        [TestMethod]
        public void MinisterioInvalidoAoAdicionarUsuarioNovoInvalido()
        {
            var ministerio = SemeadorBd.CriarMinisterio1();

            ministerio.AdicionarUsuario(_usuarioNovoInvalido);

            Assert.IsTrue(ministerio.Invalid);
        }

        [TestMethod]
        public void InvalidarMinisterioQuandoMesmoUsuarioAdicionadoMaisDeUmaVez()
        {
            var ministerio = SemeadorBd.CriarMinisterio1();

            ministerio.AdicionarUsuario(SemeadorBd.CriarUsuario1());

            Assert.IsTrue(ministerio.Invalid);
        }

        #endregion

        #region AdicionarAdministrador

        [TestMethod]
        public void MinisterioValidoAoAdicionarAdministradorNovo()
        {
            var ministerio = SemeadorBd.CriarMinisterio1();
            var qtdOriginal = ministerio.Usuarios.Count;

            ministerio.AdicionarAdministrador(_usuarioNovo);

            Assert.IsTrue(ministerio.Valid);
            Assert.AreEqual(++qtdOriginal, ministerio.Usuarios.Count);
        }

        [TestMethod]
        public void UsuarioAdministradorQuandoAdicionadoComoAdministrador()
        {
            var ministerio = SemeadorBd.CriarMinisterio1();

            ministerio.AdicionarAdministrador(_usuarioNovo);

            Assert.IsTrue(ministerio.Usuarios.First(um => um.UsuarioId == _usuarioNovo.Id).Administrador);
        }

        [TestMethod]
        public void MinisterioInvalidoAoAdicionarAdministradorNovoInvalido()
        {
            var ministerio = SemeadorBd.CriarMinisterio1();

            ministerio.AdicionarAdministrador(_usuarioNovoInvalido);

            Assert.IsTrue(ministerio.Invalid);
        }

        [TestMethod]
        public void InvalidarMinisterioQuandoMesmoAdministradorAdicionadoMaisDeUmaVez()
        {
            var ministerio = SemeadorBd.CriarMinisterio1();

            ministerio.AdicionarAdministrador(SemeadorBd.CriarUsuario1());

            Assert.IsTrue(ministerio.Invalid);
        }

        #endregion

        #region Administrador

        [TestMethod]
        public void RetornaTrueQuandoUsuarioAdministrador()
        {
            var ministerio = SemeadorBd.CriarMinisterio1();

            Assert.IsTrue(ministerio.Administrador(PadroesString.UsuarioId1));
        }

        [TestMethod]
        public void RetornaFalseQuandoUsuarioNaoForAdministrador()
        {
            var ministerio = SemeadorBd.CriarMinisterio1();

            ministerio.AdicionarUsuario(_usuarioNovo);

            Assert.IsFalse(ministerio.Administrador(_usuarioNovo.Id));
        }

        [TestMethod]
        public void RetornaFalseQuandoIdInvalidoAdministrador()
        {
            var ministerio = SemeadorBd.CriarMinisterio1();

            Assert.IsFalse(ministerio.Administrador(Guid.NewGuid().ToString("N")));
        }

        #endregion

        #region AtivarLinkConvite

        [TestMethod]
        public void GeraLinkConviteAtivoQuandoUsuarioAutorizado()
        {
            var ministerio = SemeadorBd.CriarMinisterio1();

            var autorizado = ministerio.AtivarLinkConvite(PadroesString.UsuarioId1);

            Assert.IsTrue(autorizado);
            Assert.IsTrue(ministerio.LinkConviteAtivado);
            Assert.IsFalse(string.IsNullOrWhiteSpace(ministerio.LinkConvite));
        }

        [TestMethod]
        public void NaoGeraLinkConviteAtivoQuandoUsuarioNaoAutorizado()
        {
            var ministerio = SemeadorBd.CriarMinisterio1();

            ministerio.AdicionarUsuario(_usuarioNovo);

            var autorizado = ministerio.AtivarLinkConvite(_usuarioNovo.Id);

            Assert.IsFalse(autorizado);
            Assert.IsFalse(ministerio.LinkConviteAtivado);
            Assert.IsTrue(string.IsNullOrWhiteSpace(ministerio.LinkConvite));
        }

        [TestMethod]
        public void NaoGeraLinkConviteAtivoQuandoIdDesconhecido()
        {
            var ministerio = SemeadorBd.CriarMinisterio1();

            var autorizado = ministerio.AtivarLinkConvite(Guid.NewGuid().ToString("N"));

            Assert.IsFalse(autorizado);
            Assert.IsFalse(ministerio.LinkConviteAtivado);
            Assert.IsTrue(string.IsNullOrWhiteSpace(ministerio.LinkConvite));
        }

        #endregion

        #region DesativarLinkConvite

        [TestMethod]
        public void DesativaLinkConviteQuandoUsuarioAutorizado()
        {
            var ministerio = SemeadorBd.CriarMinisterio1();
            ministerio.AtivarLinkConvite(PadroesString.UsuarioId1);

            var autorizado = ministerio.DesativarLinkConvite(PadroesString.UsuarioId1);

            Assert.IsTrue(autorizado);
            Assert.IsFalse(ministerio.LinkConviteAtivado);
            Assert.IsTrue(string.IsNullOrWhiteSpace(ministerio.LinkConvite));
        }

        [TestMethod]
        public void DesativaLinkConviteQuandoUsuarioAutorizadoMesmoComLinkInexistente()
        {
            var ministerio = SemeadorBd.CriarMinisterio1();

            var autorizado = ministerio.DesativarLinkConvite(PadroesString.UsuarioId1);

            Assert.IsTrue(autorizado);
            Assert.IsFalse(ministerio.LinkConviteAtivado);
            Assert.IsTrue(string.IsNullOrWhiteSpace(ministerio.LinkConvite));
        }

        [TestMethod]
        public void NaoDesativaLinkConviteQuandoUsuarioNaoAutorizado()
        {
            var ministerio = SemeadorBd.CriarMinisterio1();
            ministerio.AtivarLinkConvite(PadroesString.UsuarioId1);

            ministerio.AdicionarUsuario(_usuarioNovo);

            var autorizado = ministerio.DesativarLinkConvite(_usuarioNovo.Id);

            Assert.IsFalse(autorizado);
            Assert.IsTrue(ministerio.LinkConviteAtivado);
            Assert.IsFalse(string.IsNullOrWhiteSpace(ministerio.LinkConvite));
        }

        [TestMethod]
        public void NaoDesativaLinkConviteQuandoIdDesconhecido()
        {
            var ministerio = SemeadorBd.CriarMinisterio1();
            ministerio.AtivarLinkConvite(PadroesString.UsuarioId1);

            var autorizado = ministerio.AtivarLinkConvite(Guid.NewGuid().ToString("N"));

            Assert.IsFalse(autorizado);
            Assert.IsTrue(ministerio.LinkConviteAtivado);
            Assert.IsFalse(string.IsNullOrWhiteSpace(ministerio.LinkConvite));
        }

        #endregion
    }
}
