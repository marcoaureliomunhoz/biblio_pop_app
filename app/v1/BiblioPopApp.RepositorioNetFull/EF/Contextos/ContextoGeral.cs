using BiblioPopApp.RepositorioNetFull.EF.Mapeamentos;
using BiblioPopApp.RepositorioNetFull.Tabelas;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioPopApp.RepositorioNetFull.EF.Contextos
{
    public class ContextoGeral : DbContext
    {
        public ContextoGeral(string stringConexao) : base(stringConexao)
        {
            Database.Log = Console.WriteLine;

            Database.SetInitializer<ContextoGeral>(null);

            Configuration.LazyLoadingEnabled = false;

            Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<TabAutor> Autores { get; set; }
        public virtual DbSet<TabEditora> Editoras { get; set; }
        public virtual DbSet<TabLivro> Livros { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties<string>().Configure(p => p.HasColumnType("varchar"));
            modelBuilder.Properties<string>().Configure(p => p.HasMaxLength(100));
            modelBuilder.Properties<string>().Configure(p => p.IsUnicode(false));

            modelBuilder.Configurations.Add<TabAutor>(new AutorMap());
            modelBuilder.Entity<TabEditora>().ToTable("Editora");
            modelBuilder.Configurations.Add<TabLivro>(new LivroMap());
        }
    }
}
