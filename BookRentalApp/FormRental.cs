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

        public FormRental()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            comboCustomers.DataSource = _context.Customers
                .OrderBy(c => c.FullName)
                .ToList();
            comboCustomers.DisplayMember = "FullName";
            comboCustomers.ValueMember = "CustomerId";

            var currentTime = DateTime.UtcNow;

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
