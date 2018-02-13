using BiblioPopApp.Aplicacao.Descritores;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiblioPopApp.Aplicacao.RegistrarLivro.Operacoes
{
    public class NovoLivro
    {
        public string Titulo { get; set; } = "";
        public string Estante { get; set; } = "";
        public int AnoPublicacao { get; set; } = 0;
        public TEditora Editora { get; set; }
        public List<TAutor> Autores { get; set; }

        public NovoLivro()
        {
            Autores = new List<TAutor>();
        }
    }
}
