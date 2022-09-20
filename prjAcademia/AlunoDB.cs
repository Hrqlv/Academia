using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.ComponentModel;

namespace prjAcademia
{
    class AlunoDB
    {
        ServidorSQL academia = new ServidorSQL();
        public void Inserir(Aluno reg)
        {
            using (var banco = new SQLiteCommand(academia.Open()))
            {
                string sql = String.Format("INSERT INTO ALUNO VALUES({0},'{1}',{2},{3},{4})",
                    reg.ID, reg.Nome, reg.Idade, reg.Peso,
                    reg.Altura.ToString().Replace(',', '.')
                    );
                banco.CommandText = sql;
                banco.ExecuteNonQuery();
            }
        }
        public void Select(BindingList<Aluno> Alunos)
        {
            using (var banco = new SQLiteCommand(academia.Open()))
            {
                banco.CommandText = "SELECT * FROM ALUNO ORDER BY ID";
                SQLiteDataReader dr = banco.ExecuteReader();
                while (dr.Read())
                {
                    Aluno item = new Aluno(
                        dr.GetInt16(0),
                        dr.GetString(1),
                        dr.GetInt16(2),
                        dr.GetDouble(3),
                        dr.GetDouble(4)
                        );
                    Alunos.Add(item);
                }
            }
        }
        public void Editar(Aluno reg)
        {
            using (var banco = new SQLiteCommand(academia.Open()))
            {
                string sql = String.Format("UPDATE ALUNO SET NOME = '{1}',IDADE = {2}," + "PESO = {3}, ALTURA = {4} WHERE ID = {0}",
                    reg.ID, reg.Nome, reg.Idade, reg.Peso,
                    reg.Altura.ToString().Replace(',', '.')
                    );
                banco.CommandText = sql;
                banco.ExecuteNonQuery();
            }
        }
        public void Excluir(Aluno reg)
        {
            using (var banco = new SQLiteCommand(academia.Open()))
            {
                string sql = String.Format("DELETE FROM ALUNO WHERE ID = {0}", reg.ID);
                banco.CommandText = sql;
                banco.ExecuteNonQuery();
            }
        }

        public void Filtrar(System.Windows.Forms.DataGridView dgvLista, string tipo, string nome)
        {
            string sql = "";
            using (var banco = new SQLiteCommand(academia.Open()))
            {
                if (tipo.Equals("I"))
                    sql = "SELECT * FROM ALUNO WHERE " +
                        "NOME LIKE '" + nome + "%'";
                else if (tipo.Equals("M"))
                    sql = "SELECT * FROM ALUNO WHERE " +
                        " NOME LIKE '%" + nome + "%";
                else
                    sql = "SELECT * FROM ALUNO WHERE " +
                        "NOME LIKE '%" + nome + "'";

                banco.CommandText = sql;
                SQLiteDataReader dr = banco.ExecuteReader();
                BindingList<Aluno> Lista = new BindingList<Aluno>();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Aluno reg = new Aluno(
                            dr.GetInt16(0),
                            dr.GetString(1),
                            dr.GetInt16(2),
                            dr.GetDouble(3),
                            dr.GetDouble(4)
                            );
                        Lista.Add(reg);
                    }
                    dr.Close();
                    dgvLista.DataSource = Lista;
                }
            }
        }

        public Aluno SelecionarRegistro(object codigo)
        {

            string id = codigo.ToString();

            using (var banco = new SQLiteCommand(academia.Open()))
            {
                banco.CommandText = "SELECT * FROM ALUNO WHERE ID =" +
                    id;

                SQLiteDataReader dr = banco.ExecuteReader();
                while (dr.Read())
                {
                    Aluno reg = new Aluno(
                        dr.GetInt16(0),
                        dr.GetString(1),
                        dr.GetInt16(2),
                        dr.GetDouble(3),
                        dr.GetDouble(4)
                        );
                    return reg;
                }
                dr.Close();
                return null;
            }
        }
        public int ProximoCodigo()
        {
            using (var banco = new SQLiteCommand(academia.Open()))
            {
                try
                {
                    banco.CommandText = "SELECT MAX(ID) AS COD FROM ALUNO";
                    SQLiteDataReader dr = banco.ExecuteReader();
                    while (dr.Read())
                    {
                        int cod = dr.GetInt32(0) + 1;
                        dr.Close();
                        return cod;
                    }
                }
                catch (Exception)
                {
                    return 1;
                }
            }
            return 1;
        }
    }
}