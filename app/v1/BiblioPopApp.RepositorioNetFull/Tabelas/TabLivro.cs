using BiblioPopApp.Dominio.Entidades;
using System.Collections.Generic;

namespace BiblioPopApp.RepositorioNetFull.Tabelas
{
    public class TabLivro 
    {
        public int LivroId { get; set; } = 0;

        public const int TituloTamanhoMaximo = Livro.TituloTamanhoMaximo;
        public string Titulo { get; set; } = "";

        public const int EstanteTamanhoMaximo = Livro.EstanteTamanhoMaximo;
        public string Estante { get; set; } = "";

        public int AnoPublicacao { get; set; } = 0;

        public int EditoraId { get; set; } = 0;
        public virtual TabEditora Editora { get; set; }

        public virtual List<TabAutor> Autores { get; set; }
    }
}
