using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace WFSqlCsEx1
{
    public partial class Form1 : Form
    {
        public string con = @"Data Source=LAPTOP-HOL6N2JR\SQLEXPRESS;Initial Catalog=dbCadastro;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Deseja Sair Realmente?", "Sair", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection objCon = new SqlConnection(con);
            objCon.Open();
            SqlCommand cmd = new SqlCommand("SELECT * from CLIENTE", objCon);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            da.SelectCommand = cmd;
            da.Fill(ds);
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = ds.Tables[0].TableName;
            objCon.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txt_nome_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_nome_Click(object sender, EventArgs e)
        {
            ClearLabPan();
            label2.ForeColor = Color.Aquamarine;
            panel1.BackColor = Color.Aquamarine;
        }

        private void ClearLabPan()
        {
            label1.ForeColor = Color.GhostWhite;
            label2.ForeColor = Color.GhostWhite;
            label3.ForeColor = Color.GhostWhite;
            panel1.BackColor = Color.GhostWhite;
            panel4.BackColor = Color.GhostWhite;
            panel6.BackColor = Color.GhostWhite;
        }

        private void txt_idade_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_email_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_idade_Click(object sender, EventArgs e)
        {
            ClearLabPan();
            label1.ForeColor = Color.Aquamarine;
            panel4.BackColor = Color.Aquamarine;
        }
        private void txt_email_Click(object sender, EventArgs e)
        {
            ClearLabPan();
            label3.ForeColor = Color.Aquamarine;
            panel6.BackColor = Color.Aquamarine;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dbCadastroDataSetBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

            SqlConnection objCon = new SqlConnection(con);
            if (txt_nome.Text == "" || txt_idade.Text == "" || txt_email.Text == "")
            {
                MessageBox.Show("Campo Não Preenchido!");
                txt_nome.Focus();
                LimpaCampos();
                return;
            }
            try
            {
                int idade = int.Parse(txt_idade.Text);
                string sql = $"INSERT INTO CLIENTE(NOME, IDADE, EMAIL) VALUES('{txt_nome.Text}', {idade}, '{txt_email.Text}')";

                SqlCommand cmd = new SqlCommand(sql, objCon);
                cmd.CommandType = CommandType.Text;
                objCon.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Dado Cadastrado com Sucesso");
                    LimpaCampos();
                    Form1_Load(sender, e);
                }
            }
            catch (Exception ex)
            {
                LimpaCampos();
                MessageBox.Show("Dado Nao Cadastrado!\n" + ex.Message);
            }
            finally
            {
                objCon.Close();
            }
        }

        public void LimpaCampos()
        {
            txt_nome.Focus();
            txt_nome.Clear();
            txt_email.Clear();
            txt_idade.Clear();

        }

        private void btn_excluir_Click(object sender, EventArgs e)
        {
            string sql = $"DELETE FROM CLIENTE WHERE NOME = '{txt_nome.Text}'";
            SqlConnection objConn= new SqlConnection(con);
            
            SqlCommand cmd = new SqlCommand(sql, objConn);

            cmd.CommandType = CommandType.Text;
            objConn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    {
                    MessageBox.Show("Registro Excluido");
                    LimpaCampos();
                    Form1_Load(sender, e);
                    }
            }catch (Exception ex)
            {
                LimpaCampos();
                MessageBox.Show("Nao foi possivel apagar o registro");
                throw;
            }
            finally
            {
                objConn.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_nome.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txt_idade.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txt_email.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void btn_atualizar_Click(object sender, EventArgs e)
        {
   
        }
    }
}
