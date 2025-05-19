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
            this.Size = new Size(380, 200);
            this.MinimumSize = new Size(380, 200);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.BackColor = Color.White;

            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 3,
                Padding = new Padding(15),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));

            // Label: Klient
            var lblCustomer = new Label
            {
                Text = "Klient:",
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10, FontStyle.Regular)
            };
            comboCustomers = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10, FontStyle.Regular)
            };
            mainLayout.Controls.Add(lblCustomer, 0, 0);
            mainLayout.Controls.Add(comboCustomers, 1, 0);

            // Label: Książka
            var lblBook = new Label
            {
                Text = "Książka:",
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10, FontStyle.Regular)
            };
            comboBooks = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10, FontStyle.Regular)
            };
            mainLayout.Controls.Add(lblBook, 0, 1);
            mainLayout.Controls.Add(comboBooks, 1, 1);

            // Button: Wypożycz
            btnRent = new Button
            {
                Text = "Wypożycz",
                Width = 120,
                Height = 35,
                Anchor = AnchorStyles.Right,
                BackColor = Color.Teal,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Margin = new Padding(0, 10, 0, 0)
            };
            btnRent.Click += btnRent_Click;

            var buttonPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.RightToLeft,
                Dock = DockStyle.Fill
            };
            buttonPanel.Controls.Add(btnRent);

            mainLayout.Controls.Add(buttonPanel, 0, 2);
            mainLayout.SetColumnSpan(buttonPanel, 2);

            this.Controls.Add(mainLayout);
        }

        private void LoadData()
        {
            comboCustomers.DataSource = _context.Customers
                .OrderBy(c => c.FullName)
                .ToList();
            comboCustomers.DisplayMember = "FullName";
            comboCustomers.ValueMember = "CustomerId";

            var currentTime = DateTime.UtcNow;

            // Zwracaj książki, które są dostępne (brak wypożyczeń lub ostatnie zwrócone)
            var availableBooks = _context.Books
                .Where(b => !b.Rentals.Any() ||
                            b.Rentals.OrderByDescending(r => r.RentDate)
                                     .FirstOrDefault().ReturnDate < currentTime)
                .OrderBy(b => b.Title)
                .ToList();

            comboBooks.DataSource = availableBooks;
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
                    ReturnDate = DateTime.UtcNow.AddDays(7)
                };

                var book = _context.Books.Find(rental.BookId);
                book.IsAvailable = false;

                _context.Rentals.Add(rental);
                _context.SaveChanges();

                MessageBox.Show("Wypożyczenie zapisane.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd: {ex.InnerException?.Message ?? ex.Message}");
            }
        }
    }
}
