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
        private TabControl tabControl;
        private TextBox txtSearch;

        // Grids do odœwie¿ania
        private DataGridView booksGrid;
        private DataGridView customersGrid;
        private DataGridView rentalsGrid;

        public Form1()
        {
            InitializeComponent();
            InitializeUI();
        }

        private void InitializeUI()
        {
            this.Text = "Zarz¹dzanie wypo¿yczeniami";
            this.MinimumSize = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            tabControl = new TabControl { Dock = DockStyle.Fill };
            this.Controls.Add(tabControl);

            AddTab("Ksi¹¿ki", LoadBooks);
            AddTab("U¿ytkownicy", LoadCustomers);
            AddTab("Historia wypo¿yczeñ", LoadRentals);

            var leftPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 150,
                BackColor = Color.FromArgb(45, 45, 48)
            };
            this.Controls.Add(leftPanel);

            var tableLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                ColumnCount = 1,
                RowCount = 3,
                AutoSize = true,
                Padding = new Padding(10)
            };

            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

            Size buttonSize = new Size(130, 30);
            Font buttonFont = new Font("Segoe UI", 9, FontStyle.Bold);

            var btnCustomer = new Button
            {
                Text = "Dodaj klienta",
                Size = buttonSize,
                Font = buttonFont,
                BackColor = Color.Teal,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(5)
            };
            btnCustomer.Click += btnCustomers_Click;
            tableLayout.Controls.Add(btnCustomer, 0, 0);

            var btnBook = new Button
            {
                Text = "Dodaj ksi¹¿kê",
                Size = buttonSize,
                Font = buttonFont,
                BackColor = Color.Teal,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(5)
            };
            btnBook.Click += btnBooks_Click;
            tableLayout.Controls.Add(btnBook, 0, 1);

            var btnRental = new Button
            {
                Text = "Wypo¿ycz ksi¹¿kê",
                Size = buttonSize,
                Font = buttonFont,
                BackColor = Color.Teal,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(5)
            };
            btnRental.Click += btnRentals_Click;
            tableLayout.Controls.Add(btnRental, 0, 2);

            leftPanel.Controls.Add(tableLayout);
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

            if (title == "Ksi¹¿ki")
            {
                var booksPanel = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    RowCount = 2,
                    ColumnCount = 1,
                };
                booksPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
                booksPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

                txtSearch = new TextBox
                {
                    PlaceholderText = "Wyszukaj ksi¹¿kê po tytule...",
                    Dock = DockStyle.Fill,
                    Font = new Font("Segoe UI", 10),
                    Margin = new Padding(5)
                };
                txtSearch.TextChanged += (s, e) => loadAction(grid);

                booksPanel.Controls.Add(txtSearch, 0, 0);
                booksPanel.Controls.Add(grid, 0, 1);
                tabPage.Controls.Add(booksPanel);

                grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Tytu³", HeaderText = "Tytu³", Width = 200 });
                grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Autor", HeaderText = "Autor", Width = 150 });
                grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Gatunek", HeaderText = "Gatunek", Width = 120 });
                grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Dostêpnoœæ", HeaderText = "Dostêpnoœæ", Width = 120 });

                booksGrid = grid;
            }
            else
            {
                tabPage.Controls.Add(grid);
                if (title == "U¿ytkownicy") customersGrid = grid;
                else if (title == "Historia wypo¿yczeñ") rentalsGrid = grid;
            }

            loadAction(grid);
            tabControl.TabPages.Add(tabPage);
        }

        private async void LoadBooks(DataGridView dataGridViewBooks)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    string filter = txtSearch?.Text?.Trim().ToLower() ?? "";

                    var booksQuery = context.Books.AsQueryable();
                    if (!string.IsNullOrEmpty(filter))
                        booksQuery = booksQuery.Where(b => b.Title.ToLower().Contains(filter));

                    var currentTime = DateTime.UtcNow;

                    var books = await booksQuery
                        .Include(b => b.Rentals)
                        .Select(b => new
                        {
                            Tytu³ = b.Title,
                            Autor = b.Author,
                            Gatunek = b.Genre,
                            Dostêpnoœæ = !b.Rentals.Any() ||
                                          b.Rentals.OrderByDescending(r => r.RentDate)
                                                   .FirstOrDefault().ReturnDate < currentTime
                                          ? "Tak" : "Nie"
                        })
                        .ToListAsync();

                    dataGridViewBooks.DataSource = books;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"B³¹d przy ³adowaniu ksi¹¿ek: {ex.Message}");
            }
        }


        private async void LoadCustomers(DataGridView grid)
        {
            using (var context = new AppDbContext())
            {
                var customers = await context.Customers.ToListAsync();
                grid.DataSource = customers;

                if (grid.Columns["CustomerId"] != null) grid.Columns["CustomerId"].Visible = false;
                if (grid.Columns["Rentals"] != null) grid.Columns["Rentals"].Visible = false;
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
                        DataWypozyczenia = r.RentDate,
                        DataZwrotu = r.ReturnDate
                    })
                    .ToListAsync();

                grid.DataSource = rentals;
            }
        }

        // Zmodyfikowane przyciski z odœwie¿aniem ??
        private void btnCustomers_Click(object sender, EventArgs e)
        {
            FormCustomer formCustomer = new FormCustomer();
            if (formCustomer.ShowDialog() == DialogResult.OK)
                LoadCustomers(customersGrid);
        }

        private void btnBooks_Click(object sender, EventArgs e)
        {
            FormBook formBook = new FormBook();
            if (formBook.ShowDialog() == DialogResult.OK)
                LoadBooks(booksGrid);
        }

        private void btnRentals_Click(object sender, EventArgs e)
        {
            FormRental formRental = new FormRental();
            if (formRental.ShowDialog() == DialogResult.OK)
            {
                LoadRentals(rentalsGrid);
                LoadBooks(booksGrid); // ¿eby zaktualizowaæ dostêpnoœæ ksi¹¿ki
            }
        }
    }
}
