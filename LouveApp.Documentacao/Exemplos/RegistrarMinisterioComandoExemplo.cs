using LouveApp.Dominio.Comandos.MinisterioComandos.Entradas;
using Swashbuckle.AspNetCore.Filters;

namespace LouveApp.Documentacao.Exemplos
{
    public class RegistrarMinisterioComandoExemplo : IExamplesProvider<RegistrarMinisterioComando>
    {
        public RegistrarMinisterioComando GetExamples()
        {
            return new RegistrarMinisterioComando("Ministério do Rock");
        }
    }
}
