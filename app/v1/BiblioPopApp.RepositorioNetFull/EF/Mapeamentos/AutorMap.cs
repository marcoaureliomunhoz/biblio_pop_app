using BiblioPopApp.RepositorioNetFull.Tabelas;
using System.Data.Entity.ModelConfiguration;

namespace BiblioPopApp.RepositorioNetFull.EF.Mapeamentos
{
    public class AutorMap : EntityTypeConfiguration<TabAutor>
    {
        public AutorMap()
        {
            ToTable("Autor");

            HasKey(x => x.AutorId);

            Property(p => p.Nome)
                .HasColumnType("varchar")
                .HasMaxLength(TabAutor.NomeTamanhoMaximo);

            Property(p => p.Sobrenome)
                .HasColumnType("varchar")
                .HasMaxLength(TabAutor.SobrenomeTamanhoMaximo);

            Property(p => p.Email)
                .HasColumnType("varchar")
                .HasMaxLength(TabAutor.SobrenomeTamanhoMaximo);
        }
    }
}
