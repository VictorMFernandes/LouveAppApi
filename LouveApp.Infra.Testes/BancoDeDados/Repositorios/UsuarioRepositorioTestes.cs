using System;
using System.Linq;
using System.Threading.Tasks;
using LouveApp.Compartilhado.Padroes;
using LouveApp.Dominio.ValueObjects;
using LouveApp.Infra.BancoDeDados.Contexto;
using LouveApp.Infra.BancoDeDados.Repositorios;
using LouveApp.Infra.Testes.BancoDeDados.Falsos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LouveApp.Infra.Testes.BancoDeDados.Repositorios
{
    [TestClass]
    public class UsuarioRepositorioTestes
    {
        #region PegarPorId

        [TestMethod]
        public async Task RetornaUsuarioQuandoIdExiste()
        {
            using (var contexto = new BancoContexto(BancoContextoFalso.St()._opcoesDb))
            {
                var repositorio = new UsuarioRepositorio(contexto);

                var retorno = await repositorio.PegarPorId(PadroesString.UsuarioId);
                Assert.IsNotNull(retorno);
            }
        }

        [TestMethod]
        public async Task RetornaNullQuandoIdNaoExiste()
        {
            using (var contexto = new BancoContexto(BancoContextoFalso.St()._opcoesDb))
            {
                var repositorio = new UsuarioRepositorio(contexto);

                var retorno = await repositorio.PegarPorId(Guid.NewGuid().ToString());
                Assert.IsNull(retorno);
            }
        }

        #endregion

        #region PegarPorIdSemRastrear

        [TestMethod]
        public async Task RetornaUsuarioSemRastrearQuandoIdExiste()
        {
            using (var contexto = new BancoContexto(BancoContextoFalso.St()._opcoesDb))
            {
                var repositorio = new UsuarioRepositorio(contexto);

                var retorno = await repositorio.PegarPorIdSemRastrear(PadroesString.UsuarioId);
                Assert.IsNotNull(retorno);
            }
        }

        [TestMethod]
        public async Task RetornaNullSemRastrearQuandoIdNaoExiste()
        {
            using (var contexto = new BancoContexto(BancoContextoFalso.St()._opcoesDb))
            {
                var repositorio = new UsuarioRepositorio(contexto);

                var retorno = await repositorio.PegarPorIdSemRastrear(Guid.NewGuid().ToString());
                Assert.IsNull(retorno);
            }
        }

        [TestMethod]
        public async Task RetornaUsuarioSemRastrearComInstrumentosQuandoIdExiste()
        {
            using (var contexto = new BancoContexto(BancoContextoFalso.St()._opcoesDb))
            {
                var repositorio = new UsuarioRepositorio(contexto);

                var retorno = await repositorio.PegarPorIdSemRastrear(PadroesString.UsuarioId);

                Assert.IsTrue(retorno.Instrumentos.Any());
            }
        }

        #endregion

        #region PegarAutenticado

        [TestMethod]
        public async Task RetornaUsuarioAutenticadoQuandoLoginSenhaCorretos()
        {
            using (var contexto = new BancoContexto(BancoContextoFalso.St()._opcoesDb))
            {
                var repositorio = new UsuarioRepositorio(contexto);

                var retorno = await repositorio.PegarAutenticado(PadroesString.UsuarioLogin, Autenticacao.EncriptarSenha(PadroesString.UsuarioSenha));
                Assert.IsNotNull(retorno);
            }
        }

        [TestMethod]
        public async Task RetornaNullQuandoLoginIncorreto()
        {
            using (var contexto = new BancoContexto(BancoContextoFalso.St()._opcoesDb))
            {
                var repositorio = new UsuarioRepositorio(contexto);

                var retorno = await repositorio.PegarAutenticado("loginincorreto", Autenticacao.EncriptarSenha(PadroesString.UsuarioSenha));
                Assert.IsNull(retorno);
            }
        }

        [TestMethod]
        public async Task RetornaNullQuandoSenhaIncorreta()
        {
            using (var contexto = new BancoContexto(BancoContextoFalso.St()._opcoesDb))
            {
                var repositorio = new UsuarioRepositorio(contexto);

                var retorno = await repositorio.PegarAutenticado(PadroesString.UsuarioLogin, "senhaincorreta");
                Assert.IsNull(retorno);
            }
        }

        [TestMethod]
        public async Task RetornaUsuarioAutenticadoComMinisteriosQuandoLoginSenhaCorretos()
        {
            using (var contexto = new BancoContexto(BancoContextoFalso.St()._opcoesDb))
            {
                var repositorio = new UsuarioRepositorio(contexto);

                var retorno = await repositorio.PegarAutenticado(PadroesString.UsuarioLogin, Autenticacao.EncriptarSenha(PadroesString.UsuarioSenha));

                Assert.IsTrue(retorno.Ministerios.Any());
            }
        }

        #endregion

        #region IdExiste

        [TestMethod]
        public async Task RetornaTrueQuandoIdExiste()
        {
            using (var contexto = new BancoContexto(BancoContextoFalso.St()._opcoesDb))
            {
                var repositorio = new UsuarioRepositorio(contexto);

                var retorno = await repositorio.IdExiste(PadroesString.UsuarioId);
                Assert.IsTrue(retorno);
            }
        }

        [TestMethod]
        public async Task RetornaFalseQuandoIdNaoExiste()
        {
            using (var contexto = new BancoContexto(BancoContextoFalso.St()._opcoesDb))
            {
                var repositorio = new UsuarioRepositorio(contexto);

                var retorno = await repositorio.IdExiste(Guid.NewGuid().ToString());
                Assert.IsFalse(retorno);
            }
        }

        #endregion

        #region EmailExiste

        [TestMethod]
        public async Task RetornaTrueQuandoEmailExiste()
        {
            using (var contexto = new BancoContexto(BancoContextoFalso.St()._opcoesDb))
            {
                var repositorio = new UsuarioRepositorio(contexto);

                var retorno = await repositorio.EmailExiste(PadroesString.UsuarioEmail);
                Assert.IsTrue(retorno);
            }
        }

        [TestMethod]
        public async Task RetornaFalseQuandoEmailNaoExiste()
        {
            using (var contexto = new BancoContexto(BancoContextoFalso.St()._opcoesDb))
            {
                var repositorio = new UsuarioRepositorio(contexto);

                var retorno = await repositorio.EmailExiste("emailquenaoexiste@email.com");
                Assert.IsFalse(retorno);
            }
        }
        
        #endregion
    }
}
