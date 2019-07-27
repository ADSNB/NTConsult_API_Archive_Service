using Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using System.Reflection;

namespace NTConsult_Archives_API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private IServiceCollection Services;

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            Log.Information("########## Iniciando serviços");

            Services = services;
            ConfigureMvcStructure();
            ConfigureDI();
            ConfigureBackgroundWorkQueue();
            ConfigureSwagger();

            Log.Information("########## Serviços iniciados com sucesso");
        }

        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            ConfigureSwaggerRoutes(app);
            ConfigureEnviroment(app, env);
            app.UseHttpsRedirection();
            app.UseMvc();
        }

        #region Private Methods

        private void ConfigureMvcStructure()
        {
            Services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            Log.Information("########## Serviço MVC Iniciado");
        }

        private void ConfigureDI()
        {
            Services.AddSingleton<IBackgroundWorkerDomain, BackgroundWorkerDomain>();

            Log.Information("########## Serviço de injeções de dependência Iniciado");
        }

        private void ConfigureBackgroundWorkQueue()
        {
            Services.AddSingleton<IHostedService, BackgroundWorker>();

            Log.Information("########## Serviço de execução de jobs Iniciado");
        }

        private void ConfigureSwagger()
        {
            Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("QUEUE", new Swashbuckle.AspNetCore.Swagger.Info { Title = "Queue", Description = "Fila", Version = "1.0" });
                c.SwaggerDoc("JOB", new Swashbuckle.AspNetCore.Swagger.Info { Title = "JOBS", Description = "Serviço de jobs", Version = "1.0" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        private void ConfigureSwaggerRoutes(IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/QUEUE/swagger.json", "QUEUE - FILA");
                c.SwaggerEndpoint("/swagger/JOB/swagger.json", "QUEUE - JOBS");
                c.RoutePrefix = string.Empty;
            });
            Log.Information("########## Serviço do SWAGGER Iniciado");
        }

        private void ConfigureEnviroment(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            Log.Information("########## Serviço de configuração de ambiente Iniciado");
        }

        #endregion
    }
}
