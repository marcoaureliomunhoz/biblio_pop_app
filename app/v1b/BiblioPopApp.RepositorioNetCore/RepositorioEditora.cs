using BiblioPopApp.Base;
using BiblioPopApp.Dominio.Entidades;
using BiblioPopApp.Dominio.Repositorios;
using BiblioPopApp.RepositorioNetCore.EF.Contextos;
using BiblioPopApp.RepositorioNetCore.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BiblioPopApp.RepositorioNetCore
{
    public class RepositorioEditora : IRepositorioEditora
    {
        private ContextoGeral db;

        public RepositorioEditora(ContextoGeral contextoGeral)
        {
            db = contextoGeral;
        }

        public RetornoBase<bool> Alterar(Editora editora)
        {
            var retorno = new RetornoBase<bool>();

            try
            {
                var tabEditora = db.Editoras.FirstOrDefault(x => x.EditoraId == editora.EditoraId);
                tabEditora.Nome = editora.Nome;
                tabEditora.Site = editora.Site;
                retorno.Valor = db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                retorno.Mensagem = $"Não foi possível alterar a editora '{editora.EditoraId}'.";
                retorno.Problemas.Add($"Falha ao {nameof(Alterar)} em {nameof(RepositorioEditora)}: {ex.Message}");
            }

            return retorno;
        }

        public RetornoBase<int> Inserir(Editora editora)
        {
            var retorno = new RetornoBase<int>();

            try
            {
                var tabEditora = TabEditora.Fabricar(editora);
                db.Editoras.Add(tabEditora);
                db.SaveChanges();
                retorno.Valor = tabEditora.EditoraId;
            }
            catch (Exception ex)
            {
                retorno.Mensagem = $"Não foi possível inserir a editora '{editora.Nome}'.";
                retorno.Problemas.Add($"Falha ao {nameof(Inserir)} em {nameof(RepositorioEditora)}: {ex.Message}");
            }

            return retorno;
        }

        public RetornoBase<ICollection<Editora>> Listar()
        {
            var retorno = new RetornoBase<ICollection<Editora>>();

            try
            {
                var editoras = new List<Editora>();
                var tabEditoras = db.Editoras.ToList();
                foreach (var tabEditora in tabEditoras)
                {
                    editoras.Add(tabEditora.Fabricar());
                }

                retorno.Valor = editoras;
            }
            catch (Exception ex)
            {
                retorno.Mensagem = "Não foi possível listar as editoras.";
                retorno.Problemas.Add($"Falha ao {nameof(Listar)} em {nameof(RepositorioEditora)}: {ex.Message}");
            }

            return retorno;
        }

        public RetornoBase<Editora> Localizar(int editoraId)
        {
            var retorno = new RetornoBase<Editora>();

            try
            {
                var tbEditora = db.Editoras.FirstOrDefault(x => x.EditoraId == editoraId);
                if (tbEditora != null)
                {
                    retorno.Valor = tbEditora.Fabricar();
                }
                else
                {
                    retorno.Mensagem = $"Editora não localizada para ID {editoraId}.";
                }
            }
            catch (Exception ex)
            {
                retorno.Mensagem = $"Não foi possível localizar a editora {editoraId}.";
                retorno.Problemas.Add($"Falha ao {nameof(Localizar)} em {nameof(RepositorioEditora)}: {ex.Message}");
            }

            return retorno;
        }
    }
}
