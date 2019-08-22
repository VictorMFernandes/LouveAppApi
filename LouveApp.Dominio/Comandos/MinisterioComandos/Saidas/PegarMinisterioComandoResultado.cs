using LouveApp.Compartilhado.Comandos;

namespace LouveApp.Dominio.Comandos.MinisterioComandos.Saidas
{
    public class PegarMinisterioComandoResultado : IComandoResultado
    {
        #region Propriedades

        public string Id { get; set; }
        public string Nome { get; set; }
        public bool Administrador { get; set; }

        #endregion
    }
}