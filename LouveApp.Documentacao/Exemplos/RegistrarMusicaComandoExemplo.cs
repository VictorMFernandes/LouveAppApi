using LouveApp.Dominio.Comandos.MusicaComandos.Entradas;
using Swashbuckle.AspNetCore.Filters;

namespace LouveApp.Documentacao.Exemplos
{
    public class RegistrarMusicaComandoExemplo : IExamplesProvider<RegistrarMusicaComando>
    {
        public RegistrarMusicaComando GetExamples()
        {
            return new RegistrarMusicaComando("Chop Suey!"
            , "https://www.letras.mus.br/system-of-a-down/39417/"
            , "https://www.cifraclub.com.br/system-of-a-down/chop-suey/"
            , "https://www.youtube.com/watch?v=CSvFpBOe8eY"
            , "System of a Down", "C#", 60, string.Empty);
        }
    }
}
