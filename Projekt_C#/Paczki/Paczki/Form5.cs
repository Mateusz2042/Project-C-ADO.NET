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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        static private string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BazaDanych"].ConnectionString;

        static private SqlCommand cmd1 = null;
        static private SqlCommand cmd2 = null;
        static private SqlCommand cmd3 = null;
        static private SqlConnection connection = null;

        Form2 frm = new Form2();

        private void Form5_Load(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(ConnectionString);
                connection.Open();
                    try
                    {
                        SqlDataAdapter adapt1 = new SqlDataAdapter("Select Id From Kurierzy", connection);
                        DataTable dt1 = new DataTable();
                        adapt1.Fill(dt1);
                        comboBox1.ValueMember = "Id";
                        comboBox2.ValueMember = "Id";
                        comboBox1.DataSource = dt1;
                        comboBox2.DataSource = dt1;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Błąd" + ex.Message);
                    }
            }
            catch (Exception)
            {
                MessageBox.Show("Błąd połączenia z bazą.");
            }
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SqlDataAdapter adapt2 = new SqlDataAdapter("Select Nazwisko From Kurierzy Where Id = '" + comboBox2.Text + "'", connection);
            DataTable dt2 = new DataTable();
            adapt2.Fill(dt2);
            listBox1.ValueMember = "Nazwisko";
            listBox1.DataSource = dt2;
            SqlDataAdapter adapt3 = new SqlDataAdapter("Select Pensja From Kurierzy Where Id = '" + comboBox2.Text + "'", connection);
            DataTable dt3 = new DataTable();
            adapt3.Fill(dt3);
            listBox2.ValueMember = "Pensja";
            listBox2.DataSource = dt3;
            listBox1.BackColor = Color.White;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frm.Show();
            frm.Location = new Point(350, 200);
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
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

            DialogResult dr = MessageBox.Show("Czy na pewno chcesz usunąć kuriera o podanym ID?",
                      "Czy...?", MessageBoxButtons.YesNo);
            switch (dr)
            {
                case DialogResult.Yes:
                    if (comboBox1.Text != "")
                    {
                        string s1 = @"Delete from Kurierzy Where Id = '" + comboBox1.Text + "'";
                        cmd1 = new SqlCommand(s1, connection);
                        cmd1.ExecuteNonQuery();
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

        private void button4_Click(object sender, EventArgs e)
        {
            frm.Show();
            frm.Location = new Point(350, 200);
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                label7.ForeColor = Color.Red;
            else
            {
                label7.ForeColor = Color.Black;
            }

            if (label7.ForeColor == Color.Red || textBox1.Text[0] == ',')
            {
                MessageBox.Show("Zostawiłeś puste pola lub pola są uzupełnione niepoprawnie!!! \r\nUzupełnij je lub popraw.");
            }
            else if (Convert.ToInt32(textBox1.Text) < 1850)
            {
                MessageBox.Show("Za mała pensja. Minimum 1850 zł.");
            }
            else
            {
                DialogResult dr = MessageBox.Show("Czy na pewno chcesz zmienić pensję kuriera o podanym ID?",
                      "Czy...?", MessageBoxButtons.YesNo);
                switch (dr)
                {
                    case DialogResult.Yes:
                        {
                            string s1 = @"Update Kurierzy set Pensja ='" + textBox1.Text + "' Where Id = '" + comboBox1.Text + "'";
                            cmd1 = new SqlCommand(s1, connection);
                            cmd1.ExecuteNonQuery();
                            frm.Show();
                            frm.Location = new Point(350, 200);
                            this.Close();
                            break;
                        }

                    case DialogResult.No: break;
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = !(Char.IsNumber(e.KeyChar) || e.KeyChar == ',' || e.KeyChar == 8))
            {
                e.Handled = true;
            }
            // tylko jeden przecinek w polu
            else if (e.KeyChar == ',' && (sender as TextBox).Text.Contains(','))
            {
                e.Handled = true;
            }
        }
    }
}
