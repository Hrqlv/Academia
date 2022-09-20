using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjAcademia
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Academia BROTHERS;
        private void Form1_Load(object sender, EventArgs e)
        {
            CriarBancoDados();
            BROTHERS = new Academia();
            bs.DataSource = BROTHERS.Alunos;
            dgvAlunos.DataSource = bs;
            dgvAlunos.AutoResizeColumns();

        }

        private void CriarBancoDados()
        {
            ServidorSQL servidor = new ServidorSQL();
            if (servidor.CriaBancoDados() == true)
            {
                MessageBox.Show("BASE ACADEMIA CRIADA COM SUCESSO", "ACADEMIA", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void btnMatricular_Click(object sender, EventArgs e)
        {
            FormFichaMatricula ficha = new FormFichaMatricula();
            ficha.Registro = null;
            ficha.ShowDialog();
            if (ficha.Registro != null)
            {
                BROTHERS.Matricular(ficha.Registro);
                bs.MoveLast();
                bs.ResetBindings(false);
                dgvAlunos.AutoResizeColumns();

            }

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            FormFichaMatricula ficha = new FormFichaMatricula();
            ficha.Registro = (Aluno)bs.Current;
            ficha.ShowDialog();
            if (ficha.Registro != null)
            {
                BROTHERS.Editar(ficha.Registro);
                bs.MoveLast();
                bs.ResetBindings(false);
                dgvAlunos.AutoResizeColumns();

            }

        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (bs.Count == 0) return;

            Aluno reg = (Aluno)bs.Current;
            DialogResult op;
            op = MessageBox.Show("Deseja Excluir " + reg.Nome,
                "ALERTA", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (op == DialogResult.Yes)
            {
                BROTHERS.EXCLUIR(reg);
                bs.ResetBindings(false);
                dgvAlunos.AutoResizeColumns();

            }
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            FormPesquisa pesquisa = new FormPesquisa();
            pesquisa.ShowDialog();
            if (pesquisa.Registro != null)
            {
                var obj = bs.List.OfType<Aluno>().
                    ToList().Find(f => f.ID == pesquisa.Registro.ID);
                bs.Position = bs.IndexOf(obj);
                btnEditar_Click(sender, e);

            }
        }

        private void bs_CurrentChanged(object sender, EventArgs e)
        {
            if (bs.Count == 0) return;

            Aluno reg = (Aluno)bs.Current;
            string arquivo = Environment.CurrentDirectory + "\\" + reg.ID + ".png";

            if (File.Exists(arquivo))
            {
                Graphics g = pbFoto.CreateGraphics();
                g.DrawImage(Image.FromFile(arquivo), 0, 0, pbFoto.Width, pbFoto.Height);
            }
        }
    }
}