using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

//Kreator wysyłek pocztowych

namespace Paczki
{
    public partial class Form1 : Form
    {
        //zewnętrzne kontrolki
        Kontrolka1 list = new Kontrolka1();
        Kontrolka paczka = new Kontrolka();
        FontDialog dlg_czcionka = new FontDialog();
        ColorDialog dlg_kolor = new ColorDialog();
        string waga;
        static private string CreateConnectionString = "Data Source=(localdb)\\Projects;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False";
        static private string dbName = "BazaDanych";
        //static private string ConnectionString = "Data Source=(localdb)\\Projects;Initial Catalog=BazaDanych;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False";
        static private string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BazaDanych"].ConnectionString;

        static private SqlConnection connection = null;
        static private SqlCommand cmd = null;
        static private SqlDataReader reader = null;

        //tworzenie nowej bazy
        private static string NewDataBase()
        {
            //ścieżka do folderu wykonującego program
            string[] files_of_database = { Path.Combine(Application.StartupPath, dbName + ".mdf"),
                       Path.Combine(Application.StartupPath, dbName + ".ldf") };

            string query = "CREATE DATABASE " + dbName +
                " ON PRIMARY" +
                " (NAME = " + dbName + "_data," +
                " FILENAME = '" + files_of_database[0] + "'," +
                " SIZE = 5MB," +
                " MAXSIZE = 10MB," +
                " FILEGROWTH = 10%)" +

                " LOG ON" +
                " (NAME = " + dbName + "_log," +
                " FILENAME = '" + files_of_database[1] + "'," +
                " SIZE = 5MB," +
                " MAXSIZE = 5MB," +
                " FILEGROWTH = 10%)" +
                ";";

            return query;
        }

        //zamknięcie połączenia
        private static void Close()
        {
            if (reader != null)
            {
                reader.Close();
            }
            if (connection != null)
            {
                connection.Close();
            }
        }

        //łączenie z bazą
        private void ConnectToDatabase()
        {
            try
            {
                connection = new SqlConnection(ConnectionString);
                connection.Open();
                label15.Text = "Połączono z bazą";
                //label15.Text = connection.State.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Błąd połączenia z bazą.");
            }
        }

        //pojawianie się kontrolek
        public void visible()
        {
            button2.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
            button5.Visible = true;
            button6.Visible = true;
            button7.Visible = true;
            button8.Visible = true;
            button9.Visible = true;
            button10.Visible = true;
            button11.Visible = true;
            button12.Visible = true;
            button13.Visible = true;
        }

        //znikanie kontrolek
        public void invisible()
        {
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = false;
            button8.Visible = false;
            button9.Visible = false;
            button10.Visible = false;
            button11.Visible = false;
            button12.Visible = false;
            button13.Visible = false;
        }

        //czyszczenie pól
        public void clearing()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox11.Clear();
            textBox12.Clear();

            if (radioButton1.Checked)
            {
                list.textbox1 = "";
                list.combobox1 = "";
                list.Button1 = "Zatwierdź";
                
            }
            else if (radioButton2.Checked)
            {
                paczka.textbox1 = "";
                paczka.combobox1 = "";
                paczka.textbox2 = "";
                paczka.Button1 = "Zatwierdź";
            }
        }

        //ustawienia czcionki
        public void font()
        {
            textBox1.Font = dlg_czcionka.Font;
            textBox2.Font = dlg_czcionka.Font;
            textBox3.Font = dlg_czcionka.Font;
            textBox4.Font = dlg_czcionka.Font;
            textBox5.Font = dlg_czcionka.Font;
            textBox6.Font = dlg_czcionka.Font;
            textBox7.Font = dlg_czcionka.Font;
            textBox8.Font = dlg_czcionka.Font;
            textBox9.Font = dlg_czcionka.Font;
            textBox10.Font = dlg_czcionka.Font;
            textBox11.Font = dlg_czcionka.Font;
            textBox12.Font = dlg_czcionka.Font;
        }

        // ustawienie czarnej czcionki
        public void black_font()
        {
            label2.ForeColor = Color.Black;
            label3.ForeColor = Color.Black;
            label4.ForeColor = Color.Black;
            label5.ForeColor = Color.Black;
            label6.ForeColor = Color.Black;
            label7.ForeColor = Color.Black;
            label8.ForeColor = Color.Black;
            label9.ForeColor = Color.Black;
            label10.ForeColor = Color.Black;
            label11.ForeColor = Color.Black;
        }

