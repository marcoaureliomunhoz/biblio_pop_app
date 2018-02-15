using BiblioPopApp.RepositorioNetCore.Tabelas;
using Microsoft.EntityFrameworkCore;
using System;

namespace BiblioPopApp.RepositorioNetCore.EF.Contextos
{
    public class ContextoGeral : DbContext
    {
        public ContextoGeral(DbContextOptions<ContextoGeral> options) : base(options)
        {
            //var builder = new DbContextOptionsBuilder<ContextoGeral>();
            //builder.UseSqlServer("Server=localhost;Database=BiblioPop;Trusted_Connection=True;");
            //builder.Options;

            //Database.Log = msg => Console.WriteLine(msg);
        }

        public virtual DbSet<TabAutor> Autores { get; set; }
        public virtual DbSet<TabEditora> Editoras { get; set; }
        public virtual DbSet<TabLivro> Livros { get; set; }
        public virtual DbSet<TabLivroAutoria> LivroAutoria { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //configura o nome das tabelas
            modelBuilder.Entity<TabAutor>().ToTable("Autor");
            modelBuilder.Entity<TabEditora>().ToTable("Editora");
            modelBuilder.Entity<TabLivro>().ToTable("Livro");
            modelBuilder.Entity<TabLivroAutoria>().ToTable("LivroAutoria");

            //relacionamentos
            modelBuilder.ApplyConfiguration<TabLivroAutoria>(new TabLivroAutoriaMap());
        }
    }
}
