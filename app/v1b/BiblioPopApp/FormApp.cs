using BiblioPopApp.Aplicacao.RegistrarEditora;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BiblioPopApp
{
    public partial class FormApp : Form
    {
        public FormApp()
        {
            InitializeComponent();
        }

        private void menuApp_Registrar_Autor_Click(object sender, EventArgs e)
        {
            var formPrincipal = new CasosDeUso.RegistrarAutor.FormPrincipalRegistrarAutor();
            formPrincipal.ShowDialog();
        }

        private void menuApp_Registrar_Editora_Click(object sender, EventArgs e)
        {
            var formPrincipal = new CasosDeUso.RegistrarEditora.FormPrincipalRegistrarEditora();
            formPrincipal.ShowDialog();
        }

        private void menuApp_Registrar_Livro_Click(object sender, EventArgs e)
        {
            var formCadastro = new CasosDeUso.RegistrarLivro.FormCadastroRegistrarLivro(null);
            formCadastro.ShowDialog();
        }

        private void menuApp_Consultar_Acervo_Click(object sender, EventArgs e)
        {
            var formPrincipal = new CasosDeUso.ConsultarAcervo.FormPrincipalConsultarAcervo();
            formPrincipal.ShowDialog();
        }
    }
}
