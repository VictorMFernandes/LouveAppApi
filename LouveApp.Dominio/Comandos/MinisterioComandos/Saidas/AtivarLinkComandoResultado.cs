using LouveApp.Compartilhado.Comandos;

namespace LouveApp.Dominio.Comandos.MinisterioComandos.Saidas
{
    public class AtivarLinkComandoResultado : IComandoResultado
    {
        #region Propriedades Api

        public string LinkGerado { get; }

        #endregion

        #region Construtores
        public AtivarLinkComandoResultado(string linkGerado)
        {
            LinkGerado = linkGerado;
        }

        #endregion
    }
}
