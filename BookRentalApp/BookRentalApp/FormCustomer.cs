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
            InitializeComponent();  // Jeśli używasz WinForms Designera
            InitializeUI();         // Jeśli UI tworzysz ręcznie (jak tutaj)
        }

        private void InitializeUI()
        {
            this.Text = "Dodaj klienta";
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
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // dla przycisku

            // Imię i nazwisko
            var lblName = new Label
            {
                Text = "Imię i nazwisko:",
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill
            };
            txtName = new TextBox { Dock = DockStyle.Fill };
            mainLayout.Controls.Add(lblName, 0, 0);
            mainLayout.Controls.Add(txtName, 1, 0);

            // Email
            var lblEmail = new Label
            {
                Text = "Email:",
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill
            };
            txtEmail = new TextBox { Dock = DockStyle.Fill };
            mainLayout.Controls.Add(lblEmail, 0, 1);
            mainLayout.Controls.Add(txtEmail, 1, 1);

            // Data urodzenia
            var lblDOB = new Label
            {
                Text = "Data urodzenia:",
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill
            };
            dateTimePickerDOB = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Dock = DockStyle.Fill
            };
            mainLayout.Controls.Add(lblDOB, 0, 2);
            mainLayout.Controls.Add(dateTimePickerDOB, 1, 2);

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

        // Obsługa kliknięcia przycisku 'Zapisz'
        private void btnSave_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();  // Usunięcie poprzednich błędów

            if (!ValidateInputs())
                return;

            try
            {
                var customer = new Customer
                {
                    FullName = txtName.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    DateOfBirth = dateTimePickerDOB.Value.ToUniversalTime()
                };

                _context.Customers.Add(customer);
                _context.SaveChanges();

                MessageBox.Show("Klient zapisany.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd zapisu: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        // Walidacja danych wejściowych
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
