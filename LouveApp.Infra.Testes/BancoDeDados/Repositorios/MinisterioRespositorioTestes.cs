using LouveApp.Compartilhado.Padroes;
using LouveApp.Infra.BancoDeDados.Repositorios;
using LouveApp.Infra.Testes.BancoDeDados.Falsos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace LouveApp.Infra.Testes.BancoDeDados.Repositorios
{
    [TestClass]
    public class MinisterioRespositorioTestes
    {
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
    }
}
