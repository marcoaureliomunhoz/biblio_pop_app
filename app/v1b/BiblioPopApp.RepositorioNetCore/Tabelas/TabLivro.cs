using BiblioPopApp.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BiblioPopApp.RepositorioNetCore.Tabelas
{
    public class TabLivro
    {
        [Key]
        public int LivroId { get; set; } = 0;

        [MaxLength(Dominio.Entidades.Livro.TituloTamanhoMaximo)]
        public string Titulo { get; set; } = "";

        [MaxLength(Dominio.Entidades.Livro.EstanteTamanhoMaximo)]
        public string Estante { get; set; } = "";

        public int AnoPublicacao { get; set; } = 0;

        public int EditoraId { get; set; } = 0;

        [ForeignKey(nameof(EditoraId))]
        public virtual TabEditora Editora { get; set; }

        //tem que ser declarado como campo
        //não pode ser uma propriedade, se for propriedade o lerdo do EF infere que Autor também está ligado a Livro
        public List<TabAutor> Autores;

        //public virtual IList<TabLivroAutoria> LivroAutoria { get; set; }

        public TabLivro()
        {
            Autores = new List<TabAutor>();
        }

        public Livro Fabricar()
        {
            var autores = new List<Autor>();
            foreach (var autor in Autores)
            {
                autores.Add(autor.Fabricar());
            }
            return new Livro(LivroId, Titulo, Estante, AnoPublicacao, Editora.Fabricar(), autores);
        }

        public static TabLivro Fabricar(Livro livro)
        {
            var autores = new List<TabAutor>();
            foreach (var autor in livro.Autores)
            {
                autores.Add(TabAutor.Fabricar(autor));
            }
            return new TabLivro
            {
                LivroId = livro.LivroId,
                Titulo = livro.Titulo,
                Estante = livro.Estante,
                AnoPublicacao = livro.AnoPublicacao,
                EditoraId = livro.Editora.EditoraId,
                Editora = TabEditora.Fabricar(livro.Editora),
                Autores = autores
            };
        }
    }
}
