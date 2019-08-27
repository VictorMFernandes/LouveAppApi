using LouveApp.Compartilhado.Padroes;
using LouveApp.Dominio.Comandos.EscalaComandos.Entradas;
using LouveApp.Dominio.Comandos.UsuarioComandos.SubEntidade;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;

namespace LouveApp.Documentacao.Exemplos
{
    public class RegistrarEscalaComandoExemplo : IExamplesProvider<RegistrarEscalaComando>
    {
        public RegistrarEscalaComando GetExamples()
        {
            return new RegistrarEscalaComando(DateTime.Now.AddDays(5)
            , new List<UsuarioInstrumentos>
            {
                new UsuarioInstrumentos(PadroesString.UsuarioId1, new List<string>{ PadroesString.InstrumentoId3})
            }
            , new List<string> { PadroesString.MusicaId1, PadroesString.MusicaId2 });
        }
    }
}
