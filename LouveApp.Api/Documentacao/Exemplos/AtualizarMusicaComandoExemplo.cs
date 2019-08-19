using LouveApp.Dominio.Comandos.MusicaComandos.Entradas;
using LouveApp.Dominio.Sistema.Exemplos;
using Swashbuckle.AspNetCore.Filters;

namespace LouveApp.Api.Documentacao.Exemplos
{
    public class AtualizarMusicaComandoExemplo : IExamplesProvider<AtualizarMusicaComando>
    {
        public AtualizarMusicaComando GetExamples()
        {
            return ExemplosComando.AtualizarMusica;
        }
    }
}
