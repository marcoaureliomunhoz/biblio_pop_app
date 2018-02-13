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

            var retorno = registrarAutor.Realizar(new ListaAutores());

            BiblioPopAppUtil.ProcessarMensagensRetornoBase(retorno.Mensagem, retorno.Problemas, null, null, null);

            gridAutores.DataSource = retorno.Valor;

            statusBar.Text = "Nenhum autor encontrado";

            if (retorno.Valor != null)
            {
                var aux = retorno.Valor.Count > 1 ? retorno.Valor.Count.ToString() + " autores encontrados" : "Um autor encontrado";
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
