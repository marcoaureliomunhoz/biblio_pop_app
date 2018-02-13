using BiblioPopApp.Aplicacao.Descritores;
using BiblioPopApp.Aplicacao.RegistrarEditora.Operacoes;
using BiblioPopApp.Base;
using BiblioPopApp.Dominio.Entidades;
using BiblioPopApp.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiblioPopApp.Aplicacao.RegistrarEditora
{
    public class RegistrarEditora
    {
        private IRepositorioEditora repEditora;

        public RegistrarEditora(IRepositorioEditora repositorioEditora)
        {
            repEditora = repositorioEditora;
        }

        public RetornoBase<int> Realizar(NovaEditora novaEditora)
        {
            var retorno = new RetornoBase<int>();

            var editora = new Editora(0, novaEditora.Nome, novaEditora.Site);

            if (editora.EstaEmEstadoIntegro())
            {
                var aoInserir = repEditora.Inserir(editora);
                retorno.Valor = aoInserir.Valor;
                if (aoInserir.Problemas.Count > 0)
                {
                    retorno.Mensagem = "Não foi possível registrar a nova editora.";
                    retorno.Problemas.AddRange(aoInserir.Problemas);
                }
            }
            else
            {
                retorno.Mensagem = "Os dados da nova editora não foram devidamente informados.";
                retorno.Problemas.AddRange(editora.Problemas);
            }

            return retorno;
        }

        public RetornoBase<bool> Realizar(AjusteEditora ajusteEditora)
        {
            var retorno = new RetornoBase<bool>();

            var editora = new Editora(ajusteEditora.EditoraId, ajusteEditora.Nome, ajusteEditora.Site);

            if (editora.EstaEmEstadoIntegro())
            {
                var aoAlterar = repEditora.Alterar(editora);
                retorno.Valor = aoAlterar.Valor;
                if (aoAlterar.Problemas.Count > 0)
                {
                    retorno.Mensagem = "Não foi possível alterar a editora.";
                    retorno.Problemas.AddRange(aoAlterar.Problemas);
                }
            }
            else
            {
                retorno.Mensagem = "Os dados da editora não foram devidamente informados.";
                retorno.Problemas.AddRange(editora.Problemas);
            }

            return retorno;
        }

        public RetornoBase<List<TEditora>> Realizar(ListaEditoras listaEditoras)
        {
            var retorno = new RetornoBase<List<TEditora>>();

            var aoListar = repEditora.Listar();

            if (aoListar.Problemas.Count > 0)
            {
                retorno.Mensagem = "Não foi possível listar as editoras.";
                retorno.Problemas.AddRange(aoListar.Problemas);
            }
            else
            {
                retorno.Valor = new List<TEditora>();
                foreach (var item in aoListar.Valor)
                {
                    retorno.Valor.Add(new TEditora
                    {
                        EditoraId = item.EditoraId,
                        Nome = item.Nome,
                        Site = item.Site
                    });
                }
            }

            return retorno;
        }
    }
}
