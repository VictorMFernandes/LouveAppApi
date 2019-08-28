using LouveApp.Dominio.Sistema;
using LouveApp.Dal.Contexto;
using LouveApp.Dal.Repositorios;
using LouveApp.Dal.Transacoes;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.Data.Sqlite;

namespace LouveApp.Dal.Testes.Falsos
{
    internal class BancoContextoFalso : BancoContexto
    {
        public static readonly DbContextOptions<BancoContexto> _opcoesDb = new DbContextOptionsBuilder<BancoContexto>()
                                                                            .UseSqlite(Configuracoes.ConnStringTestes)
                                                                            .Options;

        public SqliteConnection Conexao { get; private set; }

        private BancoContextoFalso()
            : base(_opcoesDb)
        {
            Configuracoes.ConnString = Configuracoes.ConnStringTestes;

            using (var context = new BancoContexto(_opcoesDb))
            {
                using (var conn = new SqliteConnection(Configuracoes.ConnString))
                {
                    Conexao = conn;
                    context.Database.EnsureCreated();
                    var usuarioRepo = new UsuarioRepositorio(context, conn);
                    var ministerioRepo = new MinisterioRepositorio(context, conn);
                    var instrumentoRepo = new InstrumentoRepositorio(context, conn);
                    var uow = new Uow(context);

                    var semeador = new SemeadorBd(usuarioRepo, ministerioRepo, instrumentoRepo, uow);

                    semeador.SemearBancoDeDados();
                }
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
