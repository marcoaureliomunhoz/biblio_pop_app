using BiblioPopApp.Aplicacao.RegistrarEditora.Operacoes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BiblioPopApp.CasosDeUso.RegistrarEditora
{
    public partial class FormPrincipalRegistrarEditora : Form
    {
        public FormPrincipalRegistrarEditora()
        {
            InitializeComponent();
            gridEditoras.AutoGenerateColumns = false;
        }

        private void gridEditoras_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                var formCadastro = new FormCadastroRegistrarEditora(null);
                formCadastro.ShowDialog();
                CarregarEditoras();
            }
        }

        private void AlterarEditora()
        {
            if (gridEditoras.Rows.Count > 0 && gridEditoras.CurrentRow != null)
            {
                var teditora = gridEditoras.CurrentRow.DataBoundItem as Aplicacao.Descritores.TEditora;
                var formCadastro = new FormCadastroRegistrarEditora(teditora);
                formCadastro.ShowDialog();
                CarregarEditoras();
            }
        }

        private void CarregarEditoras()
        {
            var registrarEditora = ControleDependenciaNetFull.Resolve.InstanciaDe<Aplicacao.RegistrarEditora.RegistrarEditora>();

            var retorno = registrarEditora.Realizar(new ListaEditoras());

            BiblioPopAppUtil.ProcessarMensagensRetornoBase(retorno.Mensagem, retorno.Problemas, null, null, null);

            gridEditoras.DataSource = retorno.Valor;

            statusBar.Text = "Nenhuma editora encontrada";

            if (retorno.Valor != null)
            {
                var aux = retorno.Valor.Count > 1 ? retorno.Valor.Count.ToString() + " editoras encontradas" : "Uma editora encontrada";
                statusBar.Text = aux;
            }
        }

        private void gridEditoras_DoubleClick(object sender, EventArgs e)
        {
            AlterarEditora();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            CarregarEditoras();
        }
    }
}
