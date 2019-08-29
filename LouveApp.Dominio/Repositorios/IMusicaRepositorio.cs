using LouveApp.Dominio.Comandos.MusicaComandos.Saidas;
using LouveApp.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LouveApp.Dominio.Repositorios
{
    public interface IMusicaRepositorio
    {
        Task<IEnumerable<PegarMusicaComandoResultado>> PegarPorMinisterio(string ministerioId);
        Task<Musica> PegarPorIdComUsuariosDoMinisterio(string musicaId);
        void Atualizar(Musica musica);
        Task<IEnumerable<PegarMusicaComandoResultado>> PegarPorNomeEArtista(string ministerioId, string nome, string artista);
    }
}
