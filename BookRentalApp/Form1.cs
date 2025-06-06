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
        private TextBox txtSearch;

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
            tabControlTest.TabPages.Clear();

            AddTab("Ksi¹¿ki", LoadBooks);
            AddTab("U¿ytkownicy", LoadCustomers);
            AddTab("Historia wypo¿yczeñ", LoadRentals);
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
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AutoGenerateColumns = title == "Ksi¹¿ki" ? false : true
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


                grid.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "ID",
                    Name = "ID",
                    Visible = false
                });

                grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Tytu³", HeaderText = "Tytu³", Width = 200 });
                grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Autor", HeaderText = "Autor", Width = 150 });
                grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Gatunek", HeaderText = "Gatunek", Width = 120 });
                grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Dostêpnoœæ", HeaderText = "Dostêpnoœæ", Width = 120 });

                var editButton = new DataGridViewButtonColumn
                {
                    HeaderText = "Akcja",
                    Text = "Edytuj",
                    UseColumnTextForButtonValue = true,
                    Width = 70
                };
                grid.Columns.Add(editButton);

                var deleteButton = new DataGridViewButtonColumn
                {
                    HeaderText = "Akcja",
                    Text = "Usuñ",
                    UseColumnTextForButtonValue = true,
                    Width = 70
                };

                grid.Columns.Add(deleteButton);

                grid.CellContentClick += BooksGrid_CellContentClick;


                booksGrid = grid;
            }
            else
            {
                tabPage.Controls.Add(grid);
                if (title == "U¿ytkownicy")
                {
                    grid.CellContentClick += CustomersGrid_CellContentClick;
                    customersGrid = grid;
                }
                else if (title == "Historia wypo¿yczeñ") rentalsGrid = grid;
            }

            loadAction(grid);
            tabControlTest.TabPages.Add(tabPage);
        }

        private async void BooksGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var grid = sender as DataGridView;

            var idObj = grid.Rows[e.RowIndex].Cells["ID"].Value;
            if (idObj == null) return;

            int bookId = Convert.ToInt32(idObj);

            using var context = new AppDbContext();
            var book = await context.Books.FindAsync(bookId);

            if (book == null)
            {
                MessageBox.Show("Nie znaleziono ksi¹¿ki.", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (grid.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                if (e.ColumnIndex == grid.Columns.Count - 2) 
                {
                    var form = new FormBook(book);
                    if (form.ShowDialog() == DialogResult.OK)
                        LoadBooks(grid);
                }
                else if (e.ColumnIndex == grid.Columns.Count - 1)
                {
                    var confirm = MessageBox.Show("Czy na pewno chcesz usun¹æ tê ksi¹¿kê?", "Potwierdzenie", MessageBoxButtons.YesNo);
                    if (confirm == DialogResult.Yes)
                    {
                        try
                        {
                            context.Books.Remove(book);
                            await context.SaveChangesAsync();
                            LoadBooks(grid);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("B³¹d usuwania: " + ex.Message);
                        }
                    }
                }
            }
        }

        private async void CustomersGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var grid = sender as DataGridView;
            var idObj = grid.Rows[e.RowIndex].Cells["ID"].Value; ;
            if (idObj == null) return;

            int customerId = Convert.ToInt32(idObj);

            using var context = new AppDbContext();
            var customer = await context.Customers.FindAsync(customerId);

            if (customer == null)
            {
                MessageBox.Show("Nie znaleziono klienta.", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (grid.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                var buttonText = grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();

                if (buttonText == "Edytuj")
                {
                    var form = new FormCustomer(customer);
                    if (form.ShowDialog() == DialogResult.OK)
                        LoadCustomers(grid);
                }
                else if (buttonText == "Usuñ")
                {
                    var confirm = MessageBox.Show("Czy na pewno chcesz usun¹æ tego klienta?", "Potwierdzenie", MessageBoxButtons.YesNo);
                    if (confirm == DialogResult.Yes)
                    {
                        try
                        {
                            context.Customers.Remove(customer);
                            await context.SaveChangesAsync();
                            LoadCustomers(grid);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("B³¹d usuwania: " + ex.Message);
                        }
                    }
                }
            }

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
                            ID = b.BookId,
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

                    if (dataGridViewBooks.Columns["ID"] != null)
                        dataGridViewBooks.Columns["ID"].Visible = false;
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

                var data = customers.Select(c => new
                {
                    ID = c.CustomerId,
                    ImieNazwisko = c.FullName,
                    Email = c.Email,
                    DataUrodzenia = c.DateOfBirth.ToShortDateString()
                }).ToList();

                grid.DataSource = data;

                if (grid.Columns["ID"] != null) grid.Columns["ID"].Visible = false;

                if (grid.Columns["ImieNazwisko"] != null)
                    grid.Columns["ImieNazwisko"].HeaderText = "Imiê i nazwisko";
                if (grid.Columns["Email"] != null)
                    grid.Columns["Email"].HeaderText = "Email";
                if (grid.Columns["DataUrodzenia"] != null)
                    grid.Columns["DataUrodzenia"].HeaderText = "Data urodzenia";

                if (grid.Columns["Edytuj"] == null)
                {
                    var editButton = new DataGridViewButtonColumn
                    {
                        Name = "Edytuj",
                        HeaderText = "Akcja",
                        Text = "Edytuj",
                        UseColumnTextForButtonValue = true,
                        Width = 70
                    };
                    grid.Columns.Add(editButton);
                }

                if (grid.Columns["Usuñ"] == null)
                {
                    var deleteButton = new DataGridViewButtonColumn
                    {
                        Name = "Usuñ",
                        HeaderText = "Akcja",
                        Text = "Usuñ",
                        UseColumnTextForButtonValue = true,
                        Width = 70
                    };
                    grid.Columns.Add(deleteButton);
                }

                customersGrid = grid;
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
                LoadBooks(booksGrid); 
            }
        }
    }
}
