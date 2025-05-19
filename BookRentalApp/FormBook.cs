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
            this.Size = new Size(380, 270);
            this.MinimumSize = new Size(380, 270);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.BackColor = Color.White;

            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 5,
                Padding = new Padding(15),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));

            // Label: Tytuł
            var lblTitle = new Label
            {
                Text = "Tytuł:",
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10)
            };
            txtTitle = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10)
            };
            mainLayout.Controls.Add(lblTitle, 0, 0);
            mainLayout.Controls.Add(txtTitle, 1, 0);

            // Label: Autor
            var lblAuthor = new Label
            {
                Text = "Autor:",
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10)
            };
            txtAuthor = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10)
            };
            mainLayout.Controls.Add(lblAuthor, 0, 1);
            mainLayout.Controls.Add(txtAuthor, 1, 1);

            // Label: Gatunek
            var lblGenre = new Label
            {
                Text = "Gatunek:",
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10)
            };
            txtGenre = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10)
            };
            mainLayout.Controls.Add(lblGenre, 0, 2);
            mainLayout.Controls.Add(txtGenre, 1, 2);

            // Label: Dostępna
            var lblAvailable = new Label
            {
                Text = "Dostępna:",
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10)
            };
            chkAvailable = new CheckBox
            {
                Dock = DockStyle.Left,
                Checked = true // Domyślnie zaznaczone
            };
            mainLayout.Controls.Add(lblAvailable, 0, 3);
            mainLayout.Controls.Add(chkAvailable, 1, 3);

            // ErrorProvider
            errorProvider1 = new ErrorProvider();

            // Przycisk Zapisz (tak jak Wypożycz)
            btnSave = new Button
            {
                Text = "Zapisz",
                Width = 120,
                Height = 35,
                Anchor = AnchorStyles.Right,
                BackColor = Color.Teal,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Margin = new Padding(0, 10, 0, 0)
            };
            btnSave.Click += btnSave_Click;

            var buttonPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.RightToLeft,
                Dock = DockStyle.Fill
            };
            buttonPanel.Controls.Add(btnSave);

            mainLayout.Controls.Add(buttonPanel, 0, 4);
            mainLayout.SetColumnSpan(buttonPanel, 2);

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
                this.DialogResult = DialogResult.OK;
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
