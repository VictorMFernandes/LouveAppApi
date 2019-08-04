using LouveApp.Compartilhado.Comandos;

namespace LouveApp.Dominio.Comandos.MusicaComandos.Saidas
{
    public class RegistrarMusicaComandoResultado : IComandoResultado
    {
        #region Propriedades Api

        public string Id { get; }
        public string Nome { get; }

        #endregion

        #region Construtores

        public RegistrarMusicaComandoResultado(string id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        #endregion
    }
}
