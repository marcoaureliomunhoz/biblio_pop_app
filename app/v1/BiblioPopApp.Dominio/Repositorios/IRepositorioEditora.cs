using BiblioPopApp.Base;
using BiblioPopApp.Dominio.Entidades;
using System.Collections.Generic;

namespace BiblioPopApp.Dominio.Repositorios
{
    public interface IRepositorioEditora
    {
        RetornoBase<int> Inserir(Editora autor);
        RetornoBase<bool> Alterar(Editora autor);
        RetornoBase<ICollection<Editora>> Listar();
        RetornoBase<Editora> Localizar(int autorId);
    }
}
