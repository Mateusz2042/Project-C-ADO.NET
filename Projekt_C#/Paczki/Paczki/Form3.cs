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
using System.Windows.Input;

namespace Paczki
{
    public partial class Form3 : Form
    {

        public Form3()
        {
            InitializeComponent();
        }

        public void visible()
        {
            button2.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
            button5.Visible = true;
            button6.Visible = true;
        }

        public void invisible()
        {
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
        }

        public void checking()
        {
            if (textBox1.Text == "")
                label2.ForeColor = Color.Red;
            else
            {
                label2.ForeColor = Color.Black;
                if (textBox1.Text[0] >= 97 && textBox1.Text[0] <= 122)
                {
                    textBox1.Text = String.Concat(textBox1.Text[0].ToString().ToUpper(), textBox1.Text.Substring(1, textBox1.Text.Length - 1));
                }
            }

            if (textBox2.Text == "" || textBox2.Text[0] == '-')
                label3.ForeColor = Color.Red;
            else
            {
                label3.ForeColor = Color.Black;
                if (textBox2.Text[0] >= 97 && textBox2.Text[0] <= 122)
                {
                    textBox2.Text = String.Concat(textBox2.Text[0].ToString().ToUpper(), textBox2.Text.Substring(1, textBox2.Text.Length - 1));
                }
            }

            if (textBox3.Text == "")
                label4.ForeColor = Color.Red;
            else
                label4.ForeColor = Color.Black;

            if (textBox4.Text == "" || textBox4.Text[0] == ',')
                label5.ForeColor = Color.Red;
            else
                label5.ForeColor = Color.Black;

            if (textBox5.Text == "")
                label6.ForeColor = Color.Red;
            else
                label6.ForeColor = Color.Black;
        }

        static private string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BazaDanych"].ConnectionString;
        SqlConnection connection;
        Form2 frm = new Form2();

        private void button1_Click(object sender, EventArgs e)
        {
            checking();

            int size_surname = textBox2.TextLength;

            if (label2.ForeColor == Color.Red || label3.ForeColor == Color.Red || label4.ForeColor == Color.Red || label5.ForeColor == Color.Red)
            {
                MessageBox.Show("Zostawiłeś puste pola lub pola są uzupełnione niepoprawnie!!! \r\nUzupełnij je lub popraw.");
            }
            else if (textBox5.TextLength < 9)
            {
                MessageBox.Show("Niepoprawny numer telefonu.\r\nZa mało cyfr.");
            }
            else if (Convert.ToInt32(textBox3.Text) < 18)
            {
                int wiek = 18 - Convert.ToInt32(textBox3.Text);
                MessageBox.Show("Kandydat nie ma ukończonych 18 lat.\r\nNiech wróci za " + wiek + " lat.");
            }
            else if (Convert.ToInt32(textBox3.Text) > 100)
            {
                MessageBox.Show("Niemożliwy wiek.");
            }
            else if (Convert.ToInt32(textBox4.Text) < 1850)
            {
                MessageBox.Show("Za mała pensja. Minimum 1850 zł.");
            }
            else if (textBox5.Text[0] == '0')
            {
                MessageBox.Show("Niepoprawny numer telefonu.");
            }
            else if (textBox2.Text[size_surname - 1] == '-')
            {
                MessageBox.Show("Niepoprawny format nazwiska.");
            }
            else
            {
                Kurierzy kurier = new Kurierzy();
                kurier.Imie = textBox1.Text + " ";
                kurier.Nazwisko = textBox2.Text;
                kurier.Wiek = Convert.ToInt32(textBox3.Text);
                kurier.Pensja = Convert.ToInt32(textBox4.Text);
                kurier.Numer_telefonu = textBox5.Text;

                string s1 = @"INSERT INTO Kurierzy (Imie, Nazwisko, Wiek, Pensja, Numer_telefonu) VALUES ('" + kurier.Imie + "','" + kurier.Nazwisko + "','" + kurier.Wiek + "','" + kurier.Pensja + "','" + kurier.Numer_telefonu + "')";
                SqlCommand cmd = new SqlCommand(s1, connection);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                try
                {
                    frm.Show();
                    frm.Location = new Point(350, 200);
                    this.Close();
                }
                catch (Exception)
                {
                    throw;
                }

                MessageBox.Show("Dodano nowego kuriera: " + kurier.Imie + " " + kurier.Nazwisko);

                Close();
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(ConnectionString);
            connection.Open();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                visible();
            }
            else
            {
                invisible();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Marek" + " ";
            textBox2.Text = "Nowacki";
            textBox3.Text = "32";
            textBox4.Text = "2350";
            textBox5.Text = "596872102";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Olaf" + " ";
            textBox2.Text = "Matuszek";
            textBox3.Text = "28";
            textBox4.Text = "2130";
            textBox5.Text = "782540123";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Zbigniew" + " ";
            textBox2.Text = "Byrski";
            textBox3.Text = "41";
            textBox4.Text = "2020";
            textBox5.Text = "541023688";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Artur" + " ";
            textBox2.Text = "Osowski";
            textBox3.Text = "29";
            textBox4.Text = "1980";
            textBox5.Text = "533941002";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Krystian" + " ";
            textBox2.Text = "Szwed";
            textBox3.Text = "35";
            textBox4.Text = "2150";
            textBox5.Text = "799520114";
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = !(Char.IsNumber(e.KeyChar) || e.KeyChar == 8))
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = !(Char.IsNumber(e.KeyChar) || e.KeyChar == 8))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = !(Char.IsLetter(e.KeyChar) || e.KeyChar == '-' || e.KeyChar == 8))
            {
                e.Handled = true;
            }
            // tylko jeden myślnik w polu
            else if (e.KeyChar == '-' && (sender as TextBox).Text.Contains('-'))
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = !(Char.IsLetter(e.KeyChar) || e.KeyChar == 8))
            {
                e.Handled = true;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frm.Show();
            frm.Location = new Point(350, 200);
            this.Close();
        }
    }
}
