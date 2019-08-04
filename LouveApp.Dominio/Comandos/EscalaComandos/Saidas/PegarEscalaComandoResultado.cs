﻿using System;
using System.Collections.Generic;
using LouveApp.Compartilhado.Comandos;
using LouveApp.Dominio.Comandos.MusicaComandos.Saidas;
using LouveApp.Dominio.Comandos.UsuarioComandos.Saidas;

namespace LouveApp.Dominio.Comandos.EscalaComandos.Saidas
{
    public class PegarEscalaComandoResultado : IComandoResultado
    {
        #region Propriedades
        public string Id { get; set; }
        public DateTime Data { get; set; }
        public IEnumerable<PegarUsuarioComandoResultado> Usuarios { get; set; }
        public IEnumerable<PegarMusicaComandoResultado> Musicas { get; set; }

        #endregion
    }
}
