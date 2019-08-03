using LouveApp.Dominio.Comandos.EscalaComandos.Entradas;
using LouveApp.Dominio.Sistema.Exemplos;
using Swashbuckle.AspNetCore.Filters;

namespace LouveApp.Api.Documentacao.Exemplos
{
    public class RegistrarEscalaComandoExemplo : IExamplesProvider<RegistrarEscalaComando>
    {
        public RegistrarEscalaComando GetExamples()
        {
            return ExemplosComando.RegistrarEscala;
        }
    }
}
