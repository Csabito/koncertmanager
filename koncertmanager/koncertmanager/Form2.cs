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
            this.Size = new Size(460, 560);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(240, 240, 248);
            this.Font = new Font("Segoe UI", 9f);

            var header = new Panel()
            {
                Dock = DockStyle.Top,
                Height = 64,
                BackColor = Color.FromArgb(22, 22, 40)
            };

            var headerLabel = new Label()
            {
                Text = "   Új Koncert Hozzáadása",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 13f, FontStyle.Bold),
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0)
            };
            header.Controls.Add(headerLabel);

            var card = new Panel()
            {
                Location = new Point(20, 84),
                Size = new Size(410, 400),
                BackColor = Color.White,
                Padding = new Padding(15)
            };
            card.Paint += (s, e) =>
            {
                using (var pen = new Pen(Color.FromArgb(210, 210, 230)))
                {
                    e.Graphics.DrawRectangle(pen,
                        0, 0, card.Width - 1, card.Height - 1);
                }
            };

            var tabla = new TableLayoutPanel()
            {
                Location = new Point(15, 12),
                Size = new Size(378, 374),
                ColumnCount = 2,
                RowCount = 7,
                BackColor = Color.White
            };

            tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 118));
            tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            for (int i = 0; i < 7; i++)
                tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));

            Label Cimke(string szoveg) => new Label()
            {
                Text = szoveg,
                AutoSize = false,
                Dock = DockStyle.Fill,
                ForeColor = Color.FromArgb(100, 100, 125),
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };

            var inputBg = Color.FromArgb(248, 248, 253);
            var inputFont = new Font("Segoe UI", 9.5f);

            nev = new TextBox()
            {
                Dock = DockStyle.Fill,
                Font = inputFont,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = inputBg
            };

            eloado = new TextBox()
            {
                Dock = DockStyle.Fill,
                Font = inputFont,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = inputBg
            };

            helyszin = new TextBox()
            {
                Dock = DockStyle.Fill,
                Font = inputFont,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = inputBg
            };

            mufaj = new ComboBox()
            {
                DropDownStyle = ComboBoxStyle.DropDown,
                Dock = DockStyle.Fill,
                Font = inputFont,
                BackColor = inputBg,
                FlatStyle = FlatStyle.Flat
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
                Dock = DockStyle.Fill,
                Font = inputFont,
                BackColor = inputBg,
                FlatStyle = FlatStyle.Flat
            };
            szinpad.Items.AddRange(new string[]
            {
                "Small", "Medium", "Large"
            });
            szinpad.SelectedIndex = 0;

            idopont = new DateTimePicker()
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "yyyy-MM-dd",
                Dock = DockStyle.Fill,
                Font = inputFont,
                CalendarTitleBackColor = Color.FromArgb(22, 22, 40),
                CalendarTitleForeColor = Color.White,
                CalendarForeColor = Color.FromArgb(30, 30, 50)
            };

            jegyar = new NumericUpDown()
            {
                Minimum = 0,
                Maximum = 1000000,
                Value = 5000,
                Increment = 100,
                Dock = DockStyle.Fill,
                Font = inputFont,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = inputBg,
                ThousandsSeparator = true
            };

            tabla.Controls.Add(Cimke("Concert Name:"), 0, 0);
            tabla.Controls.Add(nev, 1, 0);
            tabla.Controls.Add(Cimke("Artist:"), 0, 1);
            tabla.Controls.Add(eloado, 1, 1);
            tabla.Controls.Add(Cimke("Genre:"), 0, 2);
            tabla.Controls.Add(mufaj, 1, 2);
            tabla.Controls.Add(Cimke("Location:"), 0, 3);
            tabla.Controls.Add(helyszin, 1, 3);
            tabla.Controls.Add(Cimke("Stage:"), 0, 4);
            tabla.Controls.Add(szinpad, 1, 4);
            tabla.Controls.Add(Cimke("Date:"), 0, 5);
            tabla.Controls.Add(idopont, 1, 5);
            tabla.Controls.Add(Cimke("Price (HUF):"), 0, 6);
            tabla.Controls.Add(jegyar, 1, 6);

            card.Controls.Add(tabla);

            mentesGomb = new Button()
            {
                Text = "💾  Mentés",
                Location = new Point(215, 500),
                Width = 110,
                Height = 36,
                BackColor = Color.FromArgb(22, 22, 40),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            mentesGomb.FlatAppearance.BorderSize = 0;
            mentesGomb.FlatAppearance.MouseOverBackColor = Color.FromArgb(55, 55, 90);

            visszaGomb = new Button()
            {
                Text = "  Vissza",
                Location = new Point(333, 500),
                Width = 97,
                Height = 36,
                BackColor = Color.FromArgb(208, 208, 224),
                ForeColor = Color.FromArgb(40, 40, 65),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5f),
                Cursor = Cursors.Hand
            };
            visszaGomb.FlatAppearance.BorderSize = 0;
            visszaGomb.FlatAppearance.MouseOverBackColor = Color.FromArgb(185, 185, 205);

            this.Controls.Add(header);
            this.Controls.Add(card);
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

            KoncertManager manager = new KoncertManager();
            manager.UjKoncert(
                nev.Text.Trim(),
                helyszin.Text.Trim(),
                szinpad.SelectedItem.ToString(),
                idopont.Value,
                eloado.Text.Trim(),
                mufaj.SelectedItem.ToString(),
                (int)jegyar.Value
            );

            MessageBox.Show("Koncert mentve!");
            Form1 form1 = new Form1();
            form1.Show();
            Close();
        }
    }
}
