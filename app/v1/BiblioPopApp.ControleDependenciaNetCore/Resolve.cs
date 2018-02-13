using BiblioPopApp.Dominio.Repositorios;
using BiblioPopApp.RepositorioNetCore;
using BiblioPopApp.RepositorioNetCore.EF.Contextos;
using Microsoft.EntityFrameworkCore;
using Unity;

namespace BiblioPopApp.ControleDependenciaNetCore
{
    public static class Resolve
    {
        private static UnityContainer _container;

        public static T InstanciaDe<T>()
        {
            if (_container == null)
            {
                var builder = new DbContextOptionsBuilder<ContextoGeral>();
                builder.UseSqlServer("Server=localhost;Database=BiblioPop;Trusted_Connection=True;");
                var contextoGeral = new ContextoGeral(builder.Options);

                _container = new UnityContainer();
                _container.RegisterInstance<ContextoGeral>(contextoGeral);
                _container.RegisterType<IRepositorioAutor, RepositorioAutor>();
                _container.RegisterType<Aplicacao.RegistrarAutor.RegistrarAutor>();
            }

            return _container.Resolve<T>();
        }
    }
}
