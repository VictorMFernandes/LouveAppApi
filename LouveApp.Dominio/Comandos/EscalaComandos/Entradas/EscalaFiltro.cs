using LouveApp.Compartilhado.PaginacaoFiltragem;
using LouveApp.Dominio.Sistema;
using System;

namespace LouveApp.Dominio.Comandos.EscalaComandos.Entradas
{
    public class EscalaFiltro : FiltroBase
    {
        private DateTime _dataMinima;
        public DateTime DataMinima
        {
            get => _dataMinima <= Configuracoes.DataMinima ? Configuracoes.DataMinima : _dataMinima;
            set
            {
                _dataMinima = value;
            }
        }

        public EscalaFiltro()
        {
            TamanhoPagina = 2;
        }
    }
}
