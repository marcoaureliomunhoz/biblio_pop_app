using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BiblioPopApp.RepositorioNetCore.Tabelas
{
    public class TabLivroAutoria
    {
        public int LivroId { get; set; }
        //public virtual TabLivro Livro { get; set; }
        public int AutorId { get; set; }
        //public virtual TabAutor Autor { get; set; }
    }

    //relacionamentos
    public class TabLivroAutoriaMap : IEntityTypeConfiguration<TabLivroAutoria>
    {
        public void Configure(EntityTypeBuilder<TabLivroAutoria> builder)
        {
            builder.ToTable("LivroAutoria");

            builder.HasKey(x => new { x.LivroId, x.AutorId });

            /*builder
                .HasOne(la => la.Livro)
                .WithMany(l => l.LivroAutoria)
                .HasForeignKey(la => la.AutorId);

            builder
                .HasOne(la => la.Autor)
                .WithMany(a => a.LivroAutoria)
                .HasForeignKey(la => la.LivroId);*/
        }
    }
}
