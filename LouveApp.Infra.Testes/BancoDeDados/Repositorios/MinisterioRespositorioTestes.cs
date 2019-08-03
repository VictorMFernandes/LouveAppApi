using LouveApp.Compartilhado.Padroes;
using LouveApp.Dominio.Entidades;
using LouveApp.Dominio.ValueObjects;
using LouveApp.Infra.BancoDeDados.Repositorios;
using LouveApp.Infra.Testes.BancoDeDados.Falsos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LouveApp.Infra.Testes.BancoDeDados.Repositorios
{
    [TestClass]
    public class MinisterioRespositorioTestes
    {
        private readonly Usuario _usuarioNovo;

        public MinisterioRespositorioTestes()
        {
            var nome = new Nome("Novo Nome");
            var email = new Email("novo@email.com");
            var autenticacao = new Autenticacao("novo@email.com", "senha", "senha");

            _usuarioNovo = new Usuario(nome, email, autenticacao);
        }

        #region PegarPorId

        [TestMethod]
        public async Task RetornaMinisterioQuandoIdExiste()
        {
            var repositorio = new MinisterioRepositorio(BancoContextoFalso.St());

            var retorno = await repositorio.PegarPorId(PadroesString.MinisterioId);
            Assert.IsNotNull(retorno);
        }

        [TestMethod]
        public async Task RetornaNullQuandoIdNaoExiste()
        {
            var repositorio = new MinisterioRepositorio(BancoContextoFalso.St());

            var retorno = await repositorio.PegarPorId(Guid.NewGuid().ToString());
            Assert.IsNull(retorno);
        }

        #endregion

        #region PegarPorLinkConvite

        [TestMethod]
        public async Task RetornaMinisterioQuandoLinkValido()
        {
            var repositorio = new MinisterioRepositorio(BancoContextoFalso.St());

            // Ativa o link para testar
            var ministerio = await repositorio.PegarPorId(PadroesString.MinisterioId);
            ministerio.AtivarLinkConvite(PadroesString.UsuarioId);
            repositorio.Atualizar(ministerio);
            await BancoContextoFalso.St().SaveChangesAsync();

            var link = ministerio.LinkConvite;
            var retorno = await repositorio.PegarPorLinkConvite(link);
            Assert.IsNotNull(retorno);

            // Desativa o link que foi gerado
            ministerio.DesativarLinkConvite(PadroesString.UsuarioId);
            repositorio.Atualizar(ministerio);
            await BancoContextoFalso.St().SaveChangesAsync();
        }

        [TestMethod]
        public async Task RetornaMinisterioComUsuariosQuandoLinkValido()
        {
            var repositorio = new MinisterioRepositorio(BancoContextoFalso.St());

            // Ativa o link para testar
            var ministerio = await repositorio.PegarPorId(PadroesString.MinisterioId);
            ministerio.AtivarLinkConvite(PadroesString.UsuarioId);
            repositorio.Atualizar(ministerio);
            await BancoContextoFalso.St().SaveChangesAsync();

            var link = ministerio.LinkConvite;
            var retorno = await repositorio.PegarPorLinkConvite(link);
            Assert.IsNotNull(retorno.Usuarios);

            // Desativa o link que foi gerado
            ministerio.DesativarLinkConvite(PadroesString.UsuarioId);
            repositorio.Atualizar(ministerio);
            await BancoContextoFalso.St().SaveChangesAsync();
        }


        [TestMethod]
        public async Task RetornaNullQuandoLinkInvalido()
        {
            var repositorio = new MinisterioRepositorio(BancoContextoFalso.St());

            var retorno = await repositorio.PegarPorLinkConvite(Guid.NewGuid().ToString("N"));
            Assert.IsNull(retorno);
        }

        #endregion

        #region PegarPorUsuario

        [TestMethod]
        public async Task RetornaMinisterioQuandoIdUsuarioValido()
        {
            var repositorio = new MinisterioRepositorio(BancoContextoFalso.St());

            var retorno = await repositorio.PegarPorUsuario(PadroesString.UsuarioId);
            Assert.AreNotEqual(0, retorno.Count());
        }

        [TestMethod]
        public async Task RetornaNullQuandoIdUsuarioInvalido()
        {
            var repositorio = new MinisterioRepositorio(BancoContextoFalso.St());

            var retorno = await repositorio.PegarPorUsuario(Guid.NewGuid().ToString("N"));
            Assert.AreEqual(0, retorno.Count());
        }

        #endregion

        #region Remover

        [TestMethod]
        public async Task RemoveMinisterioQuandoIdValido()
        {
            var repositorio = new MinisterioRepositorio(BancoContextoFalso.St());

            await repositorio.Remover(PadroesString.MinisterioId);

            var ministerioDeletado = await repositorio.PegarPorId(PadroesString.MinisterioId);

            Assert.IsNull(ministerioDeletado);

            BancoContextoFalso.St().RestaurarBanco();
        }

        [TestMethod]
        public async Task NaoRemoveMinisterioQuandoIdInvalido()
        {
            var repositorio = new MinisterioRepositorio(BancoContextoFalso.St());

            var qtdOriginal = await repositorio.Contar();

            await repositorio.Remover(Guid.NewGuid().ToString("N"));

            var qtdAposRemover = await repositorio.Contar();

            Assert.AreEqual(qtdOriginal, qtdAposRemover);
        }

        #endregion

        #region EAdministrador

        [TestMethod]
        public async Task RetornaTrueQuandoUsuarioAdministrador()
        {
            var repositorio = new MinisterioRepositorio(BancoContextoFalso.St());

            var eAdministrador = await repositorio.EAdministrador(PadroesString.UsuarioId, PadroesString.MinisterioId);

            Assert.IsTrue(eAdministrador);
        }

        [TestMethod]
        public async Task RetornaFalseQuandoUsuarioNaoEAdministrador()
        {
            var repositorio = new MinisterioRepositorio(BancoContextoFalso.St());

            var ministerio = await repositorio.PegarPorId(PadroesString.MinisterioId);
            ministerio.AdicionarUsuario(_usuarioNovo);

            await BancoContextoFalso.St().SaveChangesAsync();

            var eAdministrador = await repositorio.EAdministrador(_usuarioNovo.Id, PadroesString.MinisterioId);

            Assert.IsFalse(eAdministrador);

            BancoContextoFalso.St().RestaurarBanco();
        }

        [TestMethod]
        public async Task RetornaFalseQuandoIdInvalidoEAdministrador()
        {
            var repositorio = new MinisterioRepositorio(BancoContextoFalso.St());

            var eAdministrador = await repositorio.EAdministrador(Guid.NewGuid().ToString("N"), PadroesString.MinisterioId);

            Assert.IsFalse(eAdministrador);
        }

        #endregion

        #region Contar

        [TestMethod]
        public async Task Retornar1QuandoContarMinisterios()
        {
            var repositorio = new MinisterioRepositorio(BancoContextoFalso.St());

            Assert.AreEqual(1, await repositorio.Contar());
        }

        #endregion
    }
}
