using BiblioPopApp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BiblioPopApp.RepositorioNetCore.Tabelas
{
    public class TabEditora
    {
        [Key]
        public int EditoraId { get; set; } = 0;

        [MaxLength(Dominio.Entidades.Editora.NomeTamanhoMaximo)]
        public string Nome { get; set; } = "";

        [MaxLength(Dominio.Entidades.Editora.SiteTamanhoMaximo)]
        public string Site { get; set; } = "";

        public TabEditora()
        {
        }

        public TabEditora(int id)
        {
            EditoraId = id;
        }

        public Editora Fabricar()
        {
            return new Editora(EditoraId, Nome, Site);
        }

        public static TabEditora Fabricar(Editora editora)
        {
            return new TabEditora
            {
                EditoraId = editora.EditoraId,
                Nome = editora.Nome,
                Site = editora.Site
            };
        }
    }
}
