using System.Threading.Tasks;

namespace LouveApp.Dominio.Testes.Gerenciadores
{
    internal interface IGerenciadorTestes
    {
        void ValidarComandosAoExecutalos();
        Task RetornarNullQuandoComandoInvalido();
    }
}
