﻿using BiblioPopApp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BiblioPopApp.RepositorioNetCore.Tabelas
{
    public class TabAutor
    {
        [Key]
        public int AutorId { get; set; } = 0;

        [MaxLength(Dominio.Entidades.Autor.NomeTamanhoMaximo)]
        public string Nome { get; set; } = "";

        [MaxLength(Dominio.Entidades.Autor.SobrenomeTamanhoMaximo)]
        public string Sobrenome { get; set; } = "";

        [MaxLength(Dominio.Entidades.Autor.EmailTamanhoMaximo)]
        public string Email { get; set; } = "";

        //public IList<TabLivroAutoria> LivroAutoria { get; set; }

        public TabAutor()
        {
        }

        public TabAutor(int id)
        {
            AutorId = id;
        }

        public Autor Fabricar()
        {
            return new Autor(AutorId, Nome, Sobrenome, Email);
        }

        public static TabAutor Fabricar(Autor autor)
        {
            return new TabAutor
            {
                AutorId = autor.AutorId,
                Nome = autor.Nome,
                Sobrenome = autor.Sobrenome,
                Email = autor.Email
            };
        }
    }
}
