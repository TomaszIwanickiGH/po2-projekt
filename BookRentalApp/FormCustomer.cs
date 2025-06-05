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
        private TextBox txtName;
        private TextBox txtEmail;
        private DateTimePicker dateTimePickerDOB;
        private Button btnSave;
        //private ErrorProvider errorProvider1;

        public FormCustomer()
        {
            InitializeComponent();
            InitializeUI();
        }

        private Customer _customer;  // pole do trzymania aktualnego klienta
        private bool _isEditMode = false;

        public FormCustomer(Customer customer = null)
        {
            InitializeComponent();
            InitializeUI();

            if (customer != null)
            {
                _isEditMode = true;
                _customer = customer;
                this.Text = "Edytuj klienta";

                // Załaduj dane klienta do pól
                txtName.Text = _customer.FullName;
                txtEmail.Text = _customer.Email;
                dateTimePickerDOB.Value = _customer.DateOfBirth.ToLocalTime();
            }
        }


        private void InitializeUI()
        {
            this.Text = "Dodaj klienta";
            this.Size = new Size(380, 230);
            this.MinimumSize = new Size(380, 230);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.BackColor = Color.White;

            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 4,
                Padding = new Padding(15),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F)); // przycisk

            // Imię i nazwisko
            var lblName = new Label
            {
                Text = "Imię i nazwisko:",
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10)
            };
            txtName = new TextBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10) };
            mainLayout.Controls.Add(lblName, 0, 0);
            mainLayout.Controls.Add(txtName, 1, 0);

            // Email
            var lblEmail = new Label
            {
                Text = "Email:",
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10)
            };
            txtEmail = new TextBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10) };
            mainLayout.Controls.Add(lblEmail, 0, 1);
            mainLayout.Controls.Add(txtEmail, 1, 1);

            // Data urodzenia
            var lblDOB = new Label
            {
                Text = "Data urodzenia:",
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10)
            };
            dateTimePickerDOB = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10)
            };
            mainLayout.Controls.Add(lblDOB, 0, 2);
            mainLayout.Controls.Add(dateTimePickerDOB, 1, 2);

            // ErrorProvider
            errorProvider1 = new ErrorProvider();

            // Przycisk Zapisz
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

            mainLayout.Controls.Add(buttonPanel, 0, 3);
            mainLayout.SetColumnSpan(buttonPanel, 2);

            this.Controls.Add(mainLayout);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();

            if (!ValidateInputs()) return;

            try
            {
                if (_isEditMode)
                {
                    // Edycja istniejącego klienta
                    _customer.FullName = txtName.Text.Trim();
                    _customer.Email = txtEmail.Text.Trim();
                    _customer.DateOfBirth = dateTimePickerDOB.Value.ToUniversalTime();

                    _context.Customers.Update(_customer);
                }
                else
                {
                    // Nowy klient
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