        //sprawdzanie czy pole nie jest puste, poprawność pola,
        //zmiana pierwszej litery na wielką
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

            if (textBox3.Text == "" || textBox11.Text == "" || textBox3.Text[0] == '-')
                label4.ForeColor = Color.Red;
            else
            {
                label4.ForeColor = Color.Black;
                if (textBox3.Text[0] >= 97 && textBox3.Text[0] <= 122)
                {
                    textBox3.Text = String.Concat(textBox3.Text[0].ToString().ToUpper(), textBox3.Text.Substring(1, textBox3.Text.Length - 1));
                }
            }

            if (textBox4.Text == "" || textBox4.Text[0] == '-' || textBox4.Text[1] == '-' || textBox4.Text[2] != '-' || textBox4.Text[3] == '-' || textBox4.Text[4] == '-' || textBox4.Text[5] == '-')
                label5.ForeColor = Color.Red;
            else
                label5.ForeColor = Color.Black;

            if (textBox5.Text == "" || textBox5.Text[0] == '-')
                label6.ForeColor = Color.Red;
            else
            {
                label6.ForeColor = Color.Black;
                if (textBox5.Text[0] >= 97 && textBox5.Text[0] <= 122)
                {
                    textBox5.Text = String.Concat(textBox5.Text[0].ToString().ToUpper(), textBox5.Text.Substring(1, textBox5.Text.Length - 1));
                }
            }

            if (textBox6.Text == "")
                label7.ForeColor = Color.Red;
            else
            {
                label7.ForeColor = Color.Black;
                if (textBox6.Text[0] >= 97 && textBox6.Text[0] <= 122)
                {
                    textBox6.Text = String.Concat(textBox6.Text[0].ToString().ToUpper(), textBox6.Text.Substring(1, textBox6.Text.Length - 1));
                }
            }

            if (textBox7.Text == "" || textBox7.Text[0] == '-')
                label8.ForeColor = Color.Red;
            else
            {
                label8.ForeColor = Color.Black;
                if (textBox7.Text[0] >= 97 && textBox7.Text[0] <= 122)
                {
                    textBox7.Text = String.Concat(textBox7.Text[0].ToString().ToUpper(), textBox7.Text.Substring(1, textBox7.Text.Length - 1));
                }
            }

            if (textBox8.Text == "" || textBox12.Text == "" || textBox8.Text[0] == '-')
                label9.ForeColor = Color.Red;
            else
            {
                label9.ForeColor = Color.Black;
                if (textBox8.Text[0] >= 97 && textBox8.Text[0] <= 122)
                {
                    textBox8.Text = String.Concat(textBox8.Text[0].ToString().ToUpper(), textBox8.Text.Substring(1, textBox8.Text.Length - 1));
                }
            }

            if (textBox9.Text == "" || textBox9.Text[0] == '-' || textBox9.Text[1] == '-' || textBox9.Text[2] != '-' || textBox9.Text[3] == '-' || textBox9.Text[4] == '-' || textBox9.Text[5] == '-')
                label10.ForeColor = Color.Red;
            else
                label10.ForeColor = Color.Black;

