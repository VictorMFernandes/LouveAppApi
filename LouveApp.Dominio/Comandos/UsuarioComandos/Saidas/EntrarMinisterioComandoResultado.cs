using LouveApp.Compartilhado.Comandos;

namespace LouveApp.Dominio.Comandos.UsuarioComandos.Saidas
{
    public class EntrarMinisterioComandoResultado : IComandoResultado
    {
        #region Propriedades Api

        public string MinisterioId { get; }
        public string MinisterioNome { get; }

        #endregion

        #region Construtores

        public EntrarMinisterioComandoResultado(string ministerioId, string ministerioNome)
        {
            MinisterioId = ministerioId;
            MinisterioNome = ministerioNome;
        }

        #endregion
    }
}
