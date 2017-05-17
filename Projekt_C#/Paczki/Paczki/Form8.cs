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
using System.Data.OleDb;

namespace Paczki
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            String name = "Kody";
            String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                            "C:\\Users\\" + Environment.UserName + "\\Desktop\\Projekt_C#\\Paczki\\Paczki\\BazaKodów.xlsx" +
                            ";Extended Properties='Excel 12.0 XML;HDR=YES;';";

            OleDbConnection con = new OleDbConnection(constr);
            OleDbCommand oconn = new OleDbCommand("Select * From [" + name + "$]", con);
            con.Open();

            OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex % 2 == 1)   // parzyste wiersze mają niebieskie tło
                e.CellStyle.BackColor = Color.Lavender;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            String name = "Kody";
            String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                            "C:\\Users\\" + Environment.UserName + "\\Desktop\\Projekt_C#\\Paczki\\Paczki\\BazaKodów.xlsx" +
                            ";Extended Properties='Excel 12.0 XML;HDR=YES;';";

            OleDbConnection con = new OleDbConnection(constr);
            OleDbCommand oconn = new OleDbCommand("Select * From [" + name + "$] where Miasto like '" + textBox1.Text + "%'", con);
            con.Open();

            OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;
        }

        private void Form8_FormClosing(object sender, FormClosingEventArgs e)
        {
            Zmienna.czy_form8 = false;
        }
    }
}
