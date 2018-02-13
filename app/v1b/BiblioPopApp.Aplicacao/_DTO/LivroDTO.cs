using BiblioPopApp.Dominio.Entidades;
using System.Collections.Generic;

namespace BiblioPopApp.Aplicacao._DTO
{
    public class LivroDTO
    {
        public int LivroId { get; set; } = 0;
        public string Titulo { get; set; } = "";
        public string Estante { get; set; } = "";
        public int AnoPublicacao { get; set; } = 0;
        public int EditoraId { get; set; } = 0;
        public EditoraDTO Editora { get; set; }
        public List<AutorDTO> Autores { get; set; }

        public LivroDTO()
        {
            Autores = new List<AutorDTO>();
        }

        internal Livro Fabricar()
        {
            var autores = new List<Autor>();
            Autores.ForEach(x => autores.Add(x.Fabricar()));
            return new Livro(LivroId, Titulo, Estante, AnoPublicacao, Editora.Fabricar(), autores);
        }

        internal static LivroDTO Fabricar(Livro livro)
        {
            var autores = new List<AutorDTO>();
            foreach (var autor in livro.Autores)
            {
                autores.Add(AutorDTO.Fabricar(autor));
            }
            return new LivroDTO
            {
                LivroId = livro.LivroId,
                Titulo = livro.Titulo,
                Estante = livro.Estante,
                AnoPublicacao = livro.AnoPublicacao,
                EditoraId = livro.Editora.EditoraId,
                Editora = EditoraDTO.Fabricar(livro.Editora),
                Autores = autores
            };
        }
    }
}
