using BiblioPopApp.Base;
using BiblioPopApp.Dominio.Entidades;
using BiblioPopApp.Dominio.Repositorios;
using BiblioPopApp.RepositorioNetFull.EF.Contextos;
using BiblioPopApp.RepositorioNetFull.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BiblioPopApp.RepositorioNetFull
{
    public class RepositorioLivro : IRepositorioLivro
    {
        private ContextoGeral db;

        public RepositorioLivro(ContextoGeral contextoGeral)
        {
            db = contextoGeral;
        }

        public RetornoBase<ICollection<Autor>> ListarAutoresDisponiveis(int livroId)
        {
            var retorno = new RetornoBase<ICollection<Autor>>();

            try
            {
                var autores = new List<Autor>();
                List<TabAutor> tabAutores = null;

                if (livroId > 0)
                    tabAutores = db.Database.SqlQuery<TabAutor>($"select * from Autor where AutorId not in (select AutorId from LivroAutoria where LivroId = {livroId})").ToList();
                else
                    tabAutores = db.Autores.ToList();

                if (tabAutores != null)
                {
                    foreach (var tabAutor in tabAutores)
                    {
                        autores.Add(RepositorioAutor.TabAutorParaAutor(tabAutor));
                    }
                }

                retorno.Valor = autores;
            }
            catch (Exception ex)
            {
                retorno.Mensagem = "Não foi possível listar os autores.";
                retorno.Problemas.Add($"Falha ao {nameof(ListarAutoresDisponiveis)} em {nameof(RepositorioLivro)}: {ex.Message}");
            }

            return retorno;
        }

        public RetornoBase<int> Inserir(Livro livro)
        {
            var retorno = new RetornoBase<int>();

            try
            {
                var tabLivro = new TabLivro();
                tabLivro.LivroId = livro.LivroId;
                tabLivro.Titulo = livro.Titulo;
                tabLivro.Estante = livro.Estante;
                tabLivro.AnoPublicacao = livro.AnoPublicacao;
                tabLivro.Editora = db.Editoras.FirstOrDefault(x => x.EditoraId == livro.Editora.EditoraId);
                tabLivro.Autores = new List<TabAutor>();
                foreach (var autor in livro.Autores)
                {
                    tabLivro.Autores.Add(db.Autores.FirstOrDefault(x => x.AutorId == autor.AutorId));
                }
                db.Livros.Add(tabLivro);
                db.SaveChanges();
                retorno.Valor = tabLivro.LivroId;
            }
            catch (Exception ex)
            {
                retorno.Mensagem = $"Não foi possível inseriro livro '{livro.Titulo}'.";
                retorno.Problemas.Add($"Falha ao {nameof(Inserir)} em {nameof(RepositorioLivro)}: {ex.Message}");
            }

            return retorno;
        }

        internal static Livro TabLivroParaLivro(TabLivro tabLivro)
        {
            var editora = RepositorioEditora.TabEditoraParaEditora(tabLivro.Editora);
            var autores = new List<Autor>();
            foreach (var tabAutor in tabLivro.Autores)
            {
                autores.Add(RepositorioAutor.TabAutorParaAutor(tabAutor));
            }
            return new Livro(
                tabLivro.LivroId,
                tabLivro.Titulo,
                tabLivro.Estante,
                tabLivro.AnoPublicacao,
                editora,
                autores
            );
        }

        public RetornoBase<Livro> Localizar(int livroId)
        {
            var retorno = new RetornoBase<Livro>();

            try
            {
                var tbLivro = db.Livros.Include("Editora").Include("Autores").FirstOrDefault(x => x.LivroId == livroId);
                if (tbLivro != null)
                {
                    retorno.Valor = RepositorioLivro.TabLivroParaLivro(tbLivro);
                }
                else
                {
                    retorno.Mensagem = $"Livro não localizado para ID {livroId}.";
                }
            }
            catch (Exception ex)
            {
                retorno.Mensagem = $"Não foi possível localizar o livro {livroId}.";
                retorno.Problemas.Add($"Falha ao {nameof(Localizar)} em {nameof(RepositorioLivro)}: {ex.Message}");
            }

            return retorno;
        }

        public RetornoBase<bool> Alterar(Livro livro)
        {
            var retorno = new RetornoBase<bool>();

            try
            {
                var tabLivro = db.Livros.Include("Editora").Include("Autores").FirstOrDefault(x => x.LivroId == livro.LivroId);
                tabLivro.Titulo = livro.Titulo;
                tabLivro.Estante = livro.Estante;
                tabLivro.AnoPublicacao = livro.AnoPublicacao;
                tabLivro.EditoraId = livro.Editora.EditoraId;
                tabLivro.Editora = db.Editoras.FirstOrDefault(x => x.EditoraId == livro.Editora.EditoraId);
                foreach (var autor in livro.Autores)
                {
                    if (tabLivro.Autores.Count(x => x.AutorId == autor.AutorId) <= 0)
                    {
                        tabLivro.Autores.Add(db.Autores.FirstOrDefault(x => x.AutorId == autor.AutorId));
                    }
                }
                retorno.Valor = db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                retorno.Mensagem = $"Não foi possível alterar o livro '{livro.LivroId}'.";
                retorno.Problemas.Add($"Falha ao {nameof(Alterar)} em {nameof(RepositorioLivro)}: {ex.Message}");
            }

            return retorno;
        }

        public RetornoBase<ICollection<Livro>> ListarAcervo(string titulo, int editoraId, int autorId)
        {
            var retorno = new RetornoBase<ICollection<Livro>>();

            try
            {
                List<Expression<Func<TabLivro, bool>>> expressoes = new List<Expression<Func<TabLivro, bool>>>();
                if (!string.IsNullOrEmpty(titulo))
                {
                    expressoes.Add(x => x.Titulo.Contains(titulo));
                }
                if (editoraId > 0)
                {
                    expressoes.Add(x => x.EditoraId == editoraId);
                }
                if (autorId > 0)
                {
                    expressoes.Add(x => x.Autores.Select(a => a.AutorId).Contains(autorId));
                }

                List<TabLivro> tbLivros = null;

                if (expressoes.Count > 0)
                {
                    if (expressoes.Count > 1)
                    {
                        var exLivro = expressoes[0].And(expressoes[1]);
                        for (var i = 2; i < expressoes.Count; i++)
                        {
                            exLivro = exLivro.And(expressoes[i]);
                        }
                        tbLivros = db.Livros.Include("Editora").Include("Autores").Where(exLivro).ToList();
                    }
                    else
                    {
                        tbLivros = db.Livros.Include("Editora").Include("Autores").Where(expressoes[0]).ToList();
                    }
                }
                else
                {
                    tbLivros = db.Livros.Include("Editora").Include("Autores").ToList();
                }

                if (tbLivros != null)
                {
                    retorno.Valor = new List<Livro>();
                    foreach (var tbLivro in tbLivros)
                    {
                        retorno.Valor.Add(RepositorioLivro.TabLivroParaLivro(tbLivro));
                    }
                }
                else
                {
                    retorno.Mensagem = $"Nenhum livro no acervo para os dados de entrada.";
                }
            }
            catch (Exception ex)
            {
                retorno.Mensagem = $"Não foi possível listar o acervo.";
                retorno.Problemas.Add($"Falha ao {nameof(ListarAcervo)} em {nameof(RepositorioLivro)}: {ex.Message}");
            }

            return retorno;
        }        
    }
}
