using BookRentalApp.Models;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BookRentalApp
{
    public partial class FormRental : Form
    {
        private readonly AppDbContext _context = new AppDbContext();

        private ComboBox comboCustomers;
        private ComboBox comboBooks;
        private Button btnRent;

        public FormRental()
        {
            InitializeComponent();
            InitializeUI();
            LoadData();
        }

        private void InitializeUI()
        {
            this.Text = "Nowe wypożyczenie";
            this.Size = new Size(400, 300);
            this.MinimumSize = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.BackColor = Color.White;

            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 3,
                Padding = new Padding(20),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));

            // Klient
            var lblCustomer = new Label
            {
                Text = "Klient:",
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill
            };
            comboCustomers = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Dock = DockStyle.Fill
            };
            mainLayout.Controls.Add(lblCustomer, 0, 0);
            mainLayout.Controls.Add(comboCustomers, 1, 0);

            // Książka
            var lblBook = new Label
            {
                Text = "Książka:",
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill
            };
            comboBooks = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Dock = DockStyle.Fill
            };
            mainLayout.Controls.Add(lblBook, 0, 1);
            mainLayout.Controls.Add(comboBooks, 1, 1);

            // Przycisk Wypożycz
            btnRent = new Button
            {
                Text = "Wypożycz",
                Dock = DockStyle.Fill,
                Height = 40,
                BackColor = Color.Teal,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnRent.Click += btnRent_Click;
            mainLayout.Controls.Add(btnRent, 0, 2);
            mainLayout.SetColumnSpan(btnRent, 2);

            this.Controls.Add(mainLayout);
        }

        private void LoadData()
        {
            comboCustomers.DataSource = _context.Customers.ToList();
            comboCustomers.DisplayMember = "FullName";
            comboCustomers.ValueMember = "CustomerId";

            comboBooks.DataSource = _context.Books.Where(b => b.IsAvailable).ToList();
            comboBooks.DisplayMember = "Title";
            comboBooks.ValueMember = "BookId";
        }

        private void btnRent_Click(object sender, EventArgs e)
        {
            if (comboCustomers.SelectedItem == null || comboBooks.SelectedItem == null)
            {
                MessageBox.Show("Wybierz klienta i książkę.");
                return;
            }

            try
            {
                var rental = new Rental
                {
                    CustomerId = (int)comboCustomers.SelectedValue,
                    BookId = (int)comboBooks.SelectedValue,
                    RentDate = DateTime.UtcNow,
                    ReturnDate = null
                };

                var book = _context.Books.Find(rental.BookId);
                book.IsAvailable = false;

                _context.Rentals.Add(rental);
                _context.SaveChanges();

                MessageBox.Show("Wypożyczenie zapisane.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd: {ex.InnerException?.Message ?? ex.Message}");
            }
        }
    }
}
