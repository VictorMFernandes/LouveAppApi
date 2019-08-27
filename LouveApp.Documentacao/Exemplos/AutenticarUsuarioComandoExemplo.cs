using LouveApp.Compartilhado.Padroes;
using LouveApp.Dominio.Comandos.AutenticacaoComandos.Entradas;
using Swashbuckle.AspNetCore.Filters;

namespace LouveApp.Documentacao.Exemplos
{
    public class AutenticarUsuarioComandoExemplo : IExamplesProvider<AutenticarUsuarioComando>
    {
        public AutenticarUsuarioComando GetExamples()
        {
            return new AutenticarUsuarioComando(PadroesString.UsuarioLogin1,
                PadroesString.SenhaValida);
        }
    }
}
