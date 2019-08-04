using LouveApp.Api.Extensoes;
using LouveApp.Api.Middlewares;
using LouveApp.Compartilhado.Extensoes;
using LouveApp.Dominio.Enums;
using LouveApp.Dominio.Servicos;
using LouveApp.Infra.BancoDeDados.Contexto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LouveApp.Dominio.Sistema;
using Newtonsoft.Json;
using LouveApp.Infra.Servicos.Email.Extensoes;

namespace LouveApp.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        private readonly string _nomeAplicacao;
        private readonly string _versao;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            _nomeAplicacao = Configuration
                            .GetSection(EConfigSecao.NomeAplicacao.EnumTextos().Nome)
                            .Value;
            _versao = Configuration
                      .GetSection(EConfigSecao.Versao.EnumTextos().Nome)
                      .Value;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Configuracoes.ConnString = Configuration.GetConnectionString(EConfigSecao.ConexaoBd.EnumTextos().Nome);
            services.AddDbContextPool<BancoContexto>(options =>
                options.UseSqlite(Configuracoes.ConnString)
            );

            services.AddMvc(config =>
            {
                var regra = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();

                config.Filters.Add(new AuthorizeFilter(regra));
            })
            .AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddCors();

            services.ConfigurarAutenticacao(Configuration);

            services.AddResponseCompression();

            services.ConfigurarIoC();
            services.ConfigurarServicoEmail(Configuration);

            services.ConfigurarSwagger(_nomeAplicacao, _versao);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ISemeadorBd semeadorBd)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint($"/swagger/{_versao}/swagger.json", $"{_nomeAplicacao} API {_versao}");
                x.RoutePrefix = string.Empty;
            });

            app.UseCors(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
            });

            app.UseAuthentication();
            app.UseMiddleware<ErroMiddleware>();
            app.UseResponseCompression();
            app.UseMvc();

            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var serviceScope = serviceScopeFactory.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<BancoContexto>();
                dbContext.Database.EnsureCreated();
            }

            semeadorBd.SemearBancoDeDados();
        }
    }
}
