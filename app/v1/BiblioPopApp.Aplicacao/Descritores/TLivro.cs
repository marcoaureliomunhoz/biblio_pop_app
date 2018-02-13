using System;
using System.Collections.Generic;
using System.Text;

namespace BiblioPopApp.Aplicacao.Descritores
{
    public class TLivro
    {
        public int LivroId { get; set; } = 0;
        public string Titulo { get; set; } = "";
        public string Estante { get; set; } = "";
        public int AnoPublicacao { get; set; } = 0;
        public int EditoraId { get; set; } = 0;
        public TEditora Editora { get; set; }
        public List<TAutor> Autores { get; set; }

        public TLivro()
        {
            Autores = new List<TAutor>();
        }
    }
}
