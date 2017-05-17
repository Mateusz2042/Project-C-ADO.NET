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
using GMap.NET.MapProviders;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System.Net.NetworkInformation;

namespace Paczki
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }
        static private string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BazaDanych"].ConnectionString;

        static private SqlCommand cmd = null;
        static private SqlConnection connection = null;
        private int x;
        private int y;
        private GMapOverlay routesOverlay;
        private GMapRoute route;
        private GMapOverlay markersOverlay1;
        private GMapOverlay markersOverlay2;
        private GMarkerGoogle marker1;
        private GMarkerGoogle marker2;
        private bool isAvailable;

        private void Form7_Load(object sender, EventArgs e)
        {
            isAvailable = NetworkInterface.GetIsNetworkAvailable();

            if (isAvailable == true)
            {
                label12.Text = "ONLINE";
                label12.ForeColor = Color.Green;

                gMapControl1.DragButton = MouseButtons.Left;
                gMapControl1.CanDragMap = true;
                gMapControl1.MapProvider = GMapProviders.GoogleMap;
                gMapControl1.SetPositionByKeywords("Bielsko-Biała, Polska");
                //gMapControl1.Position = new PointLatLng(49.819, 19.049);
                gMapControl1.MinZoom = 0;
                gMapControl1.MaxZoom = 18;
                gMapControl1.Zoom = 8;
                gMapControl1.AutoScroll = true;
            }
            else
            {
                label12.Text = "OFFLINE";
                label12.ForeColor = Color.Red;
            }

            x = 0;

            try
            {
                connection = new SqlConnection(ConnectionString);
                connection.Open();
                try
                {
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
            catch (Exception)
            {
                MessageBox.Show("Błąd połączenia z bazą.");
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            trackBar1.Value = 0;

            button1.Enabled = true;

            //używam Id odbiorcy, gdyż ma identyczne Id jak przesyłka
            SqlDataAdapter adapt1 = new SqlDataAdapter("Select Miasto From Odbiorcy Where Id = '" + comboBox1.Text + "'", connection);
            DataTable dt1 = new DataTable();
            adapt1.Fill(dt1);
            listBox1.ValueMember = "Miasto";
            listBox1.DataSource = dt1;

            SqlDataAdapter adapt2 = new SqlDataAdapter("Select Ulica From Odbiorcy Where Id = '" + comboBox1.Text + "'", connection);
            DataTable dt2 = new DataTable();
            adapt2.Fill(dt2);
            listBox2.ValueMember = "Ulica";
            listBox2.DataSource = dt2;

            SqlDataAdapter adapt3 = new SqlDataAdapter("Select Nr_domu From Odbiorcy Where Id = '" + comboBox1.Text + "'", connection);
            DataTable dt3 = new DataTable();
            adapt3.Fill(dt3);
            listBox3.ValueMember = "Nr_domu";
            listBox3.DataSource = dt3;

            //Id nadawcy
            SqlDataAdapter adapt4 = new SqlDataAdapter("Select Miasto From Nadawcy Where Id = '" + comboBox1.Text + "'", connection);
            DataTable dt4 = new DataTable();
            adapt4.Fill(dt4);
            listBox4.ValueMember = "Miasto";
            listBox4.DataSource = dt4;

            SqlDataAdapter adapt5 = new SqlDataAdapter("Select Ulica From Nadawcy Where Id = '" + comboBox1.Text + "'", connection);
            DataTable dt5 = new DataTable();
            adapt5.Fill(dt5);
            listBox5.ValueMember = "Ulica";
            listBox5.DataSource = dt5;

            SqlDataAdapter adapt6 = new SqlDataAdapter("Select Nr_domu From Nadawcy Where Id = '" + comboBox1.Text + "'", connection);
            DataTable dt6 = new DataTable();
            adapt6.Fill(dt6);
            listBox6.ValueMember = "Nr_domu";
            listBox6.DataSource = dt6;

            //gMapControl1.DragButton = MouseButtons.Left;
            //gMapControl1.CanDragMap = true;
            //gMapControl1.MapProvider = GMapProviders.GoogleMap;
            //gMapControl1.SetPositionByKeywords(listBox1.Text + "Polska");
            ////gMapControl1.Position = new PointLatLng(49.819, 19.049);
            //gMapControl1.MinZoom = 8;
            //gMapControl1.MaxZoom = 18;
            //gMapControl1.Zoom = 8;
            //gMapControl1.AutoScroll = true;

            //sprawdzanie dostępu do Internetu
            isAvailable = NetworkInterface.GetIsNetworkAvailable();

            if (isAvailable == false)
            {
                MessageBox.Show("Brak dostępu do Internetu!!! \r\nNie można wyznaczyć trasy.");
                button1.Enabled = false;
            }
            else
            {
                gMapControl1.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
                GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;

                GDirections s;
                var x = GMapProviders.GoogleMap.GetDirections(out s, listBox1.Text + ", " + listBox2.Text + " " + listBox3.Text, listBox4.Text + ", " + listBox5.Text + " " + listBox6.Text, false, false, false, false, false);
                route = new GMapRoute(s.Route, "Trasa");

                route.Stroke.Width = 5;
                route.Stroke.Color = Color.Red;

                routesOverlay = new GMapOverlay("Trasa");
                routesOverlay.Routes.Add(route);
                gMapControl1.Overlays.Add(routesOverlay);
                gMapControl1.ZoomAndCenterRoute(route);

                //markery
                gMapControl1.SetPositionByKeywords(listBox1.Text + ", " + listBox2.Text + " " + listBox3.Text + "Polska");
                markersOverlay1 = new GMapOverlay("markers");
                marker1 = new GMarkerGoogle(new PointLatLng(gMapControl1.Position.Lat, gMapControl1.Position.Lng),
                GMarkerGoogleType.green);
                markersOverlay1.Markers.Add(marker1);
                gMapControl1.Overlays.Add(markersOverlay1);
                gMapControl1.SetPositionByKeywords(listBox4.Text + ", " + listBox5.Text + " " + listBox6.Text + "Polska");
                markersOverlay2 = new GMapOverlay("markers");
                marker2 = new GMarkerGoogle(new PointLatLng(gMapControl1.Position.Lat, gMapControl1.Position.Lng),
                GMarkerGoogleType.blue);
                markersOverlay2.Markers.Add(marker2);
                gMapControl1.Overlays.Add(markersOverlay2);

                comboBox1.Enabled = false;
                label7.ForeColor = Color.Red;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            y = trackBar1.Value;

            if (y > x)
            {
                gMapControl1.Zoom += (y - x);
                x = y;
            }
            else
            {
                gMapControl1.Zoom -= (x - y);
                x = y;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            routesOverlay.Routes.Remove(route);
            markersOverlay1.Markers.Remove(marker1);
            markersOverlay2.Markers.Remove(marker2);

            comboBox1.Enabled = true;
            label7.ForeColor = Color.Black;
            button1.Enabled = false;
        }

        private void Form7_FormClosing(object sender, FormClosingEventArgs e)
        {
            Zmienna.czy_form7 = false;
        }
        
    }
}
