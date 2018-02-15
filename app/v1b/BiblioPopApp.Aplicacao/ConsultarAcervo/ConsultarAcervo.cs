using BiblioPopApp.Aplicacao._DTO;
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

        public RetornoBase<List<LivroAcervoDTO>> Realizar(Operacao.ListaAcervo listaAcervo)
        {
            var retorno = new RetornoBase<List<LivroAcervoDTO>>();

            var aoListarAcervo = repLivro.ListarAcervo(listaAcervo.Titulo, listaAcervo.EditoraId, listaAcervo.AutorId);

            if (aoListarAcervo.Problemas.Count > 0)
            {
                retorno.Mensagem = "Não foi possível listar o acervo.";
                retorno.Problemas.AddRange(aoListarAcervo.Problemas);
            }
            else
            {
                var livros = new List<LivroAcervoDTO>();

                foreach (var livro in aoListarAcervo.Valor)
                {
                    var livroDto = LivroAcervoDTO.Fabricar(livro);
                    if (livroDto.Autores != null && livroDto.Autores.Count > 0)
                        livroDto.Autoria = String.Join(", ", livroDto.Autores.Select(x => x.Nome + " " + x.Sobrenome).ToArray());
                    livros.Add(livroDto);
                }

                retorno.Valor = livros;
            }

            return retorno;
        }

        public RetornoBase<List<EditoraDTO>> Realizar(Operacao.ListaEditoras listaEditoras)
        {
            var retorno = new RetornoBase<List<EditoraDTO>>();

            var aoListar = repEditora.Listar();

            if (aoListar.Problemas.Count > 0)
            {
                retorno.Mensagem = "Não foi possível listar as editoras.";
                retorno.Problemas.AddRange(aoListar.Problemas);
            }
            else
            {
                retorno.Valor = new List<EditoraDTO>();
                foreach (var editora in aoListar.Valor)
                {
                    retorno.Valor.Add(EditoraDTO.Fabricar(editora));
                }
            }

            return retorno;
        }

        public Retorno.ListaAutores Realizar(Operacao.ListaAutores listaAutores)
        {
            var retorno = new Retorno.ListaAutores();

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
    }
}
