using BiblioPopApp.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BiblioPopApp.Dominio.Entidades
{
    public class Livro : Entidade
    {
        public int LivroId { get; }

        public const int TituloTamanhoMinimo = 3;
        public const int TituloTamanhoMaximo = 100;
        public string Titulo { get; } = "";

        public const int EstanteTamanhoMaximo = 50;
        public string Estante { get; } = "";

        public int AnoPublicacao { get; } = 0;

        private int _editoraId = 0;
        private Editora _editora;
        public Editora Editora { get { return _editora; } }

        private IList<Autor> _autores;
        public IReadOnlyCollection<Autor> Autores
        {
            get { return _autores.ToArray(); }
        }

        public Livro(int id, string titulo, string estante, int anoPublicacao, Editora editora, List<Autor> autores)
        {
            _autores = new List<Autor>();

            this.ValidarTamanhoMinimo(titulo, TituloTamanhoMinimo, $"O título do livro deve ter no mínimo {TituloTamanhoMinimo} caracteres.");
            this.ValidarTamanhoMaximo(titulo, TituloTamanhoMaximo, $"O título do livro deve ter no máximo {TituloTamanhoMaximo} caracteres.");
            this.ValidarTamanhoMaximo(titulo, EstanteTamanhoMaximo, $"A estante do livro deve ter no máximo {EstanteTamanhoMaximo} caracteres.");
            DefinirEditora(editora);
            this.ValidarInstancia(autores, $"Os autores não foram definidos.");
            if (autores != null)
            {
                this.ValidarValorMinimo(autores.Count, 1, $"Defina pelo menos um autor já cadastrado.");
                if (autores.Count > 0)
                {
                    autores.ForEach(x => IncluirAutor(x));
                }
            }

            if (this.EstaEmEstadoIntegro())
            {
                LivroId = id;
                Titulo = titulo;
                Estante = estante ?? "";
                AnoPublicacao = anoPublicacao;
            }
        }

        public void IncluirAutor(Autor autor)
        {
            this.ValidarInstancia(autor, $"Não tem como incluir o autor.");
            if (autor != null)
            {
                this.ImportarEstadoDeIntegridade(autor);
                if (autor.EstaEmEstadoIntegro())
                {
                    this.ValidarValorMinimo(autor.AutorId, 1, "Informe um autor já cadastrado.");
                    if (autor.AutorId > 0)
                    {
                        _autores.Add(autor);
                    }
                }
            }
        }

        public void DefinirEditora(Editora editora)
        {
            this.ValidarInstancia(editora, $"Não tem como definir a editora.");
            if (editora != null)
            {
                this.ImportarEstadoDeIntegridade(editora);
                if (editora.EstaEmEstadoIntegro())
                {
                    this.ValidarValorMinimo(editora.EditoraId, 1, $"Informe uma editora já cadastrada.");
                    if (editora.EditoraId>0)
                    {
                        _editora = editora;
                        _editoraId = editora.EditoraId;
                    }
                }
            }
        }
    }
}
