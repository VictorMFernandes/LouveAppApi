using LouveApp.Dominio.Entidades;
using LouveApp.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LouveApp.Dominio.Comandos.MinisterioComandos.Saidas;

namespace LouveApp.Testes.Falsos
{
    internal class MinisterioRepositorioFalso : IMinisterioRepositorio
    {
        public void Atualizar(Ministerio ministerio) { }

        public Task<IEnumerable<PegarMinisteriosComandoResultado>> PegarPorUsuario(string id)
        {
            throw new NotImplementedException();
        }

        public void Criar(Ministerio ministerio) { }

        public Task<Ministerio> PegarPorId(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Ministerio> PegarPorLinkConvite(string linkConvite)
        {
            throw new NotImplementedException();
        }
    }
}
