﻿using BiblioPopApp.Base;
using BiblioPopApp.Dominio.Descritores;
using BiblioPopApp.Dominio.Entidades;
using BiblioPopApp.Dominio.Repositorios;
using BiblioPopApp.RepositorioNetFull.EF.Contextos;
using BiblioPopApp.RepositorioNetFull.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BiblioPopApp.RepositorioNetFull
{
    public class RepositorioAutor : IRepositorioAutor
    {
        private ContextoGeral db;

        public RepositorioAutor(ContextoGeral contextoGeral)
        {
            db = contextoGeral;
        }

        internal static Autor TabAutorParaAutor(TabAutor tabAutor)
        {
            return new Autor(
                tabAutor.AutorId,
                new TNomePessoa(tabAutor.Nome, tabAutor.Sobrenome),
                new TEmail(tabAutor.Email)
            );
        }

        public RetornoBase<bool> Alterar(Autor autor)
        {
            var retorno = new RetornoBase<bool>();

            try
            {
                var tabAutor = db.Autores.FirstOrDefault(x => x.AutorId == autor.AutorId);
                tabAutor.Nome = autor.Nome.Nome;
                tabAutor.Sobrenome = autor.Nome.Sobrenome;
                tabAutor.Email = autor.Email.Endereco;
                retorno.Valor = db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                retorno.Mensagem = $"Não foi possível alterar o autor '{autor.AutorId}'.";
                retorno.Problemas.Add($"Falha ao {nameof(Alterar)} em {nameof(RepositorioAutor)}: {ex.Message}");
            }

            return retorno;
        }

        public RetornoBase<int> Inserir(Autor autor)
        {
            var retorno = new RetornoBase<int>();

            try
            {
                var tabAutor = new TabAutor();
                tabAutor.AutorId = autor.AutorId;
                tabAutor.Nome = autor.Nome.Nome;
                tabAutor.Sobrenome = autor.Nome.Sobrenome;
                tabAutor.Email = autor.Email.Endereco;
                db.Autores.Add(tabAutor);
                db.SaveChanges();
                retorno.Valor = tabAutor.AutorId;
            }
            catch (Exception ex)
            {
                retorno.Mensagem = $"Não foi possível inserir o autor '{autor.Nome.Nome} {autor.Nome.Sobrenome}'.";
                retorno.Problemas.Add($"Falha ao {nameof(Inserir)} em {nameof(RepositorioAutor)}: {ex.Message}");
            }

            return retorno;
        }

        public RetornoBase<ICollection<Autor>> Listar()
        {
            var retorno = new RetornoBase<ICollection<Autor>>();

            try
            {
                var autores = new List<Autor>();
                var tabAutores = db.Autores.ToList();
                foreach (var tabAutor in tabAutores)
                {
                    autores.Add(RepositorioAutor.TabAutorParaAutor(tabAutor));
                }

                retorno.Valor = autores;
            }
            catch (Exception ex)
            {
                retorno.Mensagem = "Não foi possível listar os autores.";
                retorno.Problemas.Add($"Falha ao {nameof(Listar)} em {nameof(RepositorioAutor)}: {ex.Message}");
            }

            return retorno;
        }

        public RetornoBase<Autor> Localizar(int autorId)
        {
            var retorno = new RetornoBase<Autor>();

            try
            {
                var tbAutor = db.Autores.FirstOrDefault(x => x.AutorId == autorId);
                if (tbAutor != null)
                {
                    retorno.Valor = RepositorioAutor.TabAutorParaAutor(tbAutor);
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
