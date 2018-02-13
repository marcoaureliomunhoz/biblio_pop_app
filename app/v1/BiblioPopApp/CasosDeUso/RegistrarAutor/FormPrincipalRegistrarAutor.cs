using BiblioPopApp.Aplicacao.RegistrarAutor.Operacoes;
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
                var tautor = gridAutores.CurrentRow.DataBoundItem as Aplicacao.Descritores.TAutor;
                var formCadastro = new FormCadastroRegistrarAutor(tautor);
                formCadastro.ShowDialog();
                CarregarAutores();
            }
        }

        private void CarregarAutores()
        {
            var registrarAutor = ControleDependenciaNetFull.Resolve.InstanciaDe<Aplicacao.RegistrarAutor.RegistrarAutor>();

            var retornoDeRealizarListaAutores = registrarAutor.Realizar(new ListaAutores());

            BiblioPopAppUtil.ProcessarMensagensRetornoBase(retornoDeRealizarListaAutores.Mensagem, retornoDeRealizarListaAutores.Problemas, null, null, null);

            gridAutores.DataSource = retornoDeRealizarListaAutores.Autores;

            statusBar.Text = "Nenhum autor encontrado";

            if (retornoDeRealizarListaAutores.Autores != null)
            {
                var aux = retornoDeRealizarListaAutores.Autores.Count > 1 ? retornoDeRealizarListaAutores.Autores.Count.ToString() + " autores encontrados" : "Um autor encontrado";
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
