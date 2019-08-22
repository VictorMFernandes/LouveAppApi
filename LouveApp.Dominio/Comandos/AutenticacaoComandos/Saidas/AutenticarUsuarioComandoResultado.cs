using System.Collections.Generic;
using LouveApp.Compartilhado.Comandos;
using LouveApp.Dominio.Comandos.MinisterioComandos.Saidas;

namespace LouveApp.Dominio.Comandos.AutenticacaoComandos.Saidas
{
    public class AutenticarUsuarioComandoResultado : IComandoResultado
    {
        #region Propriedades Api

        public string Token { get; set; }
        public string Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string FotoUrl { get; private set; }
        public IEnumerable<PegarMinisterioComandoResultado> Ministerios { get; set; }

        #endregion
    }
}