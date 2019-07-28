using System.Threading.Tasks;

namespace LouveApp.Compartilhado.Comandos
{
    public interface IComandoGerenciador<T> where T : IComando
    {
        Task<IComandoResultado> Executar(T comando);
    }
}
