using LouveApp.Compartilhado.Transacoes;
using LouveApp.Dal.Contexto;
using System.Threading.Tasks;

namespace LouveApp.Dal.Transacoes
{
    public class Uow : IUow
    {
        private readonly BancoContexto _contexto;

        public Uow(BancoContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> Salvar()
        {
            return await _contexto.SaveChangesAsync() > 0;
        }

        public void RollBack()
        {
            // Não faz nada
        }
    }
}
