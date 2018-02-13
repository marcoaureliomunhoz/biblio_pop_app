using BiblioPopApp.Aplicacao.Descritores;
using BiblioPopApp.Aplicacao.RegistrarLivro.Operacoes;
using BiblioPopApp.Base;
using BiblioPopApp.Dominio.Entidades;
using BiblioPopApp.Dominio.Repositorios;
using System.Collections.Generic;

namespace BiblioPopApp.Aplicacao.RegistrarLivro
{
    public class RegistrarLivro
    {
        private IRepositorioLivro repLivro;

        public RegistrarLivro(IRepositorioLivro repositorioLivro)
        {
            repLivro = repositorioLivro;
        }

        public RetornoBase<List<TAutor>> Realizar(ListaAutoresDisponiveis listaAutoresDisponiveis)
        {
            var retorno = new RetornoBase<List<TAutor>>();

            var aoListarAutoresDisponiveis = repLivro.ListarAutoresDisponiveis(listaAutoresDisponiveis.LivroId);

            if (aoListarAutoresDisponiveis.Problemas.Count > 0)
            {
                retorno.Mensagem = "Não foi possível listar os autores disponíveis.";
                retorno.Problemas.AddRange(aoListarAutoresDisponiveis.Problemas);
            }
            else
            {
                retorno.Valor = new List<TAutor>();
                foreach (var item in aoListarAutoresDisponiveis.Valor)
                {
                    retorno.Valor.Add(new TAutor
                    {
                        AutorId = item.AutorId,
                        Nome = item.Nome.Nome,
                        Sobrenome = item.Nome.Sobrenome
                    });
                }
            }

            return retorno;
        }

        public RetornoBase<int> Realizar(NovoLivro novoLivro)
        {
            var retorno = new RetornoBase<int>();

            var editora = new Editora(novoLivro.Editora.EditoraId, novoLivro.Editora.Nome, novoLivro.Editora.Site);
            var autores = new List<Autor>();
            foreach (var tautor in novoLivro.Autores)
            {
                autores.Add(new Autor(
                    tautor.AutorId,
                    new Dominio.Descritores.TNomePessoa(tautor.Nome, tautor.Sobrenome), 
                    new Dominio.Descritores.TEmail(tautor.Email)
                ));
            }

            var livro = new Livro(0, novoLivro.Titulo, novoLivro.Estante, novoLivro.AnoPublicacao, editora, autores);

            if (livro.EstaEmEstadoIntegro())
            {
                var aoInserir = repLivro.Inserir(livro);
                retorno.Valor = aoInserir.Valor;
                if (aoInserir.Problemas.Count > 0)
                {
                    retorno.Mensagem = "Não foi possível registrar o novo livro.";
                    retorno.Problemas.AddRange(aoInserir.Problemas);
                }
            }
            else
            {
                retorno.Mensagem = "Os dados do novo livro não foram devidamente informados.";
                retorno.Problemas.AddRange(livro.Problemas);
            }

            return retorno;
        }

        public RetornoBase<TLivro> Realizar(LocalizaLivro localizaLivro)
        {
            var retorno = new RetornoBase<TLivro>();

            var aoLocalizarLivro = repLivro.Localizar(localizaLivro.LivroId);

            if (aoLocalizarLivro.Problemas.Count > 0)
            {
                retorno.Mensagem = "Não foi possível localizar o livro.";
                retorno.Problemas.AddRange(aoLocalizarLivro.Problemas);
            }
            else
            {
                var livro = retorno.Valor;
                var tlivro = new TLivro();

                tlivro.LivroId = livro.LivroId;
                tlivro.Titulo = livro.Titulo;
                tlivro.Estante = livro.Estante;
                tlivro.AnoPublicacao = livro.AnoPublicacao;
                tlivro.EditoraId = livro.EditoraId;
                tlivro.Editora = new TEditora()
                {
                    EditoraId = livro.Editora.EditoraId,
                    Nome = livro.Editora.Nome,
                    Site = livro.Editora.Site
                };
                foreach(var autor in livro.Autores)
                {
                    tlivro.Autores.Add(new TAutor()
                    {
                        AutorId = autor.AutorId,
                        Nome = autor.Nome,
                        Sobrenome = autor.Sobrenome,
                        Email = autor.Email
                    });
                }

                retorno.Valor = tlivro;
            }

            return retorno;
        }

        public RetornoBase<bool> Realizar(AjusteLivro ajusteLivro)
        {
            var retorno = new RetornoBase<bool>();

            var editora = new Editora(ajusteLivro.Editora.EditoraId, ajusteLivro.Editora.Nome, ajusteLivro.Editora.Site);
            var autores = new List<Autor>();
            foreach (var tautor in ajusteLivro.Autores)
            {
                autores.Add(new Autor(
                    tautor.AutorId,
                    new Dominio.Descritores.TNomePessoa(tautor.Nome, tautor.Sobrenome),
                    new Dominio.Descritores.TEmail(tautor.Email)
                ));
            }

            var livro = new Livro(ajusteLivro.LivroId, ajusteLivro.Titulo, ajusteLivro.Estante, ajusteLivro.AnoPublicacao, editora, autores);

            if (livro.EstaEmEstadoIntegro())
            {
                var aoAlterar = repLivro.Alterar(livro);
                retorno.Valor = aoAlterar.Valor;
                if (aoAlterar.Problemas.Count > 0)
                {
                    retorno.Mensagem = "Não foi possível ajustar o livro.";
                    retorno.Problemas.AddRange(aoAlterar.Problemas);
                }
            }
            else
            {
                retorno.Mensagem = "Os dados do livro não foram devidamente informados.";
                retorno.Problemas.AddRange(livro.Problemas);
            }

            return retorno;
        }

    }
}
