﻿using LouveApp.Dominio.Comandos.MusicaComandos.Entradas;
using LouveApp.Dominio.Sistema.Padroes;
using Swashbuckle.AspNetCore.Filters;

namespace LouveApp.Documentacao.Exemplos
{
    public class AtualizarMusicaComandoExemplo : IExamplesProvider<AtualizarMusicaComando>
    {
        public AtualizarMusicaComando GetExamples()
        {
            return new AtualizarMusicaComando(PadroesString.MusicaNome1
            , "https://www.vagalume.com.br/o-rei-leao/somos-um.html"
            , "https://www.cifraclub.com.br/system-of-a-down/chop-suey/"
            , "https://www.youtube.com/watch?v=CSvFpBOe8eY"
            , "O Rei Leão", "F#", 60, "Adoração");
        }
    }
}
