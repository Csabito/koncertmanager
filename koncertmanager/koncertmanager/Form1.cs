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
        private Button btnAddConcert, btnSearchConcert, btnDeleteConcert;
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
            btnSearchConcert.Click += (sender, e) => SearchConcerts();
            btnDeleteConcert.Click += (sender, e) => DeleteConcerts();
        }
        public void DrawElements()
        {
            // 1. Form Settings
            this.Text = "Concert Manager Pro";
            this.Size = new Size(840, 500);
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
            btnSearchConcert = new Button() { Text = "Search Concert", Width = 100, BackColor = Color.LightBlue };
            btnDeleteConcert = new Button() { Text = "Delete Concert", Width = 100, BackColor = Color.Red };

            // 6. Price Filter (NumericUpDown acts as ButtonUp/ButtonDown)
            Label lblPrice = new Label() { Text = "Max Price:", AutoSize = true, Margin = new Padding(10, 5, 0, 0) };
            numPriceFilter = new NumericUpDown() { Maximum = 50000, Value = 5000, Width = 80, Increment = 100 };

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
            lstConcerts.Columns.Add("Concert Name", 120);
            lstConcerts.Columns.Add("Location", 120);
            lstConcerts.Columns.Add("Size of Location", 120);
            lstConcerts.Columns.Add("Előadó", 120);
            lstConcerts.Columns.Add("Genre", 120);
            lstConcerts.Columns.Add("Price", 120);
            lstConcerts.Columns.Add("Date", 120);

            // Add controls to the Filter Panel
            filterPanel.Controls.AddRange(new Control[]
            {
                btnAddConcert,
                new Label() {Text = "|", AutoSize = true},
                txtSearch,
                lblPrice,
                numPriceFilter,
                cmbGenre,
                btnSearchConcert,
                btnDeleteConcert
            });
            //filterPanel.Controls.Add(btnAddConcert);
            //filterPanel.Controls.Add(new Label() { Text = "  |  ", AutoSize = true }); // Visual separator
            //filterPanel.Controls.Add(txtSearch);
            //filterPanel.Controls.Add(lblPrice);
            //filterPanel.Controls.Add(numPriceFilter);
            //filterPanel.Controls.Add(cmbGenre);
            //filterPanel.Controls.Add(btnSearchConcert);

            // Add everything to the Form
            this.Controls.Add(lstConcerts);
            this.Controls.Add(filterPanel);
            this.Controls.Add(lblTitle);
        }
        private void addConcert()
        {
            Form2 form2 = new Form2();
            form2.Show();
        }
        static KoncertManager manager = new KoncertManager();
        public void LoadConcerts()
        {
            manager.KoncertBeolvas();
            manager.FillListView(lstConcerts);
        }
        private void SearchConcerts()
        {
            manager.FilterConcerts(lstConcerts, txtSearch.Text, (int)numPriceFilter.Value, cmbGenre.SelectedItem.ToString());
        }
        private void DeleteConcerts()
        {
            if (lstConcerts.SelectedItems.Count > 0)
            {
                manager.DeleteConcert(lstConcerts);
                MessageBox.Show("Concert deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadConcerts();
            }
            else
            {
                MessageBox.Show("Please select a concert to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
    }
}
