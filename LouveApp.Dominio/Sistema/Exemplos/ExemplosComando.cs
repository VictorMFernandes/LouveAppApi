using LouveApp.Compartilhado.Padroes;
using LouveApp.Dominio.Comandos.AutenticacaoComandos.Entradas;
using LouveApp.Dominio.Comandos.EscalaComandos.Entradas;
using LouveApp.Dominio.Comandos.MinisterioComandos.Entradas;
using LouveApp.Dominio.Comandos.MusicaComandos.Entradas;
using LouveApp.Dominio.Comandos.UsuarioComandos.Entradas;
using System;
using System.Collections.Generic;

namespace LouveApp.Dominio.Sistema.Exemplos
{
    public class ExemplosComando
    {
        public static RegistrarUsuarioComando RegistrarUsuario = new RegistrarUsuarioComando("Naruto Uzumaki"
                , "naruto@email.com", PadroesString.SenhaValida, PadroesString.SenhaValida);

        public static AutenticarUsuarioComando AutenticarUsuario = new AutenticarUsuarioComando(PadroesString.UsuarioLogin1,
            PadroesString.SenhaValida);

        public static RegistrarMinisterioComando RegistrarMinisterio = new RegistrarMinisterioComando("Ministério do Rock");

        public static RegistrarEscalaComando RegistrarEscala = new RegistrarEscalaComando(DateTime.Now.AddDays(5)
            , new List<string> { PadroesString.UsuarioId1 }
            , new List<string> { PadroesString.MusicaId1, PadroesString.MusicaId2});
        public static RegistrarMusicaComando RegistrarMusica = new RegistrarMusicaComando("Chop Suey!", "https://www.youtube.com/watch?v=CSvFpBOe8eY");
    }
}
