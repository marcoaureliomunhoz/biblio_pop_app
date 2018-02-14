using BiblioPopApp.Aplicacao._DTO;
using BiblioPopApp.Dominio.Entidades;
using BiblioPopApp.Dominio.Repositorios;
using System.Collections.Generic;

namespace BiblioPopApp.Aplicacao.RegistrarAutor
{
    public class RegistrarAutor
    {
        private IRepositorioAutor repAutor;

        public RegistrarAutor(IRepositorioAutor repositorioAutor)
        {
            repAutor = repositorioAutor;
        }

        public Retorno.AoRealizarNovoAutor Realizar(Operacao.NovoAutor novoAutor)
        {
            var retorno = new Retorno.AoRealizarNovoAutor();

            var autor = novoAutor.Fabricar();

            if (autor.EstaEmEstadoIntegro())
            {
                var retornoAoInserirEmRepositorioAutor = repAutor.Inserir(autor);
                retorno.AutorId = retornoAoInserirEmRepositorioAutor.AutorId;
                if (retornoAoInserirEmRepositorioAutor.Problemas.Count>0)
                {
                    retorno.Mensagem = "Não foi possível registrar o novo autor.";
                    retorno.Problemas.AddRange(retornoAoInserirEmRepositorioAutor.Problemas);
                }
            }
            else
            {                
                retorno.Mensagem = "Os dados do novo autor não foram devidamente informados.";
                retorno.Problemas.AddRange(autor.Problemas);
            }
            
            return retorno;
        }

        public Retorno.AoRealizarAjusteAutor Realizar(Operacao.AjusteAutor ajusteAutor)
        {
            var retorno = new Retorno.AoRealizarAjusteAutor();

            var autor = ajusteAutor.Fabricar();

            if (autor.EstaEmEstadoIntegro())
            {
                var retornoAoAlterarEmRepositorioAutor = repAutor.Alterar(autor);
                retorno.AlterouComSucesso = retornoAoAlterarEmRepositorioAutor.AlterouComSucesso;
                if (retornoAoAlterarEmRepositorioAutor.Problemas.Count > 0)
                {
                    retorno.Mensagem = "Não foi possível alterar o autor.";
                    retorno.Problemas.AddRange(retornoAoAlterarEmRepositorioAutor.Problemas);
                }
            }
            else
            {
                retorno.Mensagem = "Os dados do autor não foram devidamente informados.";
                retorno.Problemas.AddRange(autor.Problemas);
            }

            return retorno;
        }

        public Retorno.AoRealizarListaAutores Realizar(Operacao.ListaAutores listaAutores)
        {
            var retorno = new Retorno.AoRealizarListaAutores();

            var retornoAoListarDeRepositorioAutor = repAutor.Listar();

            if (retornoAoListarDeRepositorioAutor.Problemas.Count > 0)
            {
                retorno.Mensagem = "Não foi possível listar os autores.";
                retorno.Problemas.AddRange(retornoAoListarDeRepositorioAutor.Problemas);
            }
            else
            {
                retorno.Autores = new List<AutorDTO>();
                foreach (var autor in retornoAoListarDeRepositorioAutor.Autores)
                {
                    retorno.Autores.Add(AutorDTO.Fabricar(autor));
                }
            }

            return retorno;
        }

        public Retorno.AoRealizarLocalizaAutor Realizar(Operacao.LocalizaAutor localizaAutor)
        {
            var retorno = new Retorno.AoRealizarLocalizaAutor();

            var retornoAoLocalizarEmRepositorioAutor = repAutor.Localizar(localizaAutor.AutorId);

            if (retornoAoLocalizarEmRepositorioAutor.Problemas.Count > 0)
            {
                retorno.Mensagem = "Não foi possível localizar o autor.";
                retorno.Problemas.AddRange(retornoAoLocalizarEmRepositorioAutor.Problemas);
            }
            else
            {
                retorno.Autor = AutorDTO.Fabricar(retornoAoLocalizarEmRepositorioAutor.Autor);
            }

            return retorno;
        }
    }
}
