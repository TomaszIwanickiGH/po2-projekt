using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;

namespace BookRentalApp
{
    public partial class Form1 : Form
    {
        //private readonly AppDbContext _context = new AppDbContext();
        private TabControl tabControl;

        public Form1()
        {
            InitializeComponent();
            InitializeUI();
            //_context = new AppDbContext();  // Inicjalizacja kontekstu bazy danych
        }

        private void InitializeUI()
        {
            this.Text = "Zarz�dzanie wypo�yczeniami";
            this.MinimumSize = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            tabControl = new TabControl
            {
                Dock = DockStyle.Fill
            };
            this.Controls.Add(tabControl);

            AddTab("Ksi��ki", LoadBooks);
            AddTab("U�ytkownicy", LoadCustomers);
            AddTab("Wypo�yczenia", LoadRentals);

            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;

            // Minimalna wielko�� okna
            this.MinimumSize = new Size(800, 600);

            // Panel po lewej stronie (na przyciski)
            var leftPanel = new Panel();
            leftPanel.Dock = DockStyle.Left;
            leftPanel.Width = 200;
            leftPanel.BackColor = Color.FromArgb(50, 50, 50); // Ciemny szary
            this.Controls.Add(leftPanel);

            // TableLayoutPanel do zarz�dzania rozmieszczeniem przycisk�w
            var tableLayout = new TableLayoutPanel();
            tableLayout.Dock = DockStyle.Fill;
            tableLayout.RowCount = 3;
            tableLayout.ColumnCount = 1;
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
            tableLayout.AutoSize = true;

            // Przycisk "Dodaj klienta"
            var btnCustomer = new Button();
            btnCustomer.Text = "Dodaj klienta";
            btnCustomer.Dock = DockStyle.Fill;
            btnCustomer.FlatStyle = FlatStyle.Flat;
            btnCustomer.BackColor = Color.Teal;
            btnCustomer.ForeColor = Color.White;
            btnCustomer.Click += btnCustomers_Click;
            tableLayout.Controls.Add(btnCustomer, 0, 0);

            // Przycisk "Dodaj ksi��k�"
            var btnBook = new Button();
            btnBook.Text = "Dodaj ksi��k�";
            btnBook.Dock = DockStyle.Fill;
            btnBook.FlatStyle = FlatStyle.Flat;
            btnBook.BackColor = Color.Teal;
            btnBook.ForeColor = Color.White;
            btnBook.Click += btnBooks_Click;
            tableLayout.Controls.Add(btnBook, 0, 1);

            // Przycisk "Wypo�yczenia"
            var btnRental = new Button();
            btnRental.Text = "Wypo�yczenia";
            btnRental.Dock = DockStyle.Fill;
            btnRental.FlatStyle = FlatStyle.Flat;
            btnRental.BackColor = Color.Teal;
            btnRental.ForeColor = Color.White;
            btnRental.Click += btnRentals_Click;
            tableLayout.Controls.Add(btnRental, 0, 2);

            // Dodanie layoutu do panelu
            leftPanel.Controls.Add(tableLayout);

            // Panel po prawej stronie na ksi��ki
            var rightPanel = new Panel();
            rightPanel.Dock = DockStyle.Fill;
            rightPanel.BackColor = Color.White; // T�o dla listy ksi��ek
            this.Controls.Add(rightPanel);

            // DataGridView do wy�wietlania ksi��ek
            var dataGridViewBooks = new DataGridView();
            dataGridViewBooks.Dock = DockStyle.Fill;
            dataGridViewBooks.AutoGenerateColumns = true;
            dataGridViewBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            rightPanel.Controls.Add(dataGridViewBooks);
            dataGridViewBooks.AutoGenerateColumns = false; // Zmienia automatyczne generowanie kolumn

            // Definiowanie kolumn
            var columnTitle = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Title",  // Powi�zanie z w�a�ciwo�ci� "Title" w modelu
                HeaderText = "Tytu�",        // Nag��wek kolumny
                Width = 200,                 // Szeroko�� kolumny
                SortMode = DataGridViewColumnSortMode.Automatic // Umo�liwia sortowanie
            };
            dataGridViewBooks.Columns.Add(columnTitle);

