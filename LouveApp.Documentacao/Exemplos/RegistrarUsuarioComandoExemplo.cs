using LouveApp.Compartilhado.Padroes;
using LouveApp.Dominio.Comandos.UsuarioComandos.Entradas;
using Swashbuckle.AspNetCore.Filters;

namespace LouveApp.Documentacao.Exemplos
{
    public class RegistrarUsuarioComandoExemplo : IExamplesProvider<RegistrarUsuarioComando>
    {
        public RegistrarUsuarioComando GetExamples()
        {
            return new RegistrarUsuarioComando("Naruto Uzumaki", "naruto@email.com"
                , PadroesString.SenhaValida, PadroesString.SenhaValida);
        }
    }
}
