using System;
using LouveApp.Compartilhado.PaginacaoFiltragem;

namespace LouveApp.Dominio.Comandos.EscalaComandos.Entradas
{
    public class EscalaFiltro : FiltroBase
    {
        public DateTime DataMinima { get; set; }

        public EscalaFiltro()
        {
            TamanhoPagina = 2;
        }
    }
}
