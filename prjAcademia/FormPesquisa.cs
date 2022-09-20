using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjAcademia
{
    public partial class FormPesquisa : Form
    {
        public FormPesquisa()
        {
            InitializeComponent();
        }

        private void btnProcurar_Click(object sender, EventArgs e)
        {
            if (rbInicio.Checked) Pesquisar("I");
            else if (rbMeio.Checked) Pesquisar("M");
            else Pesquisar("F");
        }

        private void Pesquisar(string p)
        {
            AlunoDB tabela = new AlunoDB();
            tabela.Filtrar(dgvLista, p, txtNome.Text);
            if (dgvLista.Rows.Count != 0)
            {
                dgvLista.AutoResizeColumns();
                dgvLista.Rows[0].Selected = true;

            }
        }

        private void btnAbrirFicha_Click(object sender, EventArgs e)
        {
            if (dgvLista.Rows.Count != 0)
            {
                if (dgvLista.CurrentRow != null)
                {
                    AlunoDB tabela = new AlunoDB();
                    Registro = tabela.SelecionarRegistro(
                        dgvLista.CurrentRow.Cells["ID"].Value);
                    this.Dispose();

                }
            }
        }
        internal Aluno Registro { get; set; }

        private void FormPesquisa_Load(object sender, EventArgs e)
        {

        }
    }
}
