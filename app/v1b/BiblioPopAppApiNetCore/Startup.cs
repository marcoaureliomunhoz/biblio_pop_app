using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPopApp.Dominio.Repositorios;
using BiblioPopApp.RepositorioNetCore;
using BiblioPopApp.RepositorioNetCore.EF.Contextos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BiblioPopAppApiNetCore
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
            services
                .AddMvc()
                .AddJsonOptions(
                    options => {
                        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                        options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
                    }
                );

            services.AddCors(options =>
            {
                options.AddPolicy(
                    "*",
                    builder => 
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader()
                );
            });

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("*"));
            });

            var stringConexao = "Data Source=localhost;Initial Catalog=BiblioPop;Integrated Security=False;User Id=bibliopopapp;Password=pwd1";
            services.AddDbContext<ContextoGeral>(options => options.UseSqlServer(stringConexao));

            services.AddScoped<IRepositorioAutor, RepositorioAutor>();
            services.AddScoped<BiblioPopApp.Aplicacao.RegistrarAutor.RegistrarAutor>();

            services.AddScoped<IRepositorioEditora, RepositorioEditora>();
            services.AddScoped<BiblioPopApp.Aplicacao.RegistrarEditora.RegistrarEditora>();

            services.AddScoped<IRepositorioLivro, RepositorioLivro>();
            services.AddScoped<BiblioPopApp.Aplicacao.RegistrarLivro.RegistrarLivro>();

            services.AddScoped<BiblioPopApp.Aplicacao.ConsultarAcervo.ConsultarAcervo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
