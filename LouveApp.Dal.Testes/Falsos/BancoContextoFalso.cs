using LouveApp.Dominio.Sistema;
using LouveApp.Dal.Contexto;
using LouveApp.Dal.Repositorios;
using LouveApp.Dal.Transacoes;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace LouveApp.Dal.Testes.Falsos
{
    internal class BancoContextoFalso : BancoContexto
    {
        public static readonly DbContextOptions<BancoContexto> _opcoesDb = new DbContextOptionsBuilder<BancoContexto>()
                                                                            .UseSqlite(Configuracoes.ConnStringTestes)
                                                                            .Options;

        private BancoContextoFalso()
            : base(_opcoesDb)
        {
            Configuracoes.ConnString = Configuracoes.ConnStringTestes;

            using (var context = new BancoContexto(_opcoesDb))
            {
                context.Database.EnsureCreated();
                var usuarioRepo = new UsuarioRepositorio(context);
                var ministerioRepo = new MinisterioRepositorio(context);
                var instrumentoRepo = new InstrumentoRepositorio(context);
                var uow = new Uow(context);

                var semeador = new SemeadorBd(usuarioRepo, ministerioRepo, instrumentoRepo, uow);

                semeador.SemearBancoDeDados();
            }
        }

        #region Singleton

        private static BancoContextoFalso _instancia;
        public static BancoContextoFalso St()
        {
            return _instancia = _instancia ?? new BancoContextoFalso();
        }

        #endregion

        /// <summary>
        /// Reinicializa o banco com todas as entidades padrões.
        /// Deve ser executada toda vez que alguma entidade padrão do banco for alterada/removida.
        /// </summary>
        public void RestaurarBanco()
        {
            File.Delete(Configuracoes.BancoTestesCaminho);
            _instancia = null;
            Dispose();
        }
    }
}
