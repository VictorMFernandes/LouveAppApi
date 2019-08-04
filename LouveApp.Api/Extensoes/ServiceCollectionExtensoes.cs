using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using LouveApp.Api.Documentacao.Exemplos;
using LouveApp.Api.Documentacao.Filtros;
using LouveApp.Compartilhado.Extensoes;
using LouveApp.Compartilhado.Padroes;
using LouveApp.Dominio.Enums;
using LouveApp.Dominio.Gerenciadores;
using LouveApp.Dominio.Repositorios;
using LouveApp.Dominio.Servicos;
using LouveApp.Infra.BancoDeDados.Contexto;
using LouveApp.Infra.BancoDeDados.Repositorios;
using LouveApp.Infra.BancoDeDados.Transacoes;
using LouveApp.Infra.Servicos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;

namespace LouveApp.Api.Extensoes
{
    public static class ServiceCollectionExtensoes
    {
        public static void ConfigurarIoC(this IServiceCollection services)
        {
            // Gerais
            services.AddTransient<IUow, Uow>();
            services.AddTransient<ISemeadorBd, SemeadorBd>();
            // Gerenciadores
            services.AddTransient<UsuarioGerenciador, UsuarioGerenciador>();
            services.AddTransient<MinisterioGerenciador, MinisterioGerenciador>();
            services.AddTransient<EscalaGerenciador, EscalaGerenciador>();
            services.AddTransient<MusicaGerenciador, MusicaGerenciador>();
            services.AddTransient<FotoGerenciador, FotoGerenciador>();
            services.AddTransient<AutenticacaoGerenciador, AutenticacaoGerenciador>();
            // Repositórios
            services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddTransient<IMinisterioRepositorio, MinisterioRepositorio>();
            services.AddTransient<IInstrumentoRepositorio, InstrumentoRepositorio>();
            services.AddTransient<IEscalaRepositorio, EscalaRepositorio>();
            services.AddTransient<IMusicaRepositorio, MusicaRepositorio>();
            // Serviços
            services.AddTransient<IFotoServico, FotoServico>();
        }

        public static void ConfigurarSwagger(this IServiceCollection services, string nomeAplicacao, string versao)
        {
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
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
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

        public static void ConfigurarAutenticacao(this IServiceCollection services, IConfiguration config)
        {
            var tokenSecreto = config.GetSection(EConfigSecao.TokenSecreto.EnumTextos().Nome).Value;

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                        .GetBytes(tokenSecreto)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                    RequireExpirationTime = true,
                };
            });
        }
    }
}
