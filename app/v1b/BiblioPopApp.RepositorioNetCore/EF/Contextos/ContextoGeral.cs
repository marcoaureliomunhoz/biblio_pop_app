using Microsoft.EntityFrameworkCore;

namespace BiblioPopApp.RepositorioNetCore.EF.Contextos
{
    public class ContextoGeral : DbContext
    {
        public ContextoGeral(DbContextOptions<ContextoGeral> options) : base(options)
        {
            //var builder = new DbContextOptionsBuilder<ContextoGeral>();
            //builder.UseSqlServer("Server=localhost;Database=BiblioPop;Trusted_Connection=True;");
            //builder.Options;
        }

        //public virtual DbSet<TabAutor> Autores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
