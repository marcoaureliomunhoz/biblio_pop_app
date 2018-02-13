using BiblioPopApp.RepositorioNetFull.Tabelas;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioPopApp.RepositorioNetFull.EF.Mapeamentos
{
    public class LivroMap : EntityTypeConfiguration<TabLivro>
    {
        public LivroMap()
        {
            ToTable("Livro");

            HasKey(x => x.LivroId);

            Property(p => p.Titulo)
                .HasColumnType("varchar")
                .HasMaxLength(TabLivro.TituloTamanhoMaximo);

            Property(p => p.Estante)
                .HasColumnType("varchar")
                .HasMaxLength(TabLivro.EstanteTamanhoMaximo);

            //modelBuilder.Entity<Endereco>().HasRequired(p => p.Cliente).WithMany().HasForeignKey(f => f.EnderecoClienteID);

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
