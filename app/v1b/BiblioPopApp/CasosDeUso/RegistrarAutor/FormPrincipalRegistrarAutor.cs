using BiblioPopApp.Aplicacao._DTO;
using BiblioPopApp.Aplicacao.RegistrarAutor.Operacao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BiblioPopApp.CasosDeUso.RegistrarAutor
{
    public partial class FormPrincipalRegistrarAutor : Form
    {
        public FormPrincipalRegistrarAutor()
        {
            InitializeComponent();
            gridAutores.AutoGenerateColumns = false;
        }

        private void gridAutores_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                var formCadastro = new FormCadastroRegistrarAutor(null);
                formCadastro.ShowDialog();
                CarregarAutores();
            }
        }

        private void AlterarAutor()
        {
            if (gridAutores.Rows.Count > 0 && gridAutores.CurrentRow != null)
            {
                var autor = gridAutores.CurrentRow.DataBoundItem as AutorDTO;
                var formCadastro = new FormCadastroRegistrarAutor(autor);
                formCadastro.ShowDialog();
                CarregarAutores();
            }
        }

        private void CarregarAutores()
        {
            var registrarAutor = ControleDependenciaNetFull.Resolve.InstanciaDe<Aplicacao.RegistrarAutor.RegistrarAutor>();

            var retornoAoRealizarListaAutores = registrarAutor.Realizar(new ListaAutores());

            BiblioPopAppUtil.ProcessarMensagensRetornoBase(retornoAoRealizarListaAutores.Mensagem, retornoAoRealizarListaAutores.Problemas, null, null, null);

            gridAutores.DataSource = retornoAoRealizarListaAutores.Autores;

            statusBar.Text = "Nenhum autor encontrado";

            if (retornoAoRealizarListaAutores.Autores != null)
            {
                var aux = retornoAoRealizarListaAutores.Autores.Count > 1 ? retornoAoRealizarListaAutores.Autores.Count.ToString() + " autores encontrados" : "Um autor encontrado";
                statusBar.Text = aux;
            }
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            CarregarAutores();
        }

        private void gridAutores_DoubleClick(object sender, EventArgs e)
        {
            AlterarAutor();
        }
    }
}