            var columnAuthor = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Author",
                HeaderText = "Autor",
                Width = 150,
                SortMode = DataGridViewColumnSortMode.Automatic
            };
            dataGridViewBooks.Columns.Add(columnAuthor);

            var columnGenre = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Genre",
                HeaderText = "Gatunek",
                Width = 120,
                SortMode = DataGridViewColumnSortMode.Automatic
            };
            dataGridViewBooks.Columns.Add(columnGenre);

            var columnAvailability = new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "IsAvailable",
                HeaderText = "Dost�pno��",
                Width = 120,
                SortMode = DataGridViewColumnSortMode.Automatic
            };
            dataGridViewBooks.Columns.Add(columnAvailability);

            // Ustawienia og�lne tabeli
            dataGridViewBooks.AllowUserToAddRows = false;  // Zablokowanie dodawania wierszy
            dataGridViewBooks.AllowUserToDeleteRows = false; // Zablokowanie usuwania wierszy
            dataGridViewBooks.ReadOnly = true;              // Tylko do odczytu
            dataGridViewBooks.MultiSelect = false;          // Brak zaznaczania wielu wierszy
            dataGridViewBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Zaznaczanie ca�ego wiersza
            dataGridViewBooks.RowHeadersVisible = false;    // Ukrycie nag��wk�w wierszy
            dataGridViewBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Dostosowanie szeroko�ci kolumn

            // �adowanie ksi��ek z bazy
            LoadBooks(dataGridViewBooks);
        }

        private void AddTab(string title, Action<DataGridView> loadAction)
        {
            var tabPage = new TabPage(title);
            var grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            tabPage.Controls.Add(grid);
            tabControl.TabPages.Add(tabPage);

            // �aduj dane po wej�ciu na zak�adk�
            tabControl.Selected += (s, e) =>
            {
                if (e.TabPage == tabPage)
                    loadAction(grid);
            };

            // Za�aduj dane pocz�tkowo dla pierwszej zak�adki
            if (tabControl.TabPages.Count == 1)
                loadAction(grid);
        }

        private async void LoadBooks(DataGridView dataGridViewBooks)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var books = await context.Books.ToListAsync();

                    if (books != null && books.Count > 0)
                    {
                        dataGridViewBooks.DataSource = books;

                        // Ukryj kolumn� Rentals
                        if (dataGridViewBooks.Columns["Rentals"] != null)
                            dataGridViewBooks.Columns["Rentals"].Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Brak ksi��ek w bazie danych.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"B��d przy �adowaniu ksi��ek: {ex.Message}");
            }
        }


        private async void LoadCustomers(DataGridView grid)
        {
            using (var context = new AppDbContext())
            {
                var customers = await context.Customers.ToListAsync();
                grid.DataSource = customers;

                // Ukryj kolumn� Rentals
                if (grid.Columns["Rentals"] != null)
                    grid.Columns["Rentals"].Visible = false;
            }
        }


        private async void LoadRentals(DataGridView grid)
        {
            using (var context = new AppDbContext())
            {
                var rentals = await context.Rentals
                    .Include(r => r.Customer)
                    .Include(r => r.Book)
                    .Select(r => new
                    {
                        Klient = r.Customer.FullName,
                        Ksiazka = r.Book.Title,
                        DataWypozyczenia = r.RentDate
                    })
                    .ToListAsync();
                grid.DataSource = rentals;
            }
        }



        private void btnCustomers_Click(object sender, EventArgs e)
        {
            FormCustomer formCustomer = new FormCustomer();
            formCustomer.ShowDialog();
        }

        private void btnBooks_Click(object sender, EventArgs e)
        {
            FormBook formBook = new FormBook();
            formBook.ShowDialog();
        }

        private void btnRentals_Click(object sender, EventArgs e)
        {
            FormRental formRental = new FormRental();
            formRental.ShowDialog();
        }
    }
}
