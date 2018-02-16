using BiblioPopApp.Dominio.Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiblioPopApp.Dominio.Teste.Entidades
{
    [TestClass]
    public class AutorTeste
    {
        string menorNomeValido = "".PadRight(Autor.NomeTamanhoMinimo, 'n');
        string maiorNomeValido = "".PadRight(Autor.NomeTamanhoMaximo, 'n');
        string menorSobrenomeValido = "".PadRight(Autor.SobrenomeTamanhoMinimo, 's');
        string maiorSobrenomeValido = "".PadRight(Autor.SobrenomeTamanhoMaximo, 's');

        string menorNomeInvalido = "".PadRight(Autor.NomeTamanhoMinimo-1, 'n');
        string maiorNomeInvalido = "".PadRight(Autor.NomeTamanhoMaximo+1, 'n');
        string menorSobrenomeInvalido = "".PadRight(Autor.SobrenomeTamanhoMinimo-1, 's');
        string maiorSobrenomeInvalido = "".PadRight(Autor.SobrenomeTamanhoMaximo+1, 's');

        string emailValido = "111@222";
        string maiorEmailInvalido = "111@222".PadLeft(Autor.EmailTamanhoMaximo+1, '0');

        [TestMethod]
        public void Entidade_Autor__SE__NomeVazio__ENTAO__ProblemaDeveSerSinalizado__E__EstadoDeIntegridadeDeveSerFalso()
        {
            //1-Preparação
            //Não é preciso, pois os atributos já foram preparados antes

            //2-Ação
            var autor = new Autor(0, "", menorNomeValido, emailValido);

            //3-Verificação
            Assert.IsTrue(autor.Problemas.Count > 0 && autor.EstaEmEstadoIntegro() == false);
        }

        [TestMethod]
        public void Entidade_Autor__SE__NomeComCaracteresAbaixoDoExigido__ENTAO__ProblemaDeveSerSinalizado__E__EstadoDeIntegridadeDeveSerFalso__E__NomeDeveSerVazio()
        {


            var autor = new Autor(0, menorNomeInvalido, menorSobrenomeValido, emailValido);

            Assert.IsTrue(autor.Problemas.Count > 0 && autor.EstaEmEstadoIntegro() == false && string.IsNullOrEmpty(autor.Nome));
        }

        [TestMethod]
        public void Entidade_Autor__SE__NomeComCaracteresAcimaDoPermitido__ENTAO__ProblemaDeveSerSinalizado__E__EstadoDeIntegridadeDeveSerFalso__E__NomeDeveSerVazio()
        {
            var autor = new Autor(0, maiorNomeInvalido, menorSobrenomeValido, emailValido);

            Assert.IsTrue(autor.Problemas.Count > 0 && autor.EstaEmEstadoIntegro() == false && string.IsNullOrEmpty(autor.Nome));
        }

        [TestMethod]
        public void Entidade_Autor__SE__Sobrenome_Vazio__ENTAO__ProblemaDeveSerSinalizado__E__EstadoDeIntegridadeDeveSerFalso()
        {
            var autor = new Autor(0, menorNomeValido, "", emailValido);

            Assert.IsTrue(autor.Problemas.Count > 0 && autor.EstaEmEstadoIntegro() == false);
        }

        [TestMethod]
        public void Entidade_Autor__SE__SobrenomeComCaracteresAbaixoDoExigido__ENTAO__ProblemaDeveSerSinalizado__E__EstadoDeIntegridadeDeveSerFalso__E__SobrenomeDeveSerVazio()
        {
            var autor = new Autor(0, menorNomeValido, menorSobrenomeInvalido, "email@valido.com");

            Assert.IsTrue(autor.Problemas.Count > 0 && autor.EstaEmEstadoIntegro() == false && string.IsNullOrEmpty(autor.Sobrenome));
        }

        [TestMethod]
        public void Entidade_Autor__SE__SobrenomeComCaracteresAcimaDoPermitido__ENTAO__ProblemaDeveSerSinalizado__E__EstadoDeIntegridadeDeveSerFalso__E__SobrenomeDeveSerVazio()
        {
            var autor = new Autor(0, menorNomeValido, maiorSobrenomeInvalido, emailValido);

            Assert.IsTrue(autor.Problemas.Count > 0 && autor.EstaEmEstadoIntegro() == false && string.IsNullOrEmpty(autor.Sobrenome));
        }

        [TestMethod]
        public void Entidade_Autor__SE__EmailVazio__ENTAO__EstadoDeIntegridadeDeveSerTrue()
        {
            var autor = new Autor(0, menorNomeValido, menorSobrenomeValido, "");

            Assert.IsTrue(autor.EstaEmEstadoIntegro() == true);
        }

        [TestMethod]
        public void Entidade_Autor__SE__EmailComCaracteresAcimaDoPermitido__ENTAO__ProblemaDeveSerSinalizado__E__EstadoDeIntegridadeDeveSerFalso__E__EmailDeveSerVazio()
        {
            var autor = new Autor(0, menorNomeValido, menorSobrenomeValido, maiorEmailInvalido);

            Assert.IsTrue(autor.Problemas.Count > 0 && autor.EstaEmEstadoIntegro() == false && string.IsNullOrEmpty(autor.Email));
        }

        [TestMethod]
        [DataRow("11")]
        [DataRow("111")]
        [DataRow("111@")]
        [DataRow("111@22")]
        [DataRow("111@22.")]
        [DataRow("11@22")]
        [DataRow("11@22.")]
        public void Entidade_Autor__SE__EmailInformadoForInvalido__ENTAO__ProblemaDeveSerSinalizado__E__EstadoDeIntegridadeDeveSerFalso__E__EmailDeveSerVazio(string value)
        {
            var autor = new Autor(0, menorNomeValido, menorSobrenomeValido, value);

            Assert.IsTrue(autor.Problemas.Count > 0 && autor.EstaEmEstadoIntegro() == false && string.IsNullOrEmpty(autor.Email));
        }

        [TestMethod]
        public void Entidade_Autor__SE__DadosValidos__ENTAO__ProblemaNaoDeveSerSinalizado__E__EstadoDeIntegridadeDeveSerTrue__E__PropriedadesDevemRefletirOsDadosPassados()
        {
            //1-Preparação
            //Não é preciso, pois os atributos já foram preparados antes

            //2-Ação
            var autor = new Autor(1, menorNomeValido, maiorSobrenomeValido, emailValido);

            //3-Verificação
            var resultado =
                    autor.Problemas.Count <= 0 &&
                    autor.EstaEmEstadoIntegro() == true &&
                    autor.Nome == menorNomeValido &&
                    autor.Sobrenome == maiorSobrenomeValido &&
                    autor.Email == emailValido &&
                    autor.AutorId == 1;
            Assert.IsTrue(resultado);
        }
    }
}
