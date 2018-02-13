using BiblioPopApp.Dominio.Repositorios;
using BiblioPopApp.RepositorioNetFull;
using BiblioPopApp.RepositorioNetFull.EF.Contextos;
using Unity;

namespace BiblioPopApp.ControleDependenciaNetFull
{
    public static class Resolve
    {
        private static UnityContainer _container;

        public static T InstanciaDe<T>()
        {
            if (_container == null)
            {
                var stringConexao = "Data Source=localhost;Initial Catalog=BiblioPop;Integrated Security=False;User Id=bibliopopapp;Password=pwd1";
                var contextoGeral = new ContextoGeral(stringConexao);

                _container = new UnityContainer();
                _container.RegisterInstance<ContextoGeral>(contextoGeral);

                _container.RegisterType<IRepositorioAutor, RepositorioAutor>();
                _container.RegisterType<Aplicacao.RegistrarAutor.RegistrarAutor>();

                _container.RegisterType<IRepositorioEditora, RepositorioEditora>();
                _container.RegisterType<Aplicacao.RegistrarEditora.RegistrarEditora>();

                _container.RegisterType<IRepositorioLivro, RepositorioLivro>();
                _container.RegisterType<Aplicacao.RegistrarLivro.RegistrarLivro>();

                _container.RegisterType<Aplicacao.ConsultarAcervo.ConsultarAcervo>();
            }

            return _container.Resolve<T>();
        }
    }
}
