using BiblioPopApp.Base;
using BiblioPopApp.Dominio.Entidades;
using System.Collections.Generic;

namespace BiblioPopApp.Dominio.Repositorios
{
    public interface IRepositorioAutor
    {
        RetornoBase<int> Inserir(Autor autor);
        RetornoBase<bool> Alterar(Autor autor);
        RetornoBase<ICollection<Autor>> Listar();
        RetornoBase<Autor> Localizar(int autorId);
    }
}
