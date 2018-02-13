using BiblioPopApp.Dominio.Descritores;
using BiblioPopApp.RepositorioNetCore.Tabelas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiblioPopApp.RepositorioNetCore.EF.Mapeamentos
{
    public class AutorMap : IEntityTypeConfiguration<TabAutor>
    {
        public void Configure(EntityTypeBuilder<TabAutor> builder)
        {
            builder.ToTable("Autor");

            builder.HasKey(x => x.AutorId);

            builder.Property(p => p.Nome)
                        .HasColumnType("varchar")
                        .HasMaxLength(TNomePessoa.NomeTamanhoMaximo);

            builder.Property(p => p.Sobrenome)
                        .HasColumnType("varchar")
                        .HasMaxLength(TNomePessoa.SobrenomeTamanhoMaximo);

            builder.Property(p => p.Email)
                        .HasColumnType("varchar")
                        .HasMaxLength(TEmail.EnderecoTamanhoMaximo);
        }
    }
}
