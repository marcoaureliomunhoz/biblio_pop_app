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

        public RetornoBase<int> Realizar(NovoAutor novoAutor)
        {
            var retorno = new RetornoBase<int>();

            var autor = new Autor(0, new TNomePessoa(novoAutor.Nome, novoAutor.Sobrenome), new TEmail(novoAutor.Email));

            if (autor.EstaEmEstadoIntegro())
            {
                var aoInserir = repAutor.Inserir(autor);
                retorno.Valor = aoInserir.Valor;
                if (aoInserir.Problemas.Count>0)
                {
                    retorno.Mensagem = "Não foi possível registrar o novo autor.";
                    retorno.Problemas.AddRange(aoInserir.Problemas);
                }
            }
            else
            {                
                retorno.Mensagem = "Os dados do novo autor não foram devidamente informados.";
                retorno.Problemas.AddRange(autor.Problemas);
            }
            
            return retorno;
        }

        public RetornoBase<bool> Realizar(AjusteAutor ajusteAutor)
        {
            var retorno = new RetornoBase<bool>();

            var autor = new Autor(ajusteAutor.AutorId, new TNomePessoa(ajusteAutor.Nome, ajusteAutor.Sobrenome), new TEmail(ajusteAutor.Email));

            if (autor.EstaEmEstadoIntegro())
            {
                var aoAlterar = repAutor.Alterar(autor);
                retorno.Valor = aoAlterar.Valor;
                if (aoAlterar.Problemas.Count > 0)
                {
                    retorno.Mensagem = "Não foi possível alterar o autor.";
                    retorno.Problemas.AddRange(aoAlterar.Problemas);
                }
            }
            else
            {
                retorno.Mensagem = "Os dados do autor não foram devidamente informados.";
                retorno.Problemas.AddRange(autor.Problemas);
            }

            return retorno;
        }

        public RetornoBase<List<TAutor>> Realizar(ListaAutores listaAutores)
        {
            var retorno = new RetornoBase<List<TAutor>>();

            var aoListar = repAutor.Listar();

            if (aoListar.Problemas.Count > 0)
            {
                retorno.Mensagem = "Não foi possível listar os autores.";
                retorno.Problemas.AddRange(aoListar.Problemas);
            }
            else
            {
                retorno.Valor = new List<TAutor>();
                foreach (var item in aoListar.Valor)
                {
                    retorno.Valor.Add(new TAutor
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
