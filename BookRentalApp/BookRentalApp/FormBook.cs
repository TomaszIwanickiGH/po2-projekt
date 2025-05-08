using BookRentalApp.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BookRentalApp
{
    public partial class FormBook : Form
    {
        private readonly AppDbContext _context = new AppDbContext();

        private TextBox txtTitle;
        private TextBox txtAuthor;
        private TextBox txtGenre;
        private CheckBox chkAvailable;
        private Button btnSave;
        //private ErrorProvider errorProvider1;

        public FormBook()
        {
            InitializeComponent();
            InitializeUI();
        }

        private void InitializeUI()
        {
            this.Text = "Dodaj książkę";
            this.Size = new Size(400, 400);
            this.MinimumSize = new Size(400, 400);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;

            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 5,
                Padding = new Padding(20),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            for (int i = 0; i < 4; i++)
                mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F)); // Przycisk

            // Tytuł
            var lblTitle = new Label
            {
                Text = "Tytuł:",
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill
            };
            txtTitle = new TextBox { Dock = DockStyle.Fill };
            mainLayout.Controls.Add(lblTitle, 0, 0);
            mainLayout.Controls.Add(txtTitle, 1, 0);

            // Autor
            var lblAuthor = new Label
            {
                Text = "Autor:",
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill
            };
            txtAuthor = new TextBox { Dock = DockStyle.Fill };
            mainLayout.Controls.Add(lblAuthor, 0, 1);
            mainLayout.Controls.Add(txtAuthor, 1, 1);

            // Gatunek
            var lblGenre = new Label
            {
                Text = "Gatunek:",
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill
            };
            txtGenre = new TextBox { Dock = DockStyle.Fill };
            mainLayout.Controls.Add(lblGenre, 0, 2);
            mainLayout.Controls.Add(txtGenre, 1, 2);

            // Dostępność
            var lblAvailable = new Label
            {
                Text = "Dostępna:",
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill
            };
            chkAvailable = new CheckBox { Dock = DockStyle.Left };
            mainLayout.Controls.Add(lblAvailable, 0, 3);
            mainLayout.Controls.Add(chkAvailable, 1, 3);

            // ErrorProvider
            errorProvider1 = new ErrorProvider();

            // Przycisk Zapisz
            btnSave = new Button
            {
                Text = "Zapisz",
                Dock = DockStyle.Fill,
                Height = 40,
                BackColor = Color.Teal,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSave.Click += btnSave_Click;
            mainLayout.Controls.Add(btnSave, 0, 4);
            mainLayout.SetColumnSpan(btnSave, 2);

            this.Controls.Add(mainLayout);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();

            if (!ValidateInputs()) return;

            try
            {
                var book = new Book
                {
                    Title = txtTitle.Text.Trim(),
                    Author = txtAuthor.Text.Trim(),
                    Genre = txtGenre.Text.Trim(),
                    IsAvailable = chkAvailable.Checked
                };

                _context.Books.Add(book);
                _context.SaveChanges();

                MessageBox.Show("Książka zapisana.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd zapisu: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        private bool ValidateInputs()
        {
            bool valid = true;

            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                errorProvider1.SetError(txtTitle, "Tytuł jest wymagany");
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(txtAuthor.Text))
            {
                errorProvider1.SetError(txtAuthor, "Autor jest wymagany");
                valid = false;
            }

            return valid;
        }
    }
}
