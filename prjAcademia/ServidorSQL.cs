using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace prjAcademia
{
    class ServidorSQL
    {
        public SQLiteConnection Conexao { get; set; }
        public string Database { get; set; }

        public ServidorSQL()
        {
            Database = Environment.CurrentDirectory + "\\academia.sqlite";
        }
        public SQLiteConnection Open()
        {
            string sql = String.Format("Data Source = {0}; version=3; ", Database);
            Conexao = new SQLiteConnection(sql);
            Conexao.Open();
            return Conexao;
        }
        public bool CriaBancoDados()
        {
            try
            {
                if(!File.Exists(Database))
                {
                    SQLiteConnection.CreateFile(Database);
                    CriarTabelas();
                    return true;
                }
                return false;
            } 
            catch (Exception)
            {
                return false;
            }
        }

        private void CriarTabelas()
        {
            try
            {
                string sql = "CREATE TABLE ALUNO (" +
                    "ID INT PRIMARY KEY, NOME VARCHAR(45)," +
                    "IDADE INT, PESO DOUBLE, ALTURA DOUBLE)";
                using(var banco = new SQLiteCommand(Open())){
                    banco.CommandText = sql;
                    banco.ExecuteNonQuery();
                }
            }
            catch (Exception Erro)
            {
                System.Windows.Forms.
                MessageBox.Show(Erro.Message, "ERRO AO CRIAR TELA");
            }
        }
    }
}
