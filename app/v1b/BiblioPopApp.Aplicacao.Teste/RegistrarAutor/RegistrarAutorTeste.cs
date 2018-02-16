using BiblioPopApp.Aplicacao._DTO;
using BiblioPopApp.Aplicacao.RegistrarAutor.Operacao;
using BiblioPopApp.Dominio.Entidades;
using BiblioPopApp.Dominio.Repositorios;
using BiblioPopApp.Dominio.Repositorios.Retorno;
using BiblioPopApp.RepositorioNetCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiblioPopApp.Aplicacao.Teste.RegistrarAutor
{
    [TestClass]
    public class RegistrarAutorTeste
    {
        private readonly Mock<IRepositorioAutor> repositorioAutor;

        public RegistrarAutorTeste()
        {
            repositorioAutor = new Mock<IRepositorioAutor>();
        }

        [TestMethod]
        public void Aplicacao_RegistrarAutor__SE__AoRealizarNovoAutor__NovoAutorForTotalmenteInvalido__ENTAO__InserirRepositorioNuncaDeveSerChamado()
        {
            //1-Preparação
            var novoAutorTotalmenteInvalido = new NovoAutor();
            repositorioAutor.Setup(x => x.Inserir(It.IsAny<Autor>())); //aqui não importa o retorno ao Inserir, pois nem pode chamar Inserir
            var registrarAutor = new Aplicacao.RegistrarAutor.RegistrarAutor(repositorioAutor.Object);

            //2-Ação
            var retorno = registrarAutor.Realizar(novoAutorTotalmenteInvalido);

            //3-Verificação
            repositorioAutor.Verify(m => m.Inserir(It.IsAny<Autor>()), Times.Never);
        }

        [TestMethod]
        public void Aplicacao_RegistrarAutor__SE__AoRealizarNovoAutor__NovoAutorForValido__E__RetornoDeRepositorioForNull__ENTAO__NaoPodeGerarExcecao()
        {
            //1-Preparação
            var novoAutorValido = new NovoAutor()
            {
                Nome = "marco",
                Sobrenome = "munhoz"
            };
            AoInserirEmRepositorioAutor retornoAoInserir = null;
            //aqui eu estou forçando o retorno Null para verificar se o desenvolvedor tratou retorno Null
            repositorioAutor.Setup(x => x.Inserir(It.IsAny<Autor>())).Returns(retornoAoInserir); 
            var registrarAutor = new Aplicacao.RegistrarAutor.RegistrarAutor(repositorioAutor.Object);

            //2-Ação
            var retorno = registrarAutor.Realizar(novoAutorValido);

            //3-Verificação
            //não precisa fazer nada, pois não pode gerar exceção
        }

        [TestMethod]
        public void Aplicacao_RegistrarAutor__SE__AoRealizarAjusteAutor__AjusteAutorForInvalido__ENTAO__RegistroDoRepositorioNaoPodeSerAlterado()
        {
            var registrarAutor = new Aplicacao.RegistrarAutor.RegistrarAutor(new RepositorioAutorList());

            var novoAutor = new NovoAutor
            {
                Nome = "marco",
                Sobrenome = "aurelio"
            };
            var retornoNovoAutor = registrarAutor.Realizar(novoAutor);

            var ajusteAutor = new AjusteAutor
            {
                AutorId = retornoNovoAutor.AutorId,
                Nome = "marco",
                Sobrenome = ""
            };
            var retornoAjusteAutor = registrarAutor.Realizar(ajusteAutor);

            var retornoLocalizaAutor = registrarAutor.Realizar(new LocalizaAutor { AutorId = ajusteAutor.AutorId });

            //todos os dados devem ser iguais aos dados originais
            Assert.AreEqual(novoAutor.Nome, retornoLocalizaAutor.Autor.Nome);
            Assert.AreEqual(novoAutor.Sobrenome, retornoLocalizaAutor.Autor.Sobrenome);
            Assert.AreEqual(novoAutor.Email, retornoLocalizaAutor.Autor.Email);
        }

        [TestMethod]
        public void Aplicacao_RegistrarAutor__SE__AoRealizarNovoAutor__NovoAutorForValido__ENTAO__RegistroDeveSerEncontradoNoRepositorio()
        {
            var registrarAutor = new Aplicacao.RegistrarAutor.RegistrarAutor(new RepositorioAutorList());

            var novoAutor = new NovoAutor
            {
                Nome = "marco",
                Sobrenome = "aurelio", 
                Email = "marco@gmail.com"
            };
            var retornoNovoAutor = registrarAutor.Realizar(novoAutor);

            novoAutor.AutorId = retornoNovoAutor.AutorId;

            var retornoLocalizaAutor = registrarAutor.Realizar(new LocalizaAutor { AutorId = novoAutor.AutorId });

            //todos os dados devem ser iguais aos dados originais
            Assert.IsInstanceOfType(retornoLocalizaAutor.Autor, typeof(AutorDTO));
            Assert.AreEqual(novoAutor.Nome, retornoLocalizaAutor.Autor.Nome);
            Assert.AreEqual(novoAutor.Sobrenome, retornoLocalizaAutor.Autor.Sobrenome);
            Assert.AreEqual(novoAutor.Email, retornoLocalizaAutor.Autor.Email);
        }
    }
}
