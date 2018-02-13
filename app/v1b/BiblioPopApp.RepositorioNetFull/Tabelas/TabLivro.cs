using BiblioPopApp.Dominio.Entidades;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace BiblioPopApp.RepositorioNetFull.Tabelas
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
        public virtual TabEditora Editora { get; set; }

        public virtual List<TabAutor> Autores { get; set; }

        public Livro Fabricar()
        {
            var autores = new List<Autor>();
            Autores.ForEach(x => autores.Add(x.Fabricar()));
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

    //relacionamentos
    public class LivroMap : EntityTypeConfiguration<TabLivro>
    {
        public LivroMap()
        {
            ToTable("Livro");

            HasRequired(p => p.Editora)
                .WithMany()
                .HasForeignKey(f => f.EditoraId);

            HasMany(p => p.Autores)
                .WithMany()
                .Map(x =>
                {
                    x.MapLeftKey("LivroId");
                    x.MapRightKey("AutorId");
                    x.ToTable("LivroAutoria");
                });
        }
    }
}
