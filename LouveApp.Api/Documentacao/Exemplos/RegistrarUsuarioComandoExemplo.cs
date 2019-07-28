using LouveApp.Dominio.Comandos.UsuarioComandos.Entradas;
using LouveApp.Dominio.Sistema.Exemplos;
using Swashbuckle.AspNetCore.Filters;

namespace LouveApp.Api.Documentacao.Exemplos
{
    public class RegistrarUsuarioComandoExemplo : IExamplesProvider<RegistrarUsuarioComando>
    {
        public RegistrarUsuarioComando GetExamples()
        {
            return ExemplosComando.RegistrarUsuario;
        }
    }
}
