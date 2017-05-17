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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        static private string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BazaDanych"].ConnectionString;

        static private SqlCommand cmd = null;
        static private SqlConnection connection = null;
        static private SqlDataAdapter adapt1 = null;

        private void Form6_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(ConnectionString);
            connection.Open();

            DayOfWeek dzien_tygodnia = DateTime.Now.DayOfWeek;

            switch (dzien_tygodnia)
            {
                case DayOfWeek.Monday:
                    label3.Text = "Poniedziałek";
                    break;
                case DayOfWeek.Tuesday:
                    label3.Text = "Wtorek";
                    break;
                case DayOfWeek.Wednesday:
                    label3.Text = "Środa";
                    break;
                case DayOfWeek.Thursday:
                    label3.Text = "Czwartek";
                    break;
                case DayOfWeek.Friday:
                    label3.Text = "Piątek";
                    break;
                case DayOfWeek.Saturday:
                    label3.Text = "Sobota";
                    break;
                case DayOfWeek.Sunday:
                    label3.Text = "Niedziela";
                    break;
                default:
                    break;
            }

            if (label3.Text == "Poniedziałek" || label3.Text == "Środa" || label3.Text == "Piątek")
            {
                adapt1 = new SqlDataAdapter("Select Nazwisko From Kurierzy Where Id % 2 = 0", connection);
                DataTable dt1 = new DataTable();
                adapt1.Fill(dt1);
                listBox1.ValueMember = "Nazwisko";
                listBox1.DataSource = dt1;
            }
            else if (label3.Text == "Wtorek" || label3.Text == "Czwartek" || label3.Text == "Sobota")
            {
                adapt1 = new SqlDataAdapter("Select Nazwisko From Kurierzy Where Id % 2 = 1", connection);
                DataTable dt1 = new DataTable();
                adapt1.Fill(dt1);
                listBox1.DisplayMember = "Nazwisko";
                listBox1.DataSource = dt1;
            }

            listBox1.ClearSelected();

            ////zliczanie ilości przesyłek
            //SqlDataAdapter adapt2 = new SqlDataAdapter("Select Count(*) as Ilosc From Przesylki", connection);
            //DataTable dt2 = new DataTable();
            //adapt2.Fill(dt2);
            //listBox2.ValueMember = "Ilosc";
            //listBox2.DataSource = dt2;

            SqlDataAdapter adapt2 = new SqlDataAdapter("Select Id From Przesylki Where Status like 'niedostarczono'", connection);
            DataTable dt2 = new DataTable();
            adapt2.Fill(dt2);
            listBox2.ValueMember = "Id";
            listBox2.DataSource = dt2;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Czy na pewno chcesz oznaczyć przesyłkę nr " + listBox2.Text + " jako 'dostarczono'?",
                      "Czy...?", MessageBoxButtons.YesNo);
            switch (dr)
            {
                case DialogResult.Yes:
                    string s1 = @"Update Przesylki set Status = 'dostarczono' Where Id = '" + listBox2.Text + "'";
                        cmd = new SqlCommand(s1, connection);
                        cmd.ExecuteNonQuery();

                        SqlDataAdapter adapt3 = new SqlDataAdapter("Select Id From Przesylki Where Status like 'niedostarczono'", connection);
                        DataTable dt3 = new DataTable();
                        adapt3.Fill(dt3);
                        listBox2.ValueMember = "Id";
                        listBox2.DataSource = dt3;
                    break;

                case DialogResult.No: break;
            }
        }

        private void Form6_FormClosing(object sender, FormClosingEventArgs e)
        {
            Zmienna.czy_form6 = false;
        }
    }
}
