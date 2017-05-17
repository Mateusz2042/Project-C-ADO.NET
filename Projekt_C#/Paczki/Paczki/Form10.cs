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
using Microsoft.Reporting.WinForms;
using System.IO;

namespace Paczki
{
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
        }

        static private string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BazaDanych"].ConnectionString;
        SqlConnection connection = null;
        static private SqlCommand cmd = null;

        private DataTable getData()
        {
            DataSet dss = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter("SELECT Nazwisko FROM Kurierzy", connection);
            da.Fill(dss);
            DataTable dt = dss.Tables["Nazwisko"];
            return dt;
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(ConnectionString);
            connection.Open();

            File.Copy(@"C:\Users\" + Environment.UserName + @"\Desktop\Projekt_C#\Paczki\Paczki\Report1.rdlc", Path.Combine(Application.StartupPath, "Report1.rdlc"));

            string query = "SELECT Nazwisko FROM Kurierzy";

            cmd = new SqlCommand(query, connection);

            SqlDataAdapter adapt = new SqlDataAdapter();
            adapt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adapt.Fill(ds);

            reportViewer1.LocalReport.ReportEmbeddedResource = "Report1.rdlc";
            reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("Nazwisko", ds.Tables[0]));
            reportViewer1.RefreshReport();
        }

        private void Form10_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (File.Exists(Path.Combine(Application.StartupPath, "Report1.rdlc")))
            {
                File.Delete(Path.Combine(Application.StartupPath, "Report1.rdlc"));
            }
            Zmienna.czy_form10 = false;
        }
    }
}
