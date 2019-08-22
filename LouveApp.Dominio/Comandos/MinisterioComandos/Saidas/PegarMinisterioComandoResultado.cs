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

        public PegarMinisterioComandoResultado(string id, string nome, bool administrador)
        {
            Id = id;
            Nome = nome;
            Administrador = administrador;
        }
    }
}
