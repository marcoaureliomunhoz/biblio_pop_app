using BiblioPopApp.Aplicacao._DTO;
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

        public RetornoBase<int> Realizar(Operacao.NovaEditora novaEditora)
        {
            var retorno = new RetornoBase<int>();

            var editora = novaEditora.Fabricar();

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

        public RetornoBase<bool> Realizar(Operacao.AjusteEditora ajusteEditora)
        {
            var retorno = new RetornoBase<bool>();

            var editora = ajusteEditora.Fabricar();

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

        public RetornoBase<List<EditoraDTO>> Realizar(Operacao.ListaEditoras listaEditoras)
        {
            var retorno = new RetornoBase<List<EditoraDTO>>();

            var aoListar = repEditora.Listar();

            if (aoListar.Problemas.Count > 0)
            {
                retorno.Mensagem = "Não foi possível listar as editoras.";
                retorno.Problemas.AddRange(aoListar.Problemas);
            }
            else
            {
                retorno.Valor = new List<EditoraDTO>();
                foreach (var editora in aoListar.Valor)
                {
                    retorno.Valor.Add(EditoraDTO.Fabricar(editora));
                }
            }

            return retorno;
        }

        public RetornoBase<EditoraDTO> Realizar(Operacao.LocalizaEditora localizaEditora)
        {
            var retorno = new RetornoBase<EditoraDTO>();

            var aoLocalizar = repEditora.Localizar(localizaEditora.EditoraId);

            if (aoLocalizar.Problemas.Count > 0)
            {
                retorno.Mensagem = "Não foi possível localizar a editora.";
                retorno.Problemas.AddRange(aoLocalizar.Problemas);
            }
            else
            {
                retorno.Valor = EditoraDTO.Fabricar(aoLocalizar.Valor);
            }

            return retorno;
        }
    }
}
