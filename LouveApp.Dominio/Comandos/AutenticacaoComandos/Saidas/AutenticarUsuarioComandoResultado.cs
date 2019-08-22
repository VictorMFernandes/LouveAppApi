using System.Collections.Generic;
using LouveApp.Compartilhado.Comandos;
using LouveApp.Dominio.Comandos.MinisterioComandos.Saidas;
using LouveApp.Dominio.Entidades;

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

        public AutenticarUsuarioComandoResultado(string token, Usuario usuario, IEnumerable<PegarMinisterioComandoResultado> ministerios)
        {
            Token = token;
            Id = usuario.Id;
            Nome = usuario.ToString();
            Email = usuario.Email.ToString();
            FotoUrl = usuario.Foto.ToString();
            Ministerios = ministerios;
        }
    }
}
