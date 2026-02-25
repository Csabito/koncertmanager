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

namespace koncertmanager
{
    public partial class Form2 : Form
    {
        private TextBox nev, eloado, helyszin;
        private ComboBox mufaj, szinpad;
        private DateTimePicker idopont;
        private NumericUpDown jegyar;
        private Button mentesGomb, visszaGomb;

        public Form2()
        {
            InitializeComponent();
            Felepites();
            mentesGomb.Click += (sender, e) => Ment();
            visszaGomb.Click += (sender, e) => Close();
        }

        private void Felepites()
        {
            this.Text = "Új koncert hozzáadása";
            this.Size = new Size(400, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            var tabla = new TableLayoutPanel()
            {
                Location = new Point(10, 10),
                Size = new Size(365, 300),
                ColumnCount = 2,
                RowCount = 7
            };

            tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 115));
            tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            for (int i = 0; i < 7; i++)
                tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

            Label Cimke(string szoveg) => new Label()
            {
                Text = szoveg,
                AutoSize = true,
                Margin = new Padding(0, 10, 0, 0)
            };

            nev = new TextBox()
            {
                Dock = DockStyle.Fill
            };

            eloado = new TextBox()
            {
                Dock = DockStyle.Fill
            };

            helyszin = new TextBox()
            {
                Dock = DockStyle.Fill
            };

            mufaj = new ComboBox()
            {
                DropDownStyle = ComboBoxStyle.DropDown,
                Dock = DockStyle.Fill
            };
            mufaj.Items.AddRange(new string[]
            {
                "Rock", "Pop", "Jazz", "Metal",
                "Classical", "Electronic", "Hip-Hop", "Egyéb"
            });
            mufaj.SelectedIndex = 0;

            szinpad = new ComboBox()
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Dock = DockStyle.Fill
            };
            szinpad.Items.AddRange(new string[]
            {
                "Small", "Medium", "Large"
            });
            szinpad.SelectedIndex = 0;

            idopont = new DateTimePicker()
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "yyyy-MM-dd HH:mm",
                Dock = DockStyle.Fill
            };

            jegyar = new NumericUpDown()
            {
                Minimum = 0,
                Maximum = 1000000,
                Value = 5000,
                Dock = DockStyle.Fill
            };

            tabla.Controls.Add(Cimke("Koncert neve:"), 0, 0);
            tabla.Controls.Add(nev, 1, 0);
            tabla.Controls.Add(Cimke("Előadó:"), 0, 1);
            tabla.Controls.Add(eloado, 1, 1);
            tabla.Controls.Add(Cimke("Műfaj:"), 0, 2);
            tabla.Controls.Add(mufaj, 1, 2);
            tabla.Controls.Add(Cimke("Helyszín:"), 0, 3);
            tabla.Controls.Add(helyszin, 1, 3);
            tabla.Controls.Add(Cimke("Szinpad:"), 0, 4);
            tabla.Controls.Add(szinpad, 1, 4);
            tabla.Controls.Add(Cimke("Időpont:"), 0, 5);
            tabla.Controls.Add(idopont, 1, 5);
            tabla.Controls.Add(Cimke("Jegyár (Ft):"), 0, 6);
            tabla.Controls.Add(jegyar, 1, 6);

            mentesGomb = new Button()
            {
                Text = "Mentés",
                Location = new Point(185, 320),
                Width = 85,
                Height = 28
            };

            visszaGomb = new Button()
            {
                Text = "Vissza",
                Location = new Point(280, 320),
                Width = 85,
                Height = 28
            };

            this.Controls.Add(tabla);
            this.Controls.Add(mentesGomb);
            this.Controls.Add(visszaGomb);
        }

        private void Ment()
        {
            if (string.IsNullOrWhiteSpace(nev.Text))
            {
                MessageBox.Show("A koncert neve nem lehet üres!");
                return;
            }
            if (string.IsNullOrWhiteSpace(eloado.Text))
            {
                MessageBox.Show("Az előadó neve nem lehet üres!");
                return;
            }
            if (string.IsNullOrWhiteSpace(helyszin.Text))
            {
                MessageBox.Show("A helyszín nem lehet üres!");
                return;
            }

            var ujKoncert = new KoncertFeldolg(
                nev.Text.Trim(),
                eloado.Text.Trim(),
                mufaj.SelectedItem.ToString(),
                helyszin.Text.Trim(),
                szinpad.SelectedItem.ToString(),
                idopont.Value,
                (int)jegyar.Value
            );

            File.AppendAllText(
                "concerts.txt",
                ujKoncert.ToFileString() + Environment.NewLine,
                Encoding.UTF8
            );

            MessageBox.Show("Koncert mentve!");
            Form1 form1 = new Form1();
            form1.Show();
            Close();
        }
    }
}

