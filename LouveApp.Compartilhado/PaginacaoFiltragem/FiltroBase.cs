﻿namespace LouveApp.Compartilhado.PaginacaoFiltragem
{
    public abstract class FiltroBase
    {
        public int Pagina { get; set; }
        public int TamanhoPagina { get; set; }

        protected FiltroBase()
        {
            Pagina = 1;
            TamanhoPagina = 100;
        }
    }
}
