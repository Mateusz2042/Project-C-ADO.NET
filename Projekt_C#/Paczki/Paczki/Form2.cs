using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace Paczki
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        static private string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BazaDanych"].ConnectionString;

        static private SqlCommand cmd = null;

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    //label15.Text = connection.State.ToString();

                    //Wrzucenie danych do bazy
                    try
                    {
                        //Przesylki
                        SqlDataAdapter adapt1 = new SqlDataAdapter("Select * From Przesylki", connection);
                        DataTable dt1 = new DataTable();
                        adapt1.Fill(dt1);
                        dataGridView1.DataSource = dt1;

                        //Nadawcy
                        SqlDataAdapter adapt2 = new SqlDataAdapter("Select * From Nadawcy", connection);
                        DataTable dt2 = new DataTable();
                        adapt2.Fill(dt2);
                        dataGridView2.DataSource = dt2;

                        //Odbiorcy
                        SqlDataAdapter adapt3 = new SqlDataAdapter("Select * From Odbiorcy", connection);
                        DataTable dt3 = new DataTable();
                        adapt3.Fill(dt3);
                        dataGridView3.DataSource = dt3;

                        //Kurierzy
                        SqlDataAdapter adapt4 = new SqlDataAdapter("Select * From Kurierzy", connection);
                        DataTable dt4 = new DataTable();
                        adapt4.Fill(dt4);
                        dataGridView4.DataSource = dt4;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Błąd" + ex.Message);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Błąd połączenia z bazą.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 frm1 = new Form3();
            frm1.Show();
            frm1.Location = new Point(480, 250);
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Form4 form = new Form4();
            form.Show();
            form.Location = new Point(500, 250);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            Form5 form = new Form5();
            form.Show();
            form.Location = new Point(500, 250);
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Zmienna.czy_form2 = false;
        }
    }
}
