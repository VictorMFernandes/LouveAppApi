using LouveApp.Compartilhado.Padroes;
using LouveApp.Dominio.Comandos.AutenticacaoComandos.Entradas;
using LouveApp.Dominio.Comandos.MinisterioComandos.Entradas;
using LouveApp.Dominio.Comandos.UsuarioComandos.Entradas;

namespace LouveApp.Dominio.Sistema.Exemplos
{
    public class ExemplosComando
    {
        public static RegistrarUsuarioComando RegistrarUsuario = new RegistrarUsuarioComando("Melissa Arroio Merlone dos Santos"
                , "melissa@email.com", "senha", "senha");

        public static AutenticarUsuarioComando AutenticarUsuario = new AutenticarUsuarioComando(PadroesString.UsuarioLogin,
            PadroesString.UsuarioSenha);

        public static RegistrarMinisterioComando RegistrarMinisterio = new RegistrarMinisterioComando("Ministério do Rock");
    }
}
