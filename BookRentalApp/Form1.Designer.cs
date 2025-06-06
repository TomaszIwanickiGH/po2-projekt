namespace BookRentalApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelLeft = new Panel();
            tableLayout = new TableLayoutPanel();
            btnCustomer = new Button();
            btnBook = new Button();
            btnRental = new Button();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            tabControlTest = new TabControl();
            tabPage3 = new TabPage();
            tabPage4 = new TabPage();
            panelLeft.SuspendLayout();
            tableLayout.SuspendLayout();
            tabControlTest.SuspendLayout();
            SuspendLayout();
            // 
            // panelLeft
            // 
            panelLeft.BackColor = Color.FromArgb(45, 45, 48);
            panelLeft.Controls.Add(tableLayout);
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Location = new Point(0, 0);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(150, 561);
            panelLeft.TabIndex = 0;
            // 
            // tableLayout
            // 
            tableLayout.AutoSize = true;
            tableLayout.ColumnCount = 1;
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayout.Controls.Add(btnCustomer, 0, 0);
            tableLayout.Controls.Add(btnBook, 0, 1);
            tableLayout.Controls.Add(btnRental, 0, 2);
            tableLayout.Dock = DockStyle.Top;
            tableLayout.Location = new Point(0, 0);
            tableLayout.Name = "tableLayout";
            tableLayout.Padding = new Padding(10);
            tableLayout.RowCount = 3;
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayout.Size = new Size(150, 140);
            tableLayout.TabIndex = 0;
            // 
            // btnCustomer
            // 
            btnCustomer.BackColor = Color.Teal;
            btnCustomer.FlatStyle = FlatStyle.Flat;
            btnCustomer.ForeColor = Color.White;
            btnCustomer.Location = new Point(15, 15);
            btnCustomer.Margin = new Padding(5);
            btnCustomer.Name = "btnCustomer";
            btnCustomer.Size = new Size(120, 30);
            btnCustomer.TabIndex = 0;
            btnCustomer.Text = "Dodaj Klienta";
            btnCustomer.UseVisualStyleBackColor = false;
            btnCustomer.Click += btnCustomers_Click;
            // 
            // btnBook
            // 
            btnBook.BackColor = Color.Teal;
            btnBook.FlatStyle = FlatStyle.Flat;
            btnBook.ForeColor = Color.White;
            btnBook.Location = new Point(15, 55);
            btnBook.Margin = new Padding(5);
            btnBook.Name = "btnBook";
            btnBook.Size = new Size(120, 30);
            btnBook.TabIndex = 1;
            btnBook.Text = "Dodaj Książkę";
            btnBook.UseVisualStyleBackColor = false;
            btnBook.Click += btnBooks_Click;
            // 
            // btnRental
            // 
            btnRental.BackColor = Color.Teal;
            btnRental.FlatStyle = FlatStyle.Flat;
            btnRental.ForeColor = Color.White;
            btnRental.Location = new Point(15, 95);
            btnRental.Margin = new Padding(5);
            btnRental.Name = "btnRental";
            btnRental.Size = new Size(120, 30);
            btnRental.TabIndex = 2;
            btnRental.Text = "Wypożycz książkę";
            btnRental.UseVisualStyleBackColor = false;
            btnRental.Click += btnRentals_Click;
            // 
            // tabPage1
            // 
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(642, 533);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(642, 533);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabControlTest
            // 
            tabControlTest.Controls.Add(tabPage3);
            tabControlTest.Controls.Add(tabPage4);
            tabControlTest.Dock = DockStyle.Fill;
            tabControlTest.Location = new Point(150, 0);
            tabControlTest.Name = "tabControlTest";
            tabControlTest.SelectedIndex = 0;
            tabControlTest.Size = new Size(650, 561);
            tabControlTest.TabIndex = 1;
            // 
            // tabPage3
            // 
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(642, 533);
            tabPage3.TabIndex = 0;
            tabPage3.Text = "tabPage3";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            tabPage4.Location = new Point(4, 24);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(192, 72);
            tabPage4.TabIndex = 1;
            tabPage4.Text = "tabPage4";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(800, 561);
            Controls.Add(tabControlTest);
            Controls.Add(panelLeft);
            MinimumSize = new Size(800, 600);
            Name = "Form1";
            Text = "Zarządzanie wypożyczeniami";
            panelLeft.ResumeLayout(false);
            panelLeft.PerformLayout();
            tableLayout.ResumeLayout(false);
            tabControlTest.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelLeft;
        private TableLayoutPanel tableLayout;
        private Button btnCustomer;
        private Button btnBook;
        private Button btnRental;
        private TabControl tabControl;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabControl tabControlTest;
        private TabPage tabPage3;
        private TabPage tabPage4;
    }
}
