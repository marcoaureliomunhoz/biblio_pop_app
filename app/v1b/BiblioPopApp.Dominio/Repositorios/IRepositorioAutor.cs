using BiblioPopApp.Dominio.Entidades;

namespace BiblioPopApp.Dominio.Repositorios
{
    public interface IRepositorioAutor
    {
        Retorno.AoInserirEmRepositorioAutor Inserir(Autor autor);
        Retorno.AoAlterarEmRepositorioAutor Alterar(Autor autor);
        Retorno.AoListarDeRepositorioAutor Listar();
        Retorno.AoLocalizarEmRepositorioAutor Localizar(int autorId);
    }
}
