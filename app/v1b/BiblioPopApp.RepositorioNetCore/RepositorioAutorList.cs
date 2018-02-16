using BiblioPopApp.Dominio.Entidades;
using BiblioPopApp.Dominio.Repositorios;
using BiblioPopApp.Dominio.Repositorios.Retorno;
using BiblioPopApp.RepositorioNetCore.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BiblioPopApp.RepositorioNetCore
{
    public class RepositorioAutorList : IRepositorioAutor
    {
        private List<TabAutor> dbAutores;

        public RepositorioAutorList()
        {
            this.dbAutores = new List<TabAutor>();
        }

        public RepositorioAutorList(List<TabAutor> dbAutores)
        {
            this.dbAutores = dbAutores;
        }

        public AoAlterarEmRepositorioAutor Alterar(Autor autor)
        {
            var retorno = new AoAlterarEmRepositorioAutor();

            try
            {
                var tabAutor = dbAutores.FirstOrDefault(x => x.AutorId == autor.AutorId);
                tabAutor.Nome = autor.Nome;
                tabAutor.Sobrenome = autor.Sobrenome;
                tabAutor.Email = autor.Email;
                retorno.AlterouComSucesso = true;
            }
            catch (Exception ex)
            {
                retorno.Mensagem = $"Não foi possível alterar o autor '{autor.AutorId}'.";
                retorno.Problemas.Add($"Falha ao {nameof(Alterar)} em {nameof(RepositorioAutor)}: {ex.Message}");
            }

            return retorno;
        }

        public AoInserirEmRepositorioAutor Inserir(Autor autor)
        {
            var retorno = new AoInserirEmRepositorioAutor();

            try
            {
                var tabAutor = TabAutor.Fabricar(autor);
                tabAutor.AutorId = dbAutores.Count > 0 ? dbAutores.Max(x => x.AutorId) + 1 : 1;
                dbAutores.Add(tabAutor);
                retorno.AutorId = tabAutor.AutorId;
            }
            catch (Exception ex)
            {
                retorno.Mensagem = $"Não foi possível inserir o autor '{autor.Nome} {autor.Sobrenome}'.";
                retorno.Problemas.Add($"Falha ao {nameof(Inserir)} em {nameof(RepositorioAutor)}: {ex.Message}");
            }

            return retorno;
        }

        public AoListarDeRepositorioAutor Listar()
        {
            var retorno = new AoListarDeRepositorioAutor();

            try
            {
                var autores = new List<Autor>();
                var tabAutores = dbAutores.ToList();
                foreach (var tabAutor in tabAutores)
                {
                    autores.Add(tabAutor.Fabricar());
                }

                retorno.Autores = autores;
            }
            catch (Exception ex)
            {
                retorno.Mensagem = "Não foi possível listar os autores.";
                retorno.Problemas.Add($"Falha ao {nameof(Listar)} em {nameof(RepositorioAutor)}: {ex.Message}");
            }

            return retorno;
        }

        public AoLocalizarEmRepositorioAutor Localizar(int autorId)
        {
            var retorno = new AoLocalizarEmRepositorioAutor();

            try
            {
                var tbAutor = dbAutores.FirstOrDefault(x => x.AutorId == autorId);
                if (tbAutor != null)
                {
                    retorno.Autor = tbAutor.Fabricar();
                }
                else
                {
                    retorno.Mensagem = $"Autor não localizado para ID {autorId}.";
                }
            }
            catch (Exception ex)
            {
                retorno.Mensagem = $"Não foi possível localizar o autor {autorId}.";
                retorno.Problemas.Add($"Falha ao {nameof(Localizar)} em {nameof(RepositorioAutor)}: {ex.Message}");
            }

            return retorno;
        }
    }
}
