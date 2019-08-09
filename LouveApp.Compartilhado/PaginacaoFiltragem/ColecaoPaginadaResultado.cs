using System;
using System.Collections.Generic;
using LouveApp.Compartilhado.Comandos;

namespace LouveApp.Compartilhado.PaginacaoFiltragem
{
    public class ColecaoPaginadaResultado<T> : IComandoResultado where T: class
    {
        public IEnumerable<T> Colecao { get; }
        public Uri PgProxima { get; }
        public Uri PgAnterior { get; }

        public ColecaoPaginadaResultado(IEnumerable<T> colecao, string pgProxima, string pgAnterior)
        {
            Colecao = colecao;
            PgProxima = string.IsNullOrEmpty(pgProxima)? null : new Uri(pgProxima);
            PgAnterior = string.IsNullOrEmpty(pgAnterior) ? null : new Uri(pgAnterior);
        }
    }
}
