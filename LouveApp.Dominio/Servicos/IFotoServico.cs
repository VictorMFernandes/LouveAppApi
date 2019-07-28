using LouveApp.Dominio.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace LouveApp.Dominio.Servicos
{
    public interface IFotoServico
    {
        Foto UploadFoto(IFormFile arquivo);
        bool DeletarFoto(string idPublico);
    }
}
