using LouveApp.Compartilhado.Extensoes;
using LouveApp.Dominio.Enums;
using LouveApp.Dominio.Gerenciadores;
using LouveApp.Dominio.Sistema.Padroes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace LouveApp.Api.Extensoes
{
    public static class ServiceCollectionExtensoes
    {
        public static void ConfigurarIoC(this IServiceCollection services)
        {
            // Gerenciadores
            services.AddTransient<UsuarioGerenciador, UsuarioGerenciador>();
            services.AddTransient<MinisterioGerenciador, MinisterioGerenciador>();
            services.AddTransient<EscalaGerenciador, EscalaGerenciador>();
            services.AddTransient<MusicaGerenciador, MusicaGerenciador>();
            services.AddTransient<FotoGerenciador, FotoGerenciador>();
            services.AddTransient<AutenticacaoGerenciador, AutenticacaoGerenciador>();
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

        public static void ConfigurarComportamentosApi(this IServiceCollection services)
        {
            services
            .Configure<ApiBehaviorOptions>(abo =>
            abo.InvalidModelStateResponseFactory = actionContext => new BadRequestObjectResult(new
            {
                sucesso = false,
                erros = PadroesMensagens.ParametrosInvalidos
            }));
        }
    }
}
