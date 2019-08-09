using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LouveApp.Compartilhado.PaginacaoFiltragem
{
    public class ListaPaginada<T> : List<T>
    {
        public int PaginaAtual { get; set; }
        public int QtdPaginas { get; set; }
        public int TamanhoPagina { get; set; }
        public int QtdTotal { get; set; }

        public ListaPaginada(IEnumerable<T> items, int count, int numeroDaPagina, int tamanhoDaPagina)
        {
            QtdTotal = count;
            TamanhoPagina = tamanhoDaPagina;
            PaginaAtual = numeroDaPagina;
            QtdPaginas = (int)Math.Ceiling(count / (double)tamanhoDaPagina);
            AddRange(items);
        }

        public static ListaPaginada<T> Criar(IQueryable<T> fonte,
            int numeroDaPagina, int tamanhoDaPagina)
        {
            var count = fonte.Count();
            var itens = fonte.Skip((numeroDaPagina - 1) * tamanhoDaPagina).Take(tamanhoDaPagina).ToList();
            return new ListaPaginada<T>(itens, count, numeroDaPagina, tamanhoDaPagina);
        }

        public bool EstaNaUltimaPagina()
        {
            return PaginaAtual == QtdPaginas;
        }

        public bool EstaNaPrimeiraPagina()
        {
            return PaginaAtual == 1;
        }
    }
}
