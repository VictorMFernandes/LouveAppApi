using LouveApp.Api.Extensoes;
using LouveApp.Api.Middlewares;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LouveApp.Dominio.Sistema;
using Newtonsoft.Json;
using LouveApp.Dal.Servicos.PushNotification.Extensoes;
using LouveApp.Documentacao.Integracao;
using System.Reflection;
using LouveApp.Servicos.Fotos.Integracao;
using LouveApp.Servicos.Email.Integracao;
using LouveApp.Dal.Integracao;

namespace LouveApp.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Configuracoes.InicializarConfiguracoes(Configuration, env.IsDevelopment());
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AdicionarBancoContexto();

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
            services.ConfigurarDal();
            services.ConfigurarIoC();
            services.ConfigurarServicoEmail(Configuration);
            services.ConfigurarServicoFotos(Configuration);
            services.ConfigurarPushNotification();
            services.ConfigurarComportamentosApi();

            services.ConfigurarDocumentacao(Assembly.GetExecutingAssembly().GetName().Name);
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Configuracoes.EmDesenvolvimento)
                app.UseDeveloperExceptionPage();

            app.UsarDocumentacao();

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
        }
    }
}
