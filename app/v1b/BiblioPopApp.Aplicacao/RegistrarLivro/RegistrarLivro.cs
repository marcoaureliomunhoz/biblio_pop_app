using BiblioPopApp.Aplicacao._DTO;
using BiblioPopApp.Base;
using BiblioPopApp.Dominio.Entidades;
using BiblioPopApp.Dominio.Repositorios;
using System.Collections.Generic;

namespace BiblioPopApp.Aplicacao.RegistrarLivro
{
    public class RegistrarLivro
    {
        private IRepositorioLivro repLivro;

        public RegistrarLivro(IRepositorioLivro repositorioLivro)
        {
            repLivro = repositorioLivro;
        }

        public RetornoBase<List<AutorDTO>> Realizar(Operacao.ListaAutoresDisponiveis listaAutoresDisponiveis)
        {
            var retorno = new RetornoBase<List<AutorDTO>>();

            var aoListarAutoresDisponiveis = repLivro.ListarAutoresDisponiveis(listaAutoresDisponiveis.LivroId);

            if (aoListarAutoresDisponiveis.Problemas.Count > 0)
            {
                retorno.Mensagem = "Não foi possível listar os autores disponíveis.";
                retorno.Problemas.AddRange(aoListarAutoresDisponiveis.Problemas);
            }
            else
            {
                retorno.Valor = new List<AutorDTO>();
                foreach (var autor in aoListarAutoresDisponiveis.Valor)
                {
                    retorno.Valor.Add(AutorDTO.Fabricar(autor));
                }
            }

            return retorno;
        }

        public RetornoBase<int> Realizar(Operacao.NovoLivro novoLivro)
        {
            var retorno = new RetornoBase<int>();

            var livro = novoLivro.Fabricar();

            if (livro.EstaEmEstadoIntegro())
            {
                var aoInserir = repLivro.Inserir(livro);
                retorno.Valor = aoInserir.Valor;
                if (aoInserir.Problemas.Count > 0)
                {
                    retorno.Mensagem = "Não foi possível registrar o novo livro.";
                    retorno.Problemas.AddRange(aoInserir.Problemas);
                }
            }
            else
            {
                retorno.Mensagem = "Os dados do novo livro não foram devidamente informados.";
                retorno.Problemas.AddRange(livro.Problemas);
            }

            return retorno;
        }

        public RetornoBase<LivroDTO> Realizar(Operacao.LocalizaLivro localizaLivro)
        {
            var retorno = new RetornoBase<LivroDTO>();

            var aoLocalizarLivro = repLivro.Localizar(localizaLivro.LivroId);

            if (aoLocalizarLivro.Problemas.Count > 0)
            {
                retorno.Mensagem = "Não foi possível localizar o livro.";
                retorno.Problemas.AddRange(aoLocalizarLivro.Problemas);
            }
            else
            {
                retorno.Valor = LivroDTO.Fabricar(aoLocalizarLivro.Valor);
            }

            return retorno;
        }

        public RetornoBase<bool> Realizar(Operacao.AjusteLivro ajusteLivro)
        {
            var retorno = new RetornoBase<bool>();

            var livro = ajusteLivro.Fabricar();

            if (livro.EstaEmEstadoIntegro())
            {
                var aoAlterar = repLivro.Alterar(livro);
                retorno.Valor = aoAlterar.Valor;
                if (aoAlterar.Problemas.Count > 0)
                {
                    retorno.Mensagem = "Não foi possível ajustar o livro.";
                    retorno.Problemas.AddRange(aoAlterar.Problemas);
                }
            }
            else
            {
                retorno.Mensagem = "Os dados do livro não foram devidamente informados.";
                retorno.Problemas.AddRange(livro.Problemas);
            }

            return retorno;
        }

    }
}
