using System.Threading.Tasks;

namespace LouveApp.Compartilhado.Transacoes
{
    public interface IUow
    {
        Task<bool> Salvar();
        void RollBack();
    }
}
