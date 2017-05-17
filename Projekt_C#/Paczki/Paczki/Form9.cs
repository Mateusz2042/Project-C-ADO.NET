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

namespace Paczki
{
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }

        private void nowaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void otwórzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Title = "Otwieranie pliku";
            dlg.Filter = "Pliki tekstowe|*.txt|Wszystkie pliki|*.*";
            dlg.FilterIndex = 1;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                StreamReader read = File.OpenText(dlg.FileName);

                textBox1.Text = read.ReadToEnd();
                read.Close();
            }
        }

        private void zapiszToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();

            dlg.Title = "Zapisywanie pliku";
            dlg.Filter = "Pliki tekstowe|*.txt|Wszystkie pliki|*.*";
            dlg.FilterIndex = 1;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                StreamWriter write = new StreamWriter(dlg.FileName);

                write.Write(textBox1.Text);
                write.Close();
            }
        }

        private void koniecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                DialogResult dr = MessageBox.Show("Czy chcesz zapisać notatkę?",
                      "Czy...?", MessageBoxButtons.YesNoCancel);
                switch (dr)
                {
                    case DialogResult.Yes:
                        SaveFileDialog dlg = new SaveFileDialog();

                        dlg.Title = "Zapisywanie pliku";
                        dlg.Filter = "Pliki tekstowe|*.txt|Wszystkie pliki|*.*";
                        dlg.FilterIndex = 1;

                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            StreamWriter write = new StreamWriter(dlg.FileName);

                            write.Write(textBox1.Text);
                            write.Close();
                        }
                        break;

                    case DialogResult.No:
                        this.Close();
                        Zmienna.czy_form9 = false;
                        break;

                    case DialogResult.Cancel:
                        break;
                }
            }
            else
            {
                this.Close();
                Zmienna.czy_form9 = false;
            }
        }

        private void cofnijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.CanUndo == true)
            {
                textBox1.Undo();

                textBox1.ClearUndo();
            }
        }

        private void wytnijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectedText != "")
            {
                string text = textBox1.SelectedText;
                textBox1.Cut();
                textBox2.Text = text;
            }
        }

        private void kopiujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength > 0)
            {
                string text = textBox1.SelectedText;
                textBox1.Copy();
                textBox2.Text = text;
            }
        }

        private void wklejToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text) == true && textBox2.TextLength <= Convert.ToInt32(label1.Text))
            {
                textBox1.Paste();
            }
        }

        private void wstawDatęIGodzinęToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = DateTime.Now.ToString();
        }

        private void zaznaczWszystkoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void czcionkaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog dlg = new FontDialog();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox1.Font = dlg.Font;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                DialogResult dr = MessageBox.Show("Czy chcesz zapisać notatkę?",
                      "Czy...?", MessageBoxButtons.YesNoCancel);
                switch (dr)
                {
                    case DialogResult.Yes:
                        SaveFileDialog dlg = new SaveFileDialog();

                        dlg.Title = "Zapisywanie pliku";
                        dlg.Filter = "Pliki tekstowe|*.txt|Wszystkie pliki|*.*";
                        dlg.FilterIndex = 1;

                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            StreamWriter write = new StreamWriter(dlg.FileName);

                            write.Write(textBox1.Text);
                            write.Close();
                        }
                        break;

                    case DialogResult.No:
                        this.Close();
                        Zmienna.czy_form9 = false;
                        break;

                    case DialogResult.Cancel:
                        break;
                }
            }
            else
            {
                this.Close();
                Zmienna.czy_form9 = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if ((700 - textBox1.TextLength) >= 0)
            {
                label1.Text = (700 - textBox1.TextLength).ToString();
            }
            else
	        {
                label1.Text = "0";
	        }

            if (label1.Text == "0")
            {
                string s = "";
                for (int i = 0; i < 700; i++)
                {
                    s += textBox1.Text[i];
                }
                textBox1.Text = s;
                s = "";
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(label1.Text) <= 0)
            {
                if (e.KeyChar != 8)
                {
                    e.Handled = true;   
                }
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && e.Modifiers == Keys.Control)
            {
                textBox1.SelectAll();
            }
        }
    }
}
