using BiblioPopApp.Aplicacao.Descritores;
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
    public partial class FormCadastroRegistrarEditora : Form
    {
        private int EditoraId = 0;

        public FormCadastroRegistrarEditora(TEditora teditora)
        {
            InitializeComponent();

            if (teditora != null)
            {
                EditoraId = teditora.EditoraId;
                txtNome.Text = teditora.Nome;
                txtSite.Text = teditora.Site;
                panelMenu.Visible = false;
                this.Height -= panelMenu.Height;
            }
        }

        private void DesabilitarTela()
        {
            this.Enabled = false;
        }

        private void HabilitarTela()
        {
            this.Enabled = true;
        }

        private void SalvarAjuste()
        {
            var registrarEditora = ControleDependenciaNetFull.Resolve.InstanciaDe<Aplicacao.RegistrarEditora.RegistrarEditora>();

            var ajusteEditora = new AjusteEditora();
            ajusteEditora.EditoraId = EditoraId;
            ajusteEditora.Nome = txtNome.Text;
            ajusteEditora.Site = txtSite.Text;

            var retorno = registrarEditora.Realizar(ajusteEditora);

            BiblioPopAppUtil.ProcessarMensagensRetornoBase(retorno.Mensagem, retorno.Problemas, panelMensagem, lblMensagem, listBoxProblemas);
        }

        private void Salvar()
        {
            var podeSalvar = txtNome.Text.Length > 0;

            if (EditoraId > 0)
            {
                if (podeSalvar)
                {
                    SalvarAjuste();
                }
            }
            else
            {
                btnSalvar.Enabled = podeSalvar;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            DesabilitarTela();

            var registrarEditora = ControleDependenciaNetFull.Resolve.InstanciaDe<Aplicacao.RegistrarEditora.RegistrarEditora>();

            var novaEditora = new NovaEditora();
            novaEditora.Nome = txtNome.Text;
            novaEditora.Site = txtSite.Text;

            var retorno = registrarEditora.Realizar(novaEditora);

            BiblioPopAppUtil.ProcessarMensagensRetornoBase(retorno.Mensagem, retorno.Problemas, panelMensagem, lblMensagem, listBoxProblemas);

            if (retorno.Valor > 0)
            {
                this.Close();
            }

            HabilitarTela();
        }

        private void txtNome_KeyUp(object sender, KeyEventArgs e)
        {
            Salvar();
        }

        private void txtSite_KeyUp(object sender, KeyEventArgs e)
        {
            Salvar();
        }
    }
}
