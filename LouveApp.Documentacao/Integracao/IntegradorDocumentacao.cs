using LouveApp.Documentacao.Exemplos;
using LouveApp.Documentacao.Filtros;
using LouveApp.Dominio.Sistema;
using LouveApp.Dominio.Sistema.Padroes;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;

namespace LouveApp.Documentacao.Integracao
{
    public static class IntegradorDocumentacao
    {
        public static void ConfigurarDocumentacao(this IServiceCollection services, string nomeAssembly)
        {
            var nomeAplicacao= Configuracoes.NomeAplicacao;
            var versao = Configuracoes.VersaoAplicacao;

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc(versao, new Info
                {
                    Title = nomeAplicacao,
                    Version = versao,
                    Description = $"UsuarioId Padrão: {PadroesString.UsuarioId1}\n" +
                                  $"MinisterioId Padrão: {PadroesString.MinisterioId1}"
                });

                s.ExampleFilters();
                var xmlFile = $"{nomeAssembly}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                s.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "Autorização JWT usando o esquema Bearer. Exemplo: \"Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                s.AddSecurityRequirement(security);
                s.OperationFilter<BadRequestFiltro>();
            });

            services.AddSwaggerExamplesFromAssemblyOf<RegistrarUsuarioComandoExemplo>();
        }

        public static void UsarDocumentacao(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint($"/swagger/{Configuracoes.VersaoAplicacao}/swagger.json"
                    , $"{Configuracoes.NomeAplicacao} API {Configuracoes.VersaoAplicacao}");
                x.RoutePrefix = string.Empty;
            });
        }
    }
}
