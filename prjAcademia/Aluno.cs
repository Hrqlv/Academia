using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjAcademia
{
    class Aluno
    {
        public long ID { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public double Peso { get; set; }
        public double Altura { get; set; }

        public Aluno (long ID, string Nome, int Idade,
            double Peso, double Altura)
        {
            this.ID = ID;
            this.Nome = Nome;
            this.Idade = Idade;
            this.Peso = Peso;
            this.Altura = Altura;
        }

        public Aluno()
        {
            // TODO: Complete member initialization
        }
        public double IMC
        {
            get
            {

                return Math.Round(Peso / Math.Pow(Altura, 2), 2);
            }

        }

        public string Classificacao
        {
            get
            {
                double Indice = IMC;
                if (Indice <= 18.5) return "BAIXO PESO";
                else if (Indice > 18.5 && Indice <= 24.9) return "PESO NORMAL";
                else if (Indice >= 25 && Indice <= 24.9) return "SOBRE PESO";
                else if (Indice >= 30 && Indice <= 34.9) return "OBESIDADE I";
                else if (Indice >= 35 && Indice <= 39.9) return "OBESIDADE II";
                else if (Indice >= 40 && Indice <= 49.9) return "OBESIDADE III";
                else return "OBESIDADE IV";
            }
        }
    }
}
