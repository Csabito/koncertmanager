using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace koncertmanager
{
    public partial class Form1 : Form
    {
        private Label lblTitle;
        private Button btnAddConcert;
        private TextBox txtSearch;
        private NumericUpDown numPriceFilter;
        private ComboBox cmbGenre;
        private ListView lstConcerts;
        public Form1()
        {
            InitializeComponent();
            DrawElements();
            LoadConcerts();
            btnAddConcert.Click += (sender, e) => addConcert();
        }
        public void DrawElements()
        {
            // 1. Form Settings
            this.Text = "Concert Manager Pro";
            this.Size = new Size(800, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // 2. Title Label
            lblTitle = new Label()
            {
                Text = "Concert Dashboard",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // 3. Top Filter Panel (to hold buttons, textboxes, etc.)
            FlowLayoutPanel filterPanel = new FlowLayoutPanel()
            {
                Dock = DockStyle.Top,
                Height = 60,
                Padding = new Padding(10),
                BackColor = Color.LightGray
            };

            // 4. Search TextBox
            txtSearch = new TextBox() { Width = 150 };

            // 5. Add Button
            btnAddConcert = new Button() { Text = "Add Concert", Width = 100, BackColor = Color.LightGreen };

            // 6. Price Filter (NumericUpDown acts as ButtonUp/ButtonDown)
            Label lblPrice = new Label() { Text = "Max Price:", AutoSize = true, Margin = new Padding(10, 5, 0, 0) };
            numPriceFilter = new NumericUpDown() { Maximum = 5000, Value = 100, Width = 80 };

            // 7. Genre ComboBox
            cmbGenre = new ComboBox() { DropDownStyle = ComboBoxStyle.DropDownList, Width = 120 };
            cmbGenre.Items.AddRange(new string[] { "All Genres", "Rock", "Pop", "Jazz", "Metal", "Classical" });
            cmbGenre.SelectedIndex = 0;

            // 8. ListView for Concerts
            lstConcerts = new ListView()
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true,
                GridLines = true
            };
            lstConcerts.Columns.Add("Concert Name", 250);
            lstConcerts.Columns.Add("Genre", 150);
            lstConcerts.Columns.Add("Price", 100);
            lstConcerts.Columns.Add("Date", 150);

            // Add controls to the Filter Panel
            filterPanel.Controls.Add(btnAddConcert);
            filterPanel.Controls.Add(new Label() { Text = "  |  ", AutoSize = true }); // Visual separator
            filterPanel.Controls.Add(txtSearch);
            filterPanel.Controls.Add(lblPrice);
            filterPanel.Controls.Add(numPriceFilter);
            filterPanel.Controls.Add(cmbGenre);

            // Add everything to the Form
            this.Controls.Add(lstConcerts);
            this.Controls.Add(filterPanel);
            this.Controls.Add(lblTitle);
        }
        private void addConcert()
        {
            Form2 form2 = new Form2();
            form2.Show();
            Hide();
        }
        static List<Koncert> concerts = new List<Koncert>();
        private void LoadConcerts()
        {
            //add each concert into the listview
            foreach (var item in concerts)
            {
                lstConcerts.Items.Add(new ListViewItem(new string[] { item.Knev, item.Eloadas, item.Jegyar.ToString(), item.Idopont.ToString("yyyy-MM-dd") }));

            }
        }
    }
}
