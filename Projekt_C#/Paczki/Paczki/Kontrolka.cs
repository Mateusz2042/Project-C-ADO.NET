using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paczki
{
    public partial class Kontrolka : UserControl
    {
        public Kontrolka()
        {
            InitializeComponent();
        }

        public string textbox1
        {
            set { textBox1.Text = value; }
            get { return textBox1.Text; }
        }

        public string combobox1
        {
            set { comboBox1.Text = value; }
            get { return comboBox1.Text; }
        }

        public string textbox2
        {
            set { textBox2.Text = value; }
            get { return textBox2.Text; }
        }

        public string Label2
        {
            set { label2.Text = value; }
            get { return label2.Text; }
        }

        public string Label3
        {
            set { label3.Text = value; }
            get { return label3.Text; }
        }

        public string Label4
        {
            set { label4.Text = value; }
            get { return label4.Text; }
        }

        public string Button1
        {
            set { button1.Text = value; }
            get { return button1.Text; }
        }

        public bool Groupbox1
        {
            set { groupBox1.Enabled = value; }
            get { return groupBox1.Enabled; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && !string.IsNullOrEmpty(comboBox1.Text) && textBox2.Text != "" && textBox2.Text[0] != ',')
            {
                button1.Text = "Zatwierdzone";
                groupBox1.Enabled = false;
            }

            if (textBox1.Text == "")
                label2.ForeColor = Color.Red;
            else
            {
                label2.ForeColor = Color.Black;
            }

            if (string.IsNullOrEmpty(comboBox1.Text))
                label3.ForeColor = Color.Red;
            else
            {
                label3.ForeColor = Color.Black;
            }

            if (textBox2.Text == "" || textBox2.Text[0] == ',')
                label4.ForeColor = Color.Red;
            else
            {
                label4.ForeColor = Color.Black;
            }
        }

        public void refresh()
        {
            label2.ForeColor = Color.Black;
            label3.ForeColor = Color.Black;
            label4.ForeColor = Color.Black;
            button1.BackColor = Color.White;
            button1.Text = "Zatwierdź";
        }

        public string Button_check
        {
            set { button1.Text = value; }
            get { return button1.Text; }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
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
