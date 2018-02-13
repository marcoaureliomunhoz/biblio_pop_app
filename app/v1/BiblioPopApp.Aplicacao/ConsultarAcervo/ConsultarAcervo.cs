using BiblioPopApp.Aplicacao.ConsultarAcervo.Operacoes;
using BiblioPopApp.Aplicacao.Descritores;
using BiblioPopApp.Base;
using BiblioPopApp.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BiblioPopApp.Aplicacao.ConsultarAcervo
{
    public class ConsultarAcervo
    {
        private IRepositorioLivro repLivro;
        private IRepositorioEditora repEditora;
        private IRepositorioAutor repAutor;

        public ConsultarAcervo(IRepositorioLivro repositorioLivro, IRepositorioEditora repositorioEditora, IRepositorioAutor repositorioAutor)
        {
            repLivro = repositorioLivro;
            repEditora = repositorioEditora;
            repAutor = repositorioAutor;
        }

        public RetornoBase<List<TLivroAcervo>> Realizar(ListaAcervo listaAcervo)
        {
            var retorno = new RetornoBase<List<TLivroAcervo>>();

            var aoListarAcervo = repLivro.ListarAcervo(listaAcervo.Titulo, listaAcervo.EditoraId, listaAcervo.AutorId);

            if (aoListarAcervo.Problemas.Count > 0)
            {
                retorno.Mensagem = "Não foi possível listar o acervo.";
                retorno.Problemas.AddRange(aoListarAcervo.Problemas);
            }
            else
            {
                var livros = aoListarAcervo.Valor;
                retorno.Valor = new List<TLivroAcervo>();
                foreach (var item in livros)
                {
                    var tlivro = new TLivroAcervo()
                    {
                        LivroId = item.LivroId,
                        Titulo = item.Titulo,
                        Estante = item.Estante,
                        AnoPublicacao = item.AnoPublicacao,
                        EditoraId = item.Editora.EditoraId,
                        Editora = new TEditora() { EditoraId = item.Editora.EditoraId, Nome = item.Editora.Nome, Site = item.Editora.Site }
                    };
                    foreach (var autor in item.Autores)
                    {
                        tlivro.Autores.Add(new TAutor()
                        {
                            AutorId = autor.AutorId,
                            Nome = autor.Nome.Nome,
                            Sobrenome = autor.Nome.Sobrenome,
                            Email = autor.Email.Endereco
                        });
                    }
                    tlivro.Autoria = String.Join(", ", tlivro.Autores.Select(x => x.Nome + " " + x.Sobrenome).ToArray());
                    retorno.Valor.Add(tlivro);
                }
            }

            return retorno;
        }

        public RetornoBase<List<TEditora>> Realizar(ListaEditoras listaEditoras)
        {
            var retorno = new RetornoBase<List<TEditora>>();

            var aoListar = repEditora.Listar();

            if (aoListar.Problemas.Count > 0)
            {
                retorno.Mensagem = "Não foi possível listar as editoras.";
                retorno.Problemas.AddRange(aoListar.Problemas);
            }
            else
            {
                retorno.Valor = new List<TEditora>();
                foreach (var item in aoListar.Valor)
                {
                    retorno.Valor.Add(new TEditora
                    {
                        EditoraId = item.EditoraId,
                        Nome = item.Nome,
                        Site = item.Site
                    });
                }
            }

            return retorno;
        }

        public ProtocolosDeRetorno.RetornoDeRealizarListaAutoresEmConsultarAcervo Realizar(ListaAutores listaAutores)
        {
            var retorno = new ProtocolosDeRetorno.RetornoDeRealizarListaAutoresEmConsultarAcervo();

            var retornoDeListarDeRepositorioAutor = repAutor.Listar();

            if (retornoDeListarDeRepositorioAutor.Problemas.Count > 0)
            {
                retorno.Mensagem = "Não foi possível listar os autores.";
                retorno.Problemas.AddRange(retornoDeListarDeRepositorioAutor.Problemas);
            }
            else
            {
                retorno.Autores = new List<TAutor>();
                foreach (var item in retornoDeListarDeRepositorioAutor.Autores)
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
