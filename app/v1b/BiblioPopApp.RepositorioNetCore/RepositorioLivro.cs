using BiblioPopApp.Base;
using BiblioPopApp.Dominio.Entidades;
using BiblioPopApp.Dominio.Repositorios;
using BiblioPopApp.RepositorioNetCore.EF.Contextos;
using BiblioPopApp.RepositorioNetCore.Tabelas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BiblioPopApp.RepositorioNetCore
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
                    tabAutores = db.Autores.FromSql($"select * from Autor where AutorId not in (select AutorId from LivroAutoria where LivroId = {livroId})").ToList();
                else
                    tabAutores = db.Autores.ToList();

                if (tabAutores != null)
                {
                    foreach (var tabAutor in tabAutores)
                    {
                        autores.Add(tabAutor.Fabricar());
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
                db.Livros.Add(tabLivro);
                db.SaveChanges();

                if (tabLivro.LivroId > 0)
                {
                    foreach (var autor in livro.Autores)
                    {
                        db.LivroAutoria.Add(new TabLivroAutoria { LivroId = tabLivro.LivroId, AutorId = autor.AutorId });
                    }
                    db.SaveChanges();
                }

                retorno.Valor = tabLivro.LivroId;
            }
            catch (Exception ex)
            {
                retorno.Mensagem = $"Não foi possível inseriro livro '{livro.Titulo}'.";
                retorno.Problemas.Add($"Falha ao {nameof(Inserir)} em {nameof(RepositorioLivro)}: {ex.Message}");
            }

            return retorno;
        }

        private List<TabAutor> ListarAutores(int livroId)
        {
            return db.Autores.FromSql($"select * from Autor where AutorId in (select AutorId from LivroAutoria where LivroId = {livroId})").ToList();
        }

        public RetornoBase<Livro> Localizar(int livroId)
        {
            var retorno = new RetornoBase<Livro>();

            try
            {
                var tbLivro = db.Livros.Include(x => x.Editora).FirstOrDefault(x => x.LivroId == livroId);
                if (tbLivro != null)
                {
                    tbLivro.Autores.AddRange(this.ListarAutores(livroId));
                    retorno.Valor = tbLivro.Fabricar();
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
                var tabLivro = db.Livros.Include(x => x.Editora).FirstOrDefault(x => x.LivroId == livro.LivroId);
                tabLivro.Titulo = livro.Titulo;
                tabLivro.Estante = livro.Estante;
                tabLivro.AnoPublicacao = livro.AnoPublicacao;
                tabLivro.EditoraId = livro.Editora.EditoraId;
                tabLivro.Editora = db.Editoras.FirstOrDefault(x => x.EditoraId == livro.Editora.EditoraId);
                var autores = db.LivroAutoria.Where(x => x.LivroId == livro.LivroId).ToList();
                foreach (var autor in livro.Autores)
                {
                    if (autores.Count(x => x.AutorId == autor.AutorId) <= 0)
                    {
                        db.LivroAutoria.Add(new TabLivroAutoria { LivroId = tabLivro.LivroId, AutorId = autor.AutorId });
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
                //if (autorId > 0)
                //{
                    //expressoes.Add(x => x.LivroAutoria.Select(a => a.AutorId).Contains(autorId));
                //}

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
                        tbLivros = db.Livros
                                        .Include(x => x.Editora)
                                        //.Include(x => x.LivroAutoria).ThenInclude(x => x.Autor)
                                        .Where(exLivro)
                                        .ToList();
                    }
                    else
                    {
                        tbLivros = db.Livros
                                        .Include(x => x.Editora)
                                        //.Include(x => x.LivroAutoria).ThenInclude(x => x.Autor)
                                        .Where(expressoes[0])
                                        .ToList();
                    }
                }
                else
                {
                    tbLivros = db.Livros
                                    .Include(x => x.Editora)
                                    .ToList();
                }

                if (tbLivros != null)
                {
                    retorno.Valor = new List<Livro>();
                    foreach (var tbLivro in tbLivros)
                    {
                        var tbAutores = this.ListarAutores(tbLivro.LivroId);
                        bool incluir = true;
                        if (autorId>0)
                        {
                            incluir = tbAutores.Count(x => x.AutorId == autorId) > 0;
                        }
                        if (incluir)
                        {
                            tbLivro.Autores.AddRange(tbAutores);
                            retorno.Valor.Add(tbLivro.Fabricar());
                        }
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
