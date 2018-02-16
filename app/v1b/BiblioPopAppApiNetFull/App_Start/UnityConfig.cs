using BiblioPopApp.Dominio.Repositorios;
using BiblioPopApp.RepositorioNetFull;
using BiblioPopApp.RepositorioNetFull.EF.Contextos;
using System;

using Unity;

namespace BiblioPopAppApiNetFull
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();

            var stringConexao = "Data Source=localhost;Initial Catalog=BiblioPop;Integrated Security=False;User Id=bibliopopapp;Password=pwd1";
            var contextoGeral = new ContextoGeral(stringConexao);

            //container = new UnityContainer();
            container.RegisterInstance<ContextoGeral>(contextoGeral);

            container.RegisterType<IRepositorioAutor, RepositorioAutor>();
            container.RegisterType<BiblioPopApp.Aplicacao.RegistrarAutor.RegistrarAutor>();

            container.RegisterType<IRepositorioEditora, RepositorioEditora>();
            container.RegisterType<BiblioPopApp.Aplicacao.RegistrarEditora.RegistrarEditora>();

            container.RegisterType<IRepositorioLivro, RepositorioLivro>();
            container.RegisterType<BiblioPopApp.Aplicacao.RegistrarLivro.RegistrarLivro>();

            container.RegisterType<BiblioPopApp.Aplicacao.ConsultarAcervo.ConsultarAcervo>();
        }
    }
}