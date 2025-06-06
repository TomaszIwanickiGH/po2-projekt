using BookRentalApp.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BookRentalApp
{
    public partial class FormBook : Form
    {
        private readonly AppDbContext _context = new AppDbContext();

        public FormBook()
        {
            InitializeComponent();
        }

        private Book _bookToEdit;

        public FormBook(Book book = null)
        {
            InitializeComponent();
            _bookToEdit = book;

            if (_bookToEdit != null)
            {
                txtTitle.Text = _bookToEdit.Title;
                txtAuthor.Text = _bookToEdit.Author;
                txtGenre.Text = _bookToEdit.Genre;
                this.Text = "Edytuj książkę";
            }
            else
            {
                this.Text = "Dodaj książkę";
            }
        }
 

        private void btnSave_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();

            if (!ValidateInputs()) return;

            try
            {
                if (_bookToEdit != null)
                {
                    var book = _context.Books.Find(_bookToEdit.BookId);
                    if (book != null)
                    {
                        book.Title = txtTitle.Text.Trim();
                        book.Author = txtAuthor.Text.Trim();
                        book.Genre = txtGenre.Text.Trim();
                        book.IsAvailable = true;
                    }
                }
                else
                {
                    var book = new Book
                    {
                        Title = txtTitle.Text.Trim(),
                        Author = txtAuthor.Text.Trim(),
                        Genre = txtGenre.Text.Trim(),
                        IsAvailable = true
                    };
                    _context.Books.Add(book);
                }

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
