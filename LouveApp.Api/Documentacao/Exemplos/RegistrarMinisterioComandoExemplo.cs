using LouveApp.Dominio.Comandos.MinisterioComandos.Entradas;
using LouveApp.Dominio.Sistema.Exemplos;
using Swashbuckle.AspNetCore.Filters;

namespace LouveApp.Api.Documentacao.Exemplos
{
    public class RegistrarMinisterioComandoExemplo : IExamplesProvider<RegistrarMinisterioComando>
    {
        public RegistrarMinisterioComando GetExamples()
        {
            return ExemplosComando.RegistrarMinisterio;
        }
    }
}
