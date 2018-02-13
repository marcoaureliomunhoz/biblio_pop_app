using BiblioPopApp.Base;
using BiblioPopApp.Dominio.Entidades;
using System;
using System.Collections.Generic;

namespace BiblioPopApp.Dominio.Repositorios
{
    public interface IRepositorioLivro
    {
        RetornoBase<ICollection<Autor>> ListarAutoresDisponiveis(int livroId);
        RetornoBase<int> Inserir(Livro livro);
        RetornoBase<Livro> Localizar(int livroId);
        RetornoBase<bool> Alterar(Livro livro);
        RetornoBase<ICollection<Livro>> ListarAcervo(string titulo, int editoraId, int autorId);
    }
}
