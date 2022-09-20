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
    public partial class FormFichaMatricula : Form
    {
        internal Aluno Registro { get; set; }

        public FormFichaMatricula()
        {
            InitializeComponent();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            if (Validacao() == false) return;

            if (Registro == null) novo();
            else editar();
            this.Dispose();
        }

        private bool Validacao()
        {
            if (txtID.Text.Equals(""))
            {
                ep.SetIconPadding(txtID, -30);
                ep.SetError(txtID, "ID está vazio");
                return false;
            }
            else ep.SetError(txtID, "");

            if (txtNome.Text.Equals(""))
            {
                ep.SetIconPadding(txtNome, -30);
                ep.SetError(txtNome, "Nome está vazio");
                return false;
                }
            else ep.SetError(txtNome, "");

            if (txtIdade.Text.Equals(""))
            {
                ep.SetIconPadding(txtIdade, -30);
                ep.SetError(txtIdade, "idade está vazio");
                return false;
            }
            else ep.SetError(txtIdade, "");

            if (txtPeso.Text.Equals(""))
            {
                ep.SetIconPadding(txtPeso, -30);
                ep.SetError(txtPeso, "Peso está vazio");
                return false;
            }
            else ep.SetError(txtPeso, "");

            if (txtAltura.Text.Equals(""))
            {
                ep.SetIconPadding(txtAltura, -30);
                ep.SetError(txtAltura, "Altura está vazio");
                return false;
            }
            else ep.SetError(txtAltura, "");

            return true;
            }


        private void editar()
        {
             Registro.Nome = txtNome.Text.ToUpper();
             Registro.Idade = Int16.Parse(txtIdade.Text);
             Registro.Peso = Double.Parse(txtPeso.Text);
             Registro.Altura = Double.Parse(txtAltura.Text);
             MessageBox.Show("EDIÇÃO CONCLUÍDA COM SUCESSO",
                "ALERTA", MessageBoxButtons.OK,
                MessageBoxIcon.Information);

        }

        private void novo()
        {
            Registro = new Aluno(
                Int32.Parse(txtID.Text),
                txtNome.Text.ToUpper(),
                Int16.Parse(txtIdade.Text),
                Double.Parse(txtPeso.Text),
                Double.Parse(txtAltura.Text)
                );
            MessageBox.Show("ALUNO MATRICULADO COM SUCESSO",
                "ALERTA", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            }

        private void FormFichaMatricula_Load(object sender, EventArgs e)
        {
            if(Registro != null)
            {
                txtID.Text = Registro.ID.ToString();
                txtNome.Text = Registro.Nome;
                txtIdade.Text = Registro.Idade.ToString();
                txtPeso.Text = Registro.Peso.ToString();
                txtAltura.Text = Registro.Altura.ToString();
                txtID.ReadOnly = true;
            }
            else
            {
                AlunoDB tabela = new AlunoDB();
                txtID.Text = tabela.ProximoCodigo().ToString();
                txtID.ReadOnly = true;
            }
        }

        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtIdade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPeso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtAltura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Registro = null;
            this.Dispose();
        }

        private void btnFoto_Click(object sender, EventArgs e)
        {
            string arquivo = Environment.CurrentDirectory + "\\" + txtID.Text + ".jpng";
            saveFoto.ShowDialog();
            if(File.Exists(saveFoto.FileName))
            {
                pbFoto.Image = null;
                Graphics g = pbFoto.CreateGraphics();
                g.Clear(Color.Gainsboro);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.Delete(arquivo);
                File.Copy(saveFoto.FileName, arquivo);
                g.DrawImage(Image.FromFile(arquivo), 0, 0, pbFoto.Width, pbFoto.Height);
                MessageBox.Show("Foto gravada com sucesso");

            }

        }

        private void FormFichaMatricula_Paint(object sender, PaintEventArgs e)
        {
  
            string arquivo = Environment.CurrentDirectory + "\\" + txtID.Text + ".png";
            if (File.Exists(arquivo))
            {
                Graphics g = pbFoto.CreateGraphics();
                g.DrawImage(Image.FromFile(arquivo), 0, 0, pbFoto.Width, pbFoto.Height);
            }
        } 
        }
    }


