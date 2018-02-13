using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BiblioPopApp
{
    public class BiblioPopAppUtil
    {
        public static void ProcessarMensagensRetornoBase(
            string mensagem, List<string> problemas,
            Panel panelMensagem, Label lblMensagem, ListBox listBoxProblemas
        )
        {
            if (panelMensagem != null) panelMensagem.Visible = false;
            if (lblMensagem != null) lblMensagem.Text = "";
            if (listBoxProblemas != null) listBoxProblemas.Items.Clear();
            if (problemas.Count > 0)
            {
                if (mensagem.Length > 0)
                {
                    if (lblMensagem != null) lblMensagem.Text = mensagem;
                    else
                        MessageBox.Show(mensagem, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                foreach (var problema in problemas)
                {
                    if (listBoxProblemas != null)
                        listBoxProblemas.Items.Add(problema);
                    else
                        MessageBox.Show(problema, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (panelMensagem != null) panelMensagem.Visible = true;
            }
            else if (mensagem.Length > 0)
            {
                MessageBox.Show(mensagem, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
