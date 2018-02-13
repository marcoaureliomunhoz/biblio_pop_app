using BiblioPopApp.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiblioPopApp.Dominio.Entidades
{
    public class Editora : Entidade
    {
        public int EditoraId { get; }

        public const int NomeTamanhoMinimo = 3;
        public const int NomeTamanhoMaximo = 50;
        public string Nome { get; } = "";

        public const int SiteTamanhoMaximo = 500;
        public string Site { get; set; } = "";

        protected Editora() { }

        public Editora(int id, string nome, string site)
        {
            this.ValidarTamanhoMinimo(nome, NomeTamanhoMinimo, $"O nome da editora deve ter no mínimo {NomeTamanhoMinimo} caracteres.");
            this.ValidarTamanhoMaximo(nome, NomeTamanhoMaximo, $"O nome da editora deve ter no máximo {NomeTamanhoMaximo} caracteres.");
            this.ValidarTamanhoMaximo(site, SiteTamanhoMaximo, $"O site da editora deve ter no máximo {SiteTamanhoMaximo} caracteres.");

            if (this.EstaEmEstadoIntegro())
            {
                EditoraId = id;
                Nome = nome;
                Site = site;
            }
        }
    }
}
