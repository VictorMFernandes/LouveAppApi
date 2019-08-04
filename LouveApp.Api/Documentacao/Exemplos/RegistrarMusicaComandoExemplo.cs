using LouveApp.Dominio.Comandos.MusicaComandos.Entradas;
using LouveApp.Dominio.Sistema.Exemplos;
using Swashbuckle.AspNetCore.Filters;

namespace LouveApp.Api.Documentacao.Exemplos
{
    public class RegistrarMusicaComandoExemplo : IExamplesProvider<RegistrarMusicaComando>
    {
        public RegistrarMusicaComando GetExamples()
        {
            return ExemplosComando.RegistrarMusica;
        }
    }
}
