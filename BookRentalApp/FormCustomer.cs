using BookRentalApp.Models;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BookRentalApp
{
    public partial class FormCustomer : Form
    {
        private readonly AppDbContext _context = new AppDbContext();

        public FormCustomer()
        {
            InitializeComponent();
        }

        private Customer _customer;
        private bool _isEditMode = false;

        public FormCustomer(Customer customer = null)
        {
            InitializeComponent();

            if (customer != null)
            {
                _isEditMode = true;
                _customer = customer;
                this.Text = "Edytuj klienta";

                txtName.Text = _customer.FullName;
                txtEmail.Text = _customer.Email;
                dateTimePickerDOB.Value = _customer.DateOfBirth.ToLocalTime();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();

            if (!ValidateInputs()) return;

            try
            {
                if (_isEditMode)
                {
                    _customer.FullName = txtName.Text.Trim();
                    _customer.Email = txtEmail.Text.Trim();
                    _customer.DateOfBirth = dateTimePickerDOB.Value.ToUniversalTime();

                    _context.Customers.Update(_customer);
                }
                else
                {
                    var customer = new Customer
                    {
                        FullName = txtName.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        DateOfBirth = dateTimePickerDOB.Value.ToUniversalTime()
                    };
                    _context.Customers.Add(customer);
                }

                _context.SaveChanges();

                MessageBox.Show("Klient zapisany.");
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

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                errorProvider1.SetError(txtName, "Imię i nazwisko wymagane");
                valid = false;
            }

            if (!Regex.IsMatch(txtEmail.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                errorProvider1.SetError(txtEmail, "Nieprawidłowy adres email");
                valid = false;
            }

            if (dateTimePickerDOB.Value > DateTime.Now)
            {
                errorProvider1.SetError(dateTimePickerDOB, "Data urodzenia nie może być z przyszłości");
                valid = false;
            }

            return valid;
        }
    }
}
