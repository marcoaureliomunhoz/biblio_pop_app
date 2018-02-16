using BiblioPopApp.Dominio.Repositorios;
using BiblioPopApp.RepositorioNetFull;
using BiblioPopApp.RepositorioNetFull.EF.Contextos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Unity;

namespace BiblioPopAppApiNetFull
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Serviços e configuração da API da Web

            var container = new UnityContainer();
            //container.RegisterType<IProductRepository, ProductRepository>(new HierarchicalLifetimeManager());
            var stringConexao = "Data Source=localhost;Initial Catalog=BiblioPop;Integrated Security=False;User Id=bibliopopapp;Password=pwd1";
            var contextoGeral = new ContextoGeral(stringConexao);
            container.RegisterInstance<ContextoGeral>(contextoGeral);
            container.RegisterType<IRepositorioAutor, RepositorioAutor>();
            container.RegisterType<BiblioPopApp.Aplicacao.RegistrarAutor.RegistrarAutor>();
            container.RegisterType<IRepositorioEditora, RepositorioEditora>();
            container.RegisterType<BiblioPopApp.Aplicacao.RegistrarEditora.RegistrarEditora>();
            container.RegisterType<IRepositorioLivro, RepositorioLivro>();
            container.RegisterType<BiblioPopApp.Aplicacao.RegistrarLivro.RegistrarLivro>();
            container.RegisterType<BiblioPopApp.Aplicacao.ConsultarAcervo.ConsultarAcervo>();
            config.DependencyResolver = new UnityResolver(container);

            //config.EnableCors();
            var politicas = new EnableCorsAttribute(
                origins: "*",
                methods: "*",
                headers: "*"
            );
            config.EnableCors(politicas);

            // Rotas da API da Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
