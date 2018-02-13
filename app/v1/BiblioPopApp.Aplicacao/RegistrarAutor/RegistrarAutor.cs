using BiblioPopApp.Aplicacao.Descritores;
using BiblioPopApp.Aplicacao.RegistrarAutor.Operacoes;
using BiblioPopApp.Base;
using BiblioPopApp.Dominio.Descritores;
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

        public ProtocoloDeRetorno.AoRealizarNovoAutor Realizar(NovoAutor novoAutor)
        {
            var retorno = new ProtocoloDeRetorno.AoRealizarNovoAutor();

            var autor = new Autor(0, new TNomePessoa(novoAutor.Nome, novoAutor.Sobrenome), new TEmail(novoAutor.Email));

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

        public ProtocoloDeRetorno.AoRealizarAjusteAutor Realizar(AjusteAutor ajusteAutor)
        {
            var retorno = new ProtocoloDeRetorno.AoRealizarAjusteAutor();

            var autor = new Autor(ajusteAutor.AutorId, new TNomePessoa(ajusteAutor.Nome, ajusteAutor.Sobrenome), new TEmail(ajusteAutor.Email));

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

        public ProtocoloDeRetorno.AoRealizarListaAutores Realizar(ListaAutores listaAutores)
        {
            var retorno = new ProtocoloDeRetorno.AoRealizarListaAutores();

            var retornoAoListarDeRepositorioAutor = repAutor.Listar();

            if (retornoAoListarDeRepositorioAutor.Problemas.Count > 0)
            {
                retorno.Mensagem = "Não foi possível listar os autores.";
                retorno.Problemas.AddRange(retornoAoListarDeRepositorioAutor.Problemas);
            }
            else
            {
                retorno.Autores = new List<TAutor>();
                foreach (var item in retornoAoListarDeRepositorioAutor.Autores)
                {
                    retorno.Autores.Add(new TAutor
                    {
                        AutorId = item.AutorId,
                        Nome = item.Nome.Nome,
                        Sobrenome = item.Nome.Sobrenome,
                        Email = item.Email.Endereco
                    });
                }
            }

            return retorno;
        }
    }
}
