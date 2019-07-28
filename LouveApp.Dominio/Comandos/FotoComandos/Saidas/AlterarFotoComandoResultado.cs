using LouveApp.Compartilhado.Comandos;

namespace LouveApp.Dominio.Comandos.FotoComandos.Saidas
{
    public class AlterarFotoComandoResultado : IComandoResultado
    {
        #region Propriedades Api

        public string FotoUrl { get; private set; }

        #endregion

        #region Construtores

        public AlterarFotoComandoResultado(string fotoUrl)
        {
            FotoUrl = fotoUrl;
        }

        #endregion
    }
}
