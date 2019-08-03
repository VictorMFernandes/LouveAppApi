using LouveApp.Dominio.Sistema;
using LouveApp.Infra.BancoDeDados.Contexto;
using LouveApp.Infra.BancoDeDados.Repositorios;
using LouveApp.Infra.BancoDeDados.Transacoes;
using Microsoft.EntityFrameworkCore;

namespace LouveApp.Infra.Testes.BancoDeDados.Falsos
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
    }
}
