using LouveApp.Compartilhado.Comandos;

namespace LouveApp.Dominio.Comandos.MinisterioComandos.Saidas
{
    public class RegistrarMinisterioComandoResultado : IComandoResultado
    {
        #region Propriedades Api

        public string Id { get; }
        public string Nome { get; }

        #endregion

        #region Construtores
        public RegistrarMinisterioComandoResultado(string id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        #endregion
    }
}
