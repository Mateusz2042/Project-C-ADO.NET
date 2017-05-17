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

namespace Paczki
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        static private string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BazaDanych"].ConnectionString;

        static private SqlCommand cmd1 = null;
        static private SqlCommand cmd2 = null;
        static private SqlCommand cmd3 = null;
        static private SqlConnection connection = null;

        Form2 frm = new Form2();

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(ConnectionString);
                connection.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("Błąd połączenia z bazą.");
            }

            DialogResult dr = MessageBox.Show("Czy na pewno chcesz usunąć przesyłkę o podanym ID?",
                      "Czy...?", MessageBoxButtons.YesNo);
            switch (dr)
            {
                case DialogResult.Yes:
                    if (comboBox1.Text != "")
                    {
                        string s1 = @"Delete from Przesylki Where Id = '" + comboBox1.Text + "'";
                        string s2 = @"Delete from Nadawcy Where Id = '" + comboBox1.Text + "'";
                        string s3 = @"Delete from Odbiorcy Where Id = '" + comboBox1.Text + "'";
                        cmd1 = new SqlCommand(s1, connection);
                        cmd2 = new SqlCommand(s2, connection);
                        cmd3 = new SqlCommand(s3, connection);
                        cmd1.ExecuteNonQuery();
                        cmd2.ExecuteNonQuery();
                        cmd3.ExecuteNonQuery();
                        frm.Show();
                        frm.Location = new Point(350, 200);
                        this.Close();
                        break;
                    }
                    else
                    {
                        MessageBox.Show("Brak danych do usunięcia.");
                        break;
                    }

                case DialogResult.No: break;
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    try
                    {
                        //Przesylki
                        SqlDataAdapter adapt1 = new SqlDataAdapter("Select Id From Przesylki", connection);
                        DataTable dt1 = new DataTable();
                        adapt1.Fill(dt1);
                        comboBox1.ValueMember = "Id";
                        comboBox1.DataSource = dt1;
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

        private void button2_Click(object sender, EventArgs e)
        {
            frm.Show();
            frm.Location = new Point(350, 200);
            this.Close();
        }
    }
}
