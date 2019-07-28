using System.Threading.Tasks;

namespace LouveApp.Infra.BancoDeDados.Transacoes
{
    public interface IUow
    {
        Task<bool> Salvar();
        void RollBack();
    }
}
