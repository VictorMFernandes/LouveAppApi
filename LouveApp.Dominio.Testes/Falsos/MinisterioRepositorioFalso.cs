using System.Collections.Generic;
using System.Threading.Tasks;
using LouveApp.Dominio.Comandos.MinisterioComandos.Saidas;
using LouveApp.Dominio.Entidades;
using LouveApp.Dominio.Repositorios;

namespace LouveApp.Dominio.Testes.Falsos
{
    internal class MinisterioRepositorioFalso : IMinisterioRepositorio
    {
        public void Atualizar(Ministerio ministerio) { }

        public void Remover(string id) { }

        public Task<bool> EAdministrador(string usuarioId, string ministerioId) => Task.Run(() => false);

        public Task<IEnumerable<PegarMinisteriosComandoResultado>> PegarPorUsuario(string id) => null;

        public void Criar(Ministerio ministerio) { }

        public Task<Ministerio> PegarPorId(string id) => null;

        public Task<Ministerio> PegarPorLinkConvite(string linkConvite) => null;
    }
}
