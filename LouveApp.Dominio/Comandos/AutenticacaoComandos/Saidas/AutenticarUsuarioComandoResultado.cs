using LouveApp.Compartilhado.Comandos;
using LouveApp.Dominio.Entidades;

namespace LouveApp.Dominio.Comandos.AutenticacaoComandos.Saidas
{
    public class AutenticarUsuarioComandoResultado : IComandoResultado
    {
        #region Propriedades Api

        public string Token { get; private set; }
        public string Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Foto { get; private set; }

        #endregion

        #region Construtores

        public AutenticarUsuarioComandoResultado(Usuario usuario, string token)
        {
            Id = usuario.Id;
            Nome = usuario.Nome.ToString();
            Email = usuario.Email.ToString();
            Foto = usuario.Foto?.Url;
            Token = token;
        }

        #endregion
    }
}
