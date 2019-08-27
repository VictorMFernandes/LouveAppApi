using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using LouveApp.Dominio.Servicos;
using LouveApp.Dominio.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace LouveApp.Servicos.Fotos
{
    public class FotoServico : IFotoServico
    {
        private readonly Cloudinary _cloudinary;

        public FotoServico(string cloudName, string apiKey, string apiSecret)
        {
            var acc = new Account(
                cloudName,
                apiKey,
                apiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        public Foto UploadFoto(IFormFile arquivo)
        {
            using (var stream = arquivo.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(arquivo.Name, stream),
                    Transformation = new Transformation()
                        .Width(500).Height(500).Crop("fill").Gravity("face")
                };

                var resultadoUpload = _cloudinary.Upload(uploadParams);

                return new Foto(resultadoUpload.Uri.ToString(), resultadoUpload.PublicId);
            }
        }

        public bool DeletarFoto(string idPublico)
        {
            if (string.IsNullOrEmpty(idPublico)) return true;

            var deletarParams = new DeletionParams(idPublico);
            var resultado = _cloudinary.Destroy(deletarParams);

            return resultado.Result == "ok";
        }
    }
}
