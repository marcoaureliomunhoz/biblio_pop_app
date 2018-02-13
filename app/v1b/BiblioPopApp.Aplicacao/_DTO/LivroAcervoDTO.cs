using BiblioPopApp.Dominio.Entidades;
using System.Collections.Generic;

namespace BiblioPopApp.Aplicacao._DTO
{
    public class LivroAcervoDTO : LivroDTO
    {
        public string Autoria { get; set; } = "";

        internal new static LivroAcervoDTO Fabricar(Livro livro)
        {
            var autores = new List<AutorDTO>();
            foreach (var autor in livro.Autores)
            {
                autores.Add(AutorDTO.Fabricar(autor));
            }            
            return new LivroAcervoDTO
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
