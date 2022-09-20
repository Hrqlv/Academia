using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjAcademia
{
    class Academia
    {
        public BindingList<Aluno> Alunos { get; set; }
        public Academia()
        {
            Alunos = new BindingList<Aluno>();
            AlunoDB tabela = new AlunoDB();
            tabela.Select(Alunos);
        }
        public void Matricular(Aluno novo)
        {
            if (novo != null)
            {
                try
                {
                    Alunos.Add(novo);
                    AlunoDB tabela = new AlunoDB();
                    tabela.Inserir(novo);
                }
                catch (Exception Erro)
                {
                    System.Windows.Forms.MessageBox.Show(Erro.Message);
                }
            }
        }


        internal void Editar(Aluno aluno)
        {
            Aluno reg = Alunos.FirstOrDefault(i => i.ID == aluno.ID);
            if(reg != null)
            {
                reg.Nome = aluno.Nome;
                reg.Idade = aluno.Idade;
                reg.Peso = aluno.Peso;
                reg.Altura = aluno.Altura;
                try
                {
                    AlunoDB tabela = new AlunoDB();
                    tabela.Editar(aluno);
                }
                catch (Exception Erro)
                {
                    System.Windows.Forms.MessageBox.Show(Erro.Message);
                }
            }
        }

        public void EXCLUIR(Aluno aluno)
        {
            Aluno reg = Alunos.FirstOrDefault(i => i.ID == aluno.ID);
            if(reg != null)
            {
                Alunos.Remove(reg);
                AlunoDB tabela = new AlunoDB();
                try
                {
                    tabela.Excluir(reg);
                }
                catch (Exception Erro)
                {
                    System.Windows.Forms.MessageBox.Show(Erro.Message);
                }
            }
        }
    }
}
