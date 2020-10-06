using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace CRUD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        MySqlConnection conexion = new MySqlConnection();
        private void button1_Click(object sender, EventArgs e)
        {
            string CadenaConexion = "Database = " + textBox3.Text +"; Data Source = " + textBox4.Text + "; User Id = " + textBox1.Text + "; pwd = " + textBox2.Text + "";
            conexion.ConnectionString = CadenaConexion;
            try
            {
                conexion.Open();
                string sql = "SHOW TABLES;";
                int length = 0;
                using (MySqlCommand comando = new MySqlCommand(sql, conexion))
                {
                    using (MySqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            length += 1;
                        }
                    }
                }
                string[] array = new string[length];
                int index = 0;
                using (MySqlCommand comando = new MySqlCommand(sql, conexion))
                {
                    using (MySqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            array[index] = reader.GetString(0);
                            index++;
                        }
                    }
                }
                comboBox1.Items.AddRange(array);
                MessageBox.Show("Conectado perfectamente");
                panel1.Visible = false;
                panel2.Visible = true;
            }
            catch(MySqlException ex)
            {
                MessageBox.Show("Error: " + ex);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if(Convert.ToString(e.KeyData) == "Return")
            {
                button1_Click(sender, e);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(Convert.ToString(e.KeyData) == "Return")
            {
                textBox2.Focus();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if(Convert.ToString(e.KeyData) == "Return")
            {
                textBox3.Focus();
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToString(e.KeyData) == "Return")
            {
                textBox4.Focus();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (MySqlCommand comando = new MySqlCommand("SELECT * FROM " + comboBox1.SelectedItem.ToString() + ";", conexion))
            {
                using (MySqlDataAdapter adaptador = new MySqlDataAdapter(comando))
                {
                    using (DataSet set = new DataSet())
                    {
                        adaptador.Fill(set);
                        dataGridView1.DataSource = set.Tables[0];
                    }
                }
            }
        }
    }
}
