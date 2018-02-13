using BiblioPopApp.Base;
using BiblioPopApp.Dominio.Entidades;
using System.Collections.Generic;

namespace BiblioPopApp.Dominio.Repositorios
{
    public interface IRepositorioAutor
    {
        ProtocoloDeRetorno.AoInserirEmRepositorioAutor Inserir(Autor autor);
        ProtocoloDeRetorno.AoAlterarEmRepositorioAutor Alterar(Autor autor);
        ProtocoloDeRetorno.AoListarDeRepositorioAutor Listar();
        ProtocoloDeRetorno.AoLocalizarEmRepositorioAutor Localizar(int autorId);
    }
}
