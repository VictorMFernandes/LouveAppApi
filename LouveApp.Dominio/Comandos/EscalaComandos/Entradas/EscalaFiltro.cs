using LouveApp.Compartilhado.PaginacaoFiltragem;
using System;

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
