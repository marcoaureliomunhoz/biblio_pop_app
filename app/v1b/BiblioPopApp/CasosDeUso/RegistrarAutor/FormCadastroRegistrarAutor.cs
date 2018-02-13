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
    public partial class FormCadastroRegistrarAutor : Form
    {
        private int AutorId = 0;

        public FormCadastroRegistrarAutor(AutorDTO autor)
        {
            InitializeComponent();

            if (autor != null)
            {
                AutorId = autor.AutorId;
                txtNome.Text = autor.Nome;
                txtSobrenome.Text = autor.Sobrenome;
                txtEmail.Text = autor.Email;
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
            var registrarAutor = ControleDependenciaNetFull.Resolve.InstanciaDe<Aplicacao.RegistrarAutor.RegistrarAutor>();

            var ajusteAutor = new AjusteAutor();
            ajusteAutor.AutorId = AutorId;
            ajusteAutor.Nome = txtNome.Text;
            ajusteAutor.Sobrenome = txtSobrenome.Text;
            ajusteAutor.Email = txtEmail.Text;

            var retorno = registrarAutor.Realizar(ajusteAutor);

            BiblioPopAppUtil.ProcessarMensagensRetornoBase(retorno.Mensagem, retorno.Problemas, panelMensagem, lblMensagem, listBoxProblemas);
        }

        private void Salvar()
        {
            var podeSalvar = txtNome.Text.Length > 0 && txtSobrenome.Text.Length > 0;

            if (AutorId > 0)
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

            var registrarAutor = ControleDependenciaNetFull.Resolve.InstanciaDe<Aplicacao.RegistrarAutor.RegistrarAutor>();

            var novoAutor = new NovoAutor();
            novoAutor.Nome = txtNome.Text;
            novoAutor.Sobrenome = txtSobrenome.Text;
            novoAutor.Email = txtEmail.Text;

            var retornoAoRealizarNovoAutor = registrarAutor.Realizar(novoAutor);

            BiblioPopAppUtil.ProcessarMensagensRetornoBase(retornoAoRealizarNovoAutor.Mensagem, retornoAoRealizarNovoAutor.Problemas, panelMensagem, lblMensagem, listBoxProblemas);

            if (retornoAoRealizarNovoAutor.AutorId > 0)
            {
                this.Close();
            }

            HabilitarTela();
        }

        private void txtNome_KeyUp(object sender, KeyEventArgs e)
        {
            Salvar();
        }

        private void txtSobrenome_KeyUp(object sender, KeyEventArgs e)
        {
            Salvar();
        }

        private void txtEmail_KeyUp(object sender, KeyEventArgs e)
        {
            Salvar();
        }
    }
}
