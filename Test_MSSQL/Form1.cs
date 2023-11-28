using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace Test_MSSQL
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

            sqlConnection.Open();

            if (sqlConnection.State != ConnectionState.Open)
            {
                MessageBox.Show("Подключение к базе данных отсутствует!");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand(
            $"INSERT INTO [Students] (Name, Surname, Birthday, Phone, Email) VALUES (@Name, @Surname, @Birthday, @Phone, @Email)",
            sqlConnection);

            DateTime birthday = DateTime.Parse(textBox3.Text);

            sqlCommand.Parameters.AddWithValue("Name", textBox1.Text);
            sqlCommand.Parameters.AddWithValue("Surname", textBox2.Text);
            sqlCommand.Parameters.AddWithValue("Birthday", $"{birthday.Month}/{birthday.Day}/{birthday.Year}");
            sqlCommand.Parameters.AddWithValue("Phone", textBox4.Text);
            sqlCommand.Parameters.AddWithValue("Email", textBox5.Text);

            MessageBox.Show(sqlCommand.ExecuteNonQuery().ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(
                textBox6.Text,
                sqlConnection);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            dataGridView1.DataSource = dataSet.Tables[0];
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