            if (textBox10.Text == "" || textBox10.Text[0] == '-')
                label11.ForeColor = Color.Red;
            else
            {
                label11.ForeColor = Color.Black;
                if (textBox10.Text[0] >= 97 && textBox10.Text[0] <= 122)
                {
                    textBox10.Text = String.Concat(textBox10.Text[0].ToString().ToUpper(), textBox10.Text.Substring(1, textBox10.Text.Length - 1));
                }
            }
        }

        //dodanie nadawcy
        public void save_Nadawca()
        {
            Nadawcy nadawca = new Nadawcy();
            nadawca.Imie = textBox1.Text;
            nadawca.Nazwisko = textBox2.Text;
            nadawca.Ulica = textBox3.Text;
            nadawca.Nr_domu = textBox11.Text;
            nadawca.Kod_pocztowy = textBox4.Text;
            nadawca.Miasto = textBox5.Text;
            string s1 = @"INSERT INTO Nadawcy (Imie, Nazwisko, Ulica, Nr_domu, Kod_pocztowy, Miasto) VALUES ('" + nadawca.Imie + "','" + nadawca.Nazwisko + "','" + nadawca.Ulica + "','" + nadawca.Nr_domu + "','" + nadawca.Kod_pocztowy + "','" + nadawca.Miasto + "')";
            SqlCommand cmd = new SqlCommand(s1, connection);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //dodanie odbiorcy
        public void save_Odbiorca()
        {
            Odbiorcy odbiorca = new Odbiorcy();
            odbiorca.Imie = textBox6.Text;
            odbiorca.Nazwisko = textBox7.Text;
            odbiorca.Ulica = textBox8.Text;
            odbiorca.Nr_domu = textBox12.Text;
            odbiorca.Kod_pocztowy = textBox9.Text;
            odbiorca.Miasto = textBox10.Text;
            string s2 = @"INSERT INTO Odbiorcy (Imie, Nazwisko, Ulica, Nr_domu, Kod_pocztowy, Miasto) VALUES ('" + odbiorca.Imie + "','" + odbiorca.Nazwisko + "','" + odbiorca.Ulica + "','" + odbiorca.Nr_domu + "','" + odbiorca.Kod_pocztowy + "','" + odbiorca.Miasto + "')";
            SqlCommand cmd = new SqlCommand(s2, connection);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //dodanie przesyłki
        public void save_Przesylka()
        {
            Przesylki przesylka = new Przesylki();

            if (radioButton1.Checked)
	        {
                przesylka.Typ_przesylki = radioButton1.Text;
                przesylka.Nazwa = list.textbox1;
                przesylka.Metoda_przeslania = list.combobox1;
                przesylka.Waga = list.textbox2;
            }
            else if (radioButton2.Checked)
            {
                przesylka.Typ_przesylki = radioButton2.Text;
                przesylka.Nazwa = paczka.textbox1;
                przesylka.Metoda_przeslania = paczka.combobox1;
                przesylka.Waga = paczka.textbox2;
            }

            przesylka.Nadawca = textBox1.Text + " " + textBox2.Text;
            przesylka.Odbiorca = textBox6.Text + " " + textBox7.Text;
            przesylka.Data_nadania = label12.Text;
            przesylka.Status = "niedostarczono";

            string s1 = @"INSERT INTO Przesylki (Typ_przesylki, Nazwa, Metoda_przeslania, Waga, Nadawca, Odbiorca, Data_nadania, Status) VALUES ('" + przesylka.Typ_przesylki + "','" + przesylka.Nazwa + "','" + przesylka.Metoda_przeslania + "','" + przesylka.Waga + "','" + przesylka.Nadawca + "','" + przesylka.Odbiorca + "', '" + przesylka.Data_nadania + "', '" + przesylka.Status + "')";
            SqlCommand cmd = new SqlCommand(s1, connection);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                groupBox1.Visible = true;
                groupBox2.Visible = true;
                checkBox1.Visible = true;
                black_font();
                button1.Enabled = true;
                this.Controls.Remove(paczka);
                list.refresh();
                list.Top = 288;
                list.Left = 20;
                this.Controls.Add(list);
                list.textbox1 = "";
                list.combobox1 = "";
                clearing();
                checkBox1.Checked = false;
                list.Groupbox1 = true;
                paczka.Groupbox1 = true;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                groupBox1.Visible = true;
                groupBox2.Visible = true;
                checkBox1.Visible = true;
                black_font();
                button1.Enabled = true;
                this.Controls.Remove(list);
                paczka.refresh();
                paczka.Top = 288;
                paczka.Left = 20;
                this.Controls.Add(paczka);
                paczka.textbox1 = "";
                paczka.combobox1 = "";
                paczka.textbox2 = "";
                clearing();
                checkBox1.Checked = false;
                list.Groupbox1 = true;
                paczka.Groupbox1 = true;
            }
        }

        private void nowyFormularzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            radioButton1.Visible = true;
            radioButton2.Visible = true;
            button1.Visible = true;
            progressBar1.Visible = true;
        }

        private void kolorTłaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dlg_kolor.ShowDialog() == DialogResult.OK)
            {
                this.BackColor = dlg_kolor.Color;
            }
        }

        private void zamknijKreatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void zamknijProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dlg_czcionka.ShowDialog() == DialogResult.OK)
            {
                font();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer2.Start();
            // sprawdzenie, czy isteniej baza i połączenie
            try
            {
                if (!File.Exists(Path.Combine(Application.StartupPath, dbName + ".mdf")) && !File.Exists(Path.Combine(Application.StartupPath, dbName + ".ldf")))
                {
                    connection = new SqlConnection(CreateConnectionString);
                    connection.Open();

                    cmd = new SqlCommand(NewDataBase(), connection);
                    cmd.ExecuteNonQuery();

                    ConnectToDatabase();
                    
                    //tworzenie tabel i kolumn
                    //// Przesyłki
                    string s1 = @"CREATE TABLE Przesylki (Id int primary key identity, Typ_przesylki char(10), Nazwa char(50), Metoda_przeslania char(20), Waga char(10), Nadawca char (50), Odbiorca char(50), Data_nadania char(20), Status char(20))";
                    SqlCommand cmd1 = new SqlCommand(s1, connection);
                    cmd1.ExecuteNonQuery();

                    // Nadawcy
                    string s2 = @"CREATE TABLE Nadawcy (Id int primary key identity, Imie char(30), Nazwisko char(30), Ulica char(30), Nr_domu char(10), Kod_pocztowy char (6), Miasto char(30))";
                    SqlCommand cmd2 = new SqlCommand(s2, connection);
                    cmd2.ExecuteNonQuery();

                    // Odbiorcy
                    string s3 = @"CREATE TABLE Odbiorcy (Id int primary key identity, Imie char(30), Nazwisko char(30), Ulica char(30), Nr_domu char(10), Kod_pocztowy char (6), Miasto char(30))";
                    SqlCommand cmd3 = new SqlCommand(s3, connection);
                    cmd3.ExecuteNonQuery();

                    // Kurierzy
                    string s4 = @"CREATE TABLE Kurierzy (Id int primary key identity, Imie char(30), Nazwisko char(30), Wiek int, Pensja int, Numer_telefonu char(9))";
                    SqlCommand cmd4 = new SqlCommand(s4, connection);
                    cmd4.ExecuteNonQuery();

                    //dodanie kurierów
                    string s5 = @"INSERT INTO Kurierzy (Imie, Nazwisko, Wiek, Pensja, Numer_telefonu) VALUES
                    ('Jan', 'Nowak', 43, 2150, '500153964'), ('Kamil', 'Kowalski', 48, 2020, '510205643'), ('Wojciech', 'Byrdy', 50, 2500, '796534721'), ('Mateusz', 'Zawadzki', 36, 2300, '734526842'), 
                    ('Arkadiusz', 'Bednarz', 32, 1980, '531462879'), ('Krzysztof', 'Piekarczyk', 39, 2030, '789653214'), ('Hubert', 'Borkowski', 40, 2360, '733598621'), ('Bartłomiej', 'Słonka', 35, 1860, '763954103'), 
                    ('Artur', 'Turczyn', 50, 2000, '541203904'), ('Tomasz', 'Kwiatkowski', 38, 2460, '797955421'), ('Marian', 'Chlebowski', 41, 2310, '785552360'), ('Aleksander', 'Turczyn', 24, 1900, '596200143'), 
                    ('Zbigniew', 'Lesicki', 35, 2100, '755369201'), ('Piotr', 'Dudziak', 26, 1950, '763002007'), ('Artur', 'Wroński', 42, 2510, '788956201'), ('Ernest', 'Czyż', 39, 2390, '755201430')";
                    SqlCommand cmd5 = new SqlCommand(s5, connection);
                    cmd5.ExecuteNonQuery();

                    //dodanie nadawców
                    string s6 = @"INSERT INTO Nadawcy (Imie, Nazwisko, Ulica, Nr_domu, Kod_pocztowy, Miasto) VALUES
                    ('Monika', 'Olszewska', 'Jagiellończyka', 10, '66-400', 'Gorzów Wielkopolski'), ('Anna', 'Olszewska', 'Piotrkowska', 107, '90-001', 'Łódź'), ('Magdalena', 'Zalewska', 'Kołłątaja', 31, '87-100', 'Toruń'), 
                    ('Katarzyna', 'Zielona', 'Liliowa', 10, '43-300', 'Bielsko-Biała'), ('Paulina', 'Kalinowska', 'Gorczewska', 216, '01-464', 'Warszawa'), ('Marcin', 'Radwan', 'Wolnego', 14, '40-203', 'Katowice'), 
                    ('Arkadiusz', 'Bysko', 'Heweliusza', 11, '80-761', 'Gdańsk'), ('Patryk', 'Jackowski', 'Krakowska', 28, '45-309', 'Opole'), ('Zbyszek', 'Bartkowski', 'Głogowska', 31, '11-610', 'Poznań'), 
                    ('Daniel', 'Kanik', 'Bartoszewicza', 9, '01-464', 'Warszawa'), ('Krzysztof', 'Stelmaszczyk', 'Mickiewicza', 5, '34-100', 'Wadowice'), ('Olaf', 'Maciejewski', 'Rakoczego', 1, '80-761', 'Gdańsk'), 
                    ('Aleksandra', 'Zontek', 'Zwierzyniecka', 29, '31-403', 'Kraków'), ('Wanda', 'Morawska', 'Cechowa', 18, '43-300', 'Bielsko-Biała'), ('Janusz', 'Zaleski', 'Krótka', 5, '34-300', 'Żywiec'), 
                    ('Barbara', 'Szostak', 'Sobieskiego', 11, '40-203', 'Katowice'), ('Stanisław', 'Grzesiak', 'Wałowa', 32, '33-100', 'Tarnów'), ('Tomasz', 'Małkowski', 'Młyńska', 5, '11-610', 'Poznań'), 
                    ('Michał', 'Kołodziej', 'Sikorskiego', 42, '66-400', 'Gorzów Wielkopolski'), ('Joanna', 'Kubacka', 'Wodna', 17, '31-403', 'Kraków'), ('Mateusz', 'Szybiński', 'Szeroka', 22, '87-100', 'Toruń'), 
                    ('Włodzimierz', 'Pietraszko', 'Mostowa', 2, '43-300', 'Bielsko-Biała'), ('Hanna', 'Kaczmarczyk', 'Baczyńskiego', 7, '35-085', 'Rzeszów'), ('Sylwia', 'Kulik', 'Wolska', 94, '01-464', 'Warszawa')";
                    SqlCommand cmd6 = new SqlCommand(s6, connection);
                    cmd6.ExecuteNonQuery();

                    //dodanie odbiorców
                    string s7 = @"INSERT INTO Odbiorcy (Imie, Nazwisko, Ulica, Nr_domu, Kod_pocztowy, Miasto) VALUES
                    ('Karolina', 'Brzozowska', 'Kościuszki', 9, '34-500', 'Zakopane'), ('Elżbieta', 'Głowińska', 'Wąska', 2, '33-100', 'Tarnów'), ('Monika', 'Nalewka', 'Słowackiego', 3, '34-300', 'Żywiec'), 
                    ('Wojciech', 'Wolski', 'Fabryczna', 17, '78-400 ', 'Szczecinek'), ('Krystyna', 'Skiba', 'Karpacka', 24, '43-300', 'Bielsko-Biała'), ('Patryk', 'Waligóra', 'Katarzynki', 4, '80-761', 'Gdańsk'), 
                    ('Paweł', 'Kowalczyk', 'Towarowa', 16, '65-751', 'Zielona Góra'), ('Adam', 'Iwański', 'Wojtyły', 17, '34-100', 'Wadowice'), ('Bartłomiej', 'Orawski', 'Chłodna', 7, '16-400', 'Suwałki'), 
                    ('Alicja', 'Fornal', 'Kluczewska', 30, '41-303', 'Dąbrowa Górnicza'), ('Andrzej', 'Stańko', 'Pomorska', 62, '90-001', 'Łódź'), ('Mateusz', 'Opolski', 'Gołębia', 3, '31-403', 'Kraków'), 
                    ('Robert', 'Flisiński', 'Bankowa', 16, '87-100', 'Toruń'), ('Natalia', 'Chleboś', 'Zawady', 1, '11-610', 'Poznań'), ('Barbara', 'Niewiadoma', 'Widok', 16, '01-464', 'Warszawa'), 
                    ('Mirosław', 'Przypadło', 'Okopowa', 56, '01-464', 'Warszawa'), ('Aneta', 'Drewniak', 'Reymonta', 22, '01-464', 'Warszawa'), ('Agata', 'Mróz', 'Śląska', 35, '81-222', 'Gdynia'), 
                    ('Artur', 'Zięcina', 'Słoneczna', 4, '40-203', 'Katowice'), ('Karolina', 'Fulara', 'Gdowska', 35, '31-403', 'Kraków'), ('Joanna', 'Pytko', 'Waryńskiego', 8, '16-400', 'Suwałki'), 
                    ('Anna', 'Nowacka', 'Bema', 6, '41-303', 'Dąbrowa Górnicza'), ('Katarzyna', 'Rękawek', 'Hetmańska', 25, '11-610', 'Poznań'), ('Patrycja', 'Dagmarska', 'Chopina', 5, '81-704', 'Sopot')";
                    SqlCommand cmd7 = new SqlCommand(s7, connection);
                    cmd7.ExecuteNonQuery();

                    //dodanie przesyłek
                    string s8 = @"INSERT INTO Przesylki (Typ_przesylki, Nazwa, Metoda_przeslania, Waga, Nadawca, Odbiorca, Data_nadania, Status) VALUES 
                    ('List', 'niespodzianka', 'ekonomiczna', 0, 'Monika Olszewska', 'Karolina Brzozowska', '16 marca 2016', 'niedostarczono'), ('Paczka', 'część', 'polecona', 1, 'Anna Olszewska', 'Elżbieta Głowińska', '16 marca 2016', 'niedostarczono'), ('Paczka', 'niespodzianka', 'ekonomiczna', 1, 'Magdalena Zalewska', 'Monika Nalewka', '17 marca 2016', 'niedostarczono'), 
                    ('List', 'zwrot podatku', 'priorytetowa', 0, 'Katarzyna Zielona', 'Wojciech Wolski', '17 marca 2016', 'niedostarczono'), ('List', 'zwrot podatku', 'priorytetowa', 0, 'Paulina Kalinowska', 'Krystyna Skiba', '17 marca 2016', 'niedostarczono'), ('Paczka', 'imieniny', 'polecona', 2, 'Marcin Radwan', 'Patryk Waligóra', '18 marca 2016', 'niedostarczono'), 
                    ('Paczka', 'urodziny', 'kurierska', 3, 'Arkadiusz Bysko', 'Paweł Kowalczyk', '18 marca 2016', 'niedostarczono'), ('List', 'podanie', 'ekonomiczna', 0, 'Patryk Jackowski', 'Adam Iwański', '18 marca 2016', 'niedostarczono'), ('Paczka', 'rocznica', 'priorytetowa', 7, 'Zbyszek Bartkowski', 'Bartłomiej Orawski', '18 marca 2016', 'niedostarczono'), 
                    ('List', 'zwrot podatku', 'ekonomiczna', 0, 'Daniel Kanik', 'Alicja Fornal', '18 marca 2016', 'niedostarczono'), ('List', 'wycieczka', 'polecona', 0, 'Krzysztof Stelmaszczyk', 'Andrzej Stańko', '19 marca 2016', 'niedostarczono'), ('List', 'wiersz', 'kurierska', 0, 'Olaf Maciejewski', 'Mateusz Opolski', '19 marca 2016', 'niedostarczono'), 
                    ('Paczka', 'niespodzianka', 'polecona', 2, 'Aleksandra Zontek', 'Robert Flisiński', '20 marca 2016', 'niedostarczono'), ('Paczka', 'urodziny', 'ekonomiczna', 4, 'Wanda Morawska', 'Natalia Chleboś', '20 marca 2016', 'niedostarczono'), ('List', 'życzenia', 'polecona', 0, 'Janusz Zaleski', 'Barbara Niewiadoma', '20 marca 2016', 'niedostarczono'), 
                    ('Paczka', 'narzędzia', 'priorytetowa', 5, 'Barbara Szostak', 'Mirosław Przypadło', '21 marca 2016', 'niedostarczono'), ('List', 'sprawozdanie', 'priorytetowa', 0, 'Stanisław Grzesiak', 'Aneta Drewniak', '21 marca 2016', 'niedostarczono'), ('Paczka', 'niespodzianka', 'priorytetowa', 6, 'Tomasz Małkowski', 'Agata Mróz', '22 marca 2016', 'niedostarczono'), 
                    ('List', 'polecenie', 'polecona', 0, 'Michał Kołodziej', 'Artur Zięcina', '22 marca 2016', 'niedostarczono'), ('Paczka', 'auto-kom', 'kurierska', 5, 'Joanna Kubacka', 'Karolina Fulara', '22 marca 2016', 'niedostarczono'), ('List', 'życzenia', 'kurierska', 0, 'Mateusz Szybiński', 'Joanna Pytko', '22 marca 2016', 'niedostarczono'), 
                    ('List', 'podanie', 'priorytetowa', 0, 'Włodzimierz Pietraszko', 'Anna Nowacka', '23 marca 2016', 'niedostarczono'), ('List', 'wakacje', 'ekonomiczna', 0, 'Hanna Kaczmarczyk', 'Katarzyna Rękawek', '23 marca 2016', 'niedostarczono'), ('List', 'podanie', 'polecona', 0, 'Sylwia Kulik', 'Patrycja Dagmarska', '23 marca 2016', 'niedostarczono')";
                    SqlCommand cmd8 = new SqlCommand(s8, connection);
                    cmd8.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd!\n" + ex);
            }
            finally
            {
                Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConnectToDatabase();

            checking();

            int size_surname1 = textBox2.TextLength;
            int size_surname2 = textBox7.TextLength;

            if (radioButton1.Checked)
            {
                if (label2.ForeColor == Color.Red || label3.ForeColor == Color.Red || label4.ForeColor == Color.Red || label5.ForeColor == Color.Red || label6.ForeColor == Color.Red || label7.ForeColor == Color.Red || label8.ForeColor == Color.Red || label9.ForeColor == Color.Red || label10.ForeColor == Color.Red || label11.ForeColor == Color.Red || label12.ForeColor == Color.Red || list.Button_check == "Zatwierdź")
                {
                    MessageBox.Show("Zostawiłeś puste pola lub pola są uzupełnione niepoprawnie!!! \r\nUzupełnij je lub popraw.");
                }
                else if (label15.Text != "Połączono z bazą")
                {
                    MessageBox.Show("Brak połączenia z bazą danych");
                }
                else if (textBox2.Text[size_surname1 - 1] == '-')
                {
                    MessageBox.Show("Niepoprawny format nazwiska nadawcy.");
                }
                else if (textBox7.Text[size_surname2 - 1] == '-')
                {
                    MessageBox.Show("Niepoprawny format nazwiska odbiorcy.");
                }
                else
                {
                    timer1.Start();
                    save_Nadawca();
                    save_Odbiorca();
                    save_Przesylka();
                    checkBox1.Checked = false;
                }
            }
            else if (radioButton2.Checked)
            {
                if (label2.ForeColor == Color.Red || label3.ForeColor == Color.Red || label4.ForeColor == Color.Red || label5.ForeColor == Color.Red || label6.ForeColor == Color.Red || label7.ForeColor == Color.Red || label8.ForeColor == Color.Red || label9.ForeColor == Color.Red || label10.ForeColor == Color.Red || label11.ForeColor == Color.Red || label12.ForeColor == Color.Red || paczka.Button_check == "Zatwierdź")
                {
                    MessageBox.Show("Zostawiłeś puste pola lub pola są uzupełnione niepoprawnie!!! \r\nUzupełnij je lub popraw.");
                }
                else if (label15.Text != "Połączono z bazą")
                {
                    MessageBox.Show("Brak połączenia z bazą danych");
                }
                else if (textBox2.Text[size_surname1 - 1] == '-')
                {
                    MessageBox.Show("Niepoprawny format nazwiska nadawcy.");
                }
                else if (textBox7.Text[size_surname2 - 1] == '-')
                {
                    MessageBox.Show("Niepoprawny format nazwiska odbiorcy.");
                }
                else
                {
                    timer1.Start();
                    save_Nadawca();
                    save_Odbiorca();
                    save_Przesylka();
                    checkBox1.Checked = false;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.PerformStep();

            if (progressBar1.Value == progressBar1.Maximum)
            {
                progressBar1.Value = 0;
                timer1.Stop();
                MessageBox.Show("Zlecenie wysłane.");
                clearing();
                list.Groupbox1 = true;
                paczka.Groupbox1 = true;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label12.Text = DateTime.Now.ToLongDateString();
            label13.Text = DateTime.Now.ToLongTimeString();
        }

        private void restartKreatoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void oProgramieToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Program do generowania zleceń na wysyłki listów i paczek.\r\nWykonał: Mateusz Przybyło\r\nŚrodowisko: Visual Studio 2013 C#\r\nRok: 2015/2016");
        }

        // Połączenie z bazą danych
        private void bazaDanychToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectToDatabase();
        }

        private void bazęDanychToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Zmienna.czy_form2 == false)
            {
                Form2 frm = new Form2();
                frm.Show();
                frm.Location = new Point(350, 200);
                Zmienna.czy_form2 = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Anna";
            textBox2.Text = "Zawadzka";
            textBox3.Text = "Polna";
            textBox11.Text = "60";
            textBox4.Text = "43-300";
            textBox5.Text = "Bielsko-Biała";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Marcin";
            textBox2.Text = "Nowakowski";
            textBox3.Text = "Ogarna";
            textBox11.Text = "85";
            textBox4.Text = "80-761";
            textBox5.Text = "Gdańsk";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Agata";
            textBox2.Text = "Walas";
            textBox3.Text = "Nowowiejska";
            textBox11.Text = "5";
            textBox4.Text = "01-464";
            textBox5.Text = "Warszawa";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Mateusz";
            textBox2.Text = "Kanikowski";
            textBox3.Text = "Szpitalna";
            textBox11.Text = "13";
            textBox4.Text = "31-403";
            textBox5.Text = "Kraków";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Wojciech";
            textBox2.Text = "Ochocki";
            textBox3.Text = "Kośnego";
            textBox11.Text = "8";
            textBox4.Text = "45-309";
            textBox5.Text = "Opole";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Elżbieta";
            textBox2.Text = "Czaniecka";
            textBox3.Text = "Dyrekcyjna";
            textBox11.Text = "1";
            textBox4.Text = "40-203";
            textBox5.Text = "Katowice";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            textBox6.Text = "Elwira";
            textBox7.Text = "Bosak";
            textBox8.Text = "Zatorska";
            textBox12.Text = "10";
            textBox9.Text = "34-100";
            textBox10.Text = "Wadowice";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBox6.Text = "Arkadiusz";
            textBox7.Text = "Pytlarz";
            textBox8.Text = "Drewnowska";
            textBox12.Text = "9";
            textBox9.Text = "90-001";
            textBox10.Text = "Łódź";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox6.Text = "Katarzyna";
            textBox7.Text = "Wasiak";
            textBox8.Text = "Lompy";
            textBox12.Text = "7";
            textBox9.Text = "43-300";
            textBox10.Text = "Bielsko-Biała";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox6.Text = "Szymon";
            textBox7.Text = "Bakar";
            textBox8.Text = "Kopernika";
            textBox12.Text = "11";
            textBox9.Text = "40-203";
            textBox10.Text = "Katowice";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox6.Text = "Stanisław";
            textBox7.Text = "Opolski";
            textBox8.Text = "Grodzka";
            textBox12.Text = "6";
            textBox9.Text = "31-403";
            textBox10.Text = "Kraków";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox6.Text = "Karol";
            textBox7.Text = "Żurawski";
            textBox8.Text = "Grzybowska";
            textBox12.Text = "73";
            textBox9.Text = "01-464";
            textBox10.Text = "Warszawa";
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

        //pozwolenia na znaki wpisywanie do poszczególnych textboxów
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = !(Char.IsNumber(e.KeyChar) || e.KeyChar == '-' || e.KeyChar == 8))
            {
                e.Handled = true;
            }
            // tylko jeden myślnik w polu
            else if (e.KeyChar == '-' && (sender as TextBox).Text.Contains('-'))
            {
                e.Handled = true;
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = !(Char.IsNumber(e.KeyChar) || e.KeyChar == '-' || e.KeyChar == 8))
            {
                e.Handled = true;
            }
            // tylko jeden myślnik w polu
            else if (e.KeyChar == '-' && (sender as TextBox).Text.Contains('-'))
            {
                e.Handled = true;
            }
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = !(Char.IsNumber(e.KeyChar) || e.KeyChar == '/' || e.KeyChar == 8))
            {
                e.Handled = true;
            }
            // tylko jeden ukośnik w polu
            else if (e.KeyChar == '/' && (sender as TextBox).Text.Contains('/'))
            {
                e.Handled = true;
            }
        }

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = !(Char.IsNumber(e.KeyChar) || e.KeyChar == '/' || e.KeyChar == 8))
            {
                e.Handled = true;
            }
            // tylko jeden ukośnik w polu
            else if (e.KeyChar == '/' && (sender as TextBox).Text.Contains('/'))
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

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = !(Char.IsLetter(e.KeyChar) || e.KeyChar == 8))
            {
                e.Handled = true;
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
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

        private void kurierówObecnychWPracyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Zmienna.czy_form6 == false)
            {
                Form6 form6 = new Form6();
                form6.Show();
                form6.Location = new Point(500, 200);
                Zmienna.czy_form6 = true;
            }
        }

        private void mapęToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Zmienna.czy_form7 == false)
            {
                Form7 form7 = new Form7();
                form7.Show();
                form7.Location = new Point(350, 170);
                Zmienna.czy_form7 = true;
            }
        }

        private void bazęMiastIKodówPocztowychToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Zmienna.czy_form8 == false)
            {
                Form8 form8 = new Form8();
                form8.Show();
                form8.Location = new Point(520, 180);
                Zmienna.czy_form8 = true;
            }
        }

        private void notatkaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Zmienna.czy_form9 == false)
            {
                Form9 form9 = new Form9();
                form9.Show();
                form9.Location = new Point(400, 280);
                Zmienna.czy_form9 = true;
            }
        }

        private void raportyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Zmienna.czy_form10 == false)
            {
                Form10 form10 = new Form10();
                form10.Show();
                form10.Location = new Point(350, 170);
                Zmienna.czy_form10 = true;
            }
        }
    }
}