using Applications.App;
using Applications.Interfaces;
using Domain.Interfaces.Generics;
using Domain.Interfaces.IRegistroDePonto;
using Infra.Configuracao;
using Infra.Repositorio;
using Infra.Repositorio.Genericos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApiRegistroDePonto
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            // CONFIGURAÇÕES DO IIS
            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });
            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
            });

            // INTERFACE E REPOSITORIO
            services.AddSingleton(typeof(IGeneric<>), typeof(RepositorioGenerico<>));
            services.AddSingleton<InterfaceRegistroDePonto, RepositorioRegistroDePonto>();

            // INTERFACE APLICAÇÃO
            services.AddSingleton<InterfaceAppRegistroDePonto, AppRegistroDePonto>();
            services.AddDbContext<Contexto>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("Contexto")));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
