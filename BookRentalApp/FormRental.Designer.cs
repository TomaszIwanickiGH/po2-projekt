namespace BookRentalApp
{
    partial class FormRental
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblCustomer = new Label();
            lblBook = new Label();
            comboCustomers = new ComboBox();
            comboBooks = new ComboBox();
            btnRent = new Button();
            SuspendLayout();
            // 
            // lblCustomer
            // 
            lblCustomer.AutoSize = true;
            lblCustomer.Font = new Font("Segoe UI", 10F);
            lblCustomer.Location = new Point(15, 20);
            lblCustomer.Name = "lblCustomer";
            lblCustomer.Size = new Size(46, 19);
            lblCustomer.TabIndex = 0;
            lblCustomer.Text = "Klient:";
            lblCustomer.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblBook
            // 
            lblBook.AutoSize = true;
            lblBook.Font = new Font("Segoe UI", 10F);
            lblBook.Location = new Point(15, 60);
            lblBook.Name = "lblBook";
            lblBook.Size = new Size(53, 19);
            lblBook.TabIndex = 1;
            lblBook.Text = "Książka";
            lblBook.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // comboCustomers
            // 
            comboCustomers.DropDownStyle = ComboBoxStyle.DropDownList;
            comboCustomers.Font = new Font("Segoe UI", 10F);
            comboCustomers.FormattingEnabled = true;
            comboCustomers.Location = new Point(130, 20);
            comboCustomers.Name = "comboCustomers";
            comboCustomers.Size = new Size(220, 25);
            comboCustomers.TabIndex = 2;
            // 
            // comboBooks
            // 
            comboBooks.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBooks.Font = new Font("Segoe UI", 10F);
            comboBooks.FormattingEnabled = true;
            comboBooks.Location = new Point(130, 60);
            comboBooks.Name = "comboBooks";
            comboBooks.Size = new Size(220, 25);
            comboBooks.TabIndex = 3;
            // 
            // btnRent
            // 
            btnRent.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnRent.BackColor = Color.Teal;
            btnRent.FlatStyle = FlatStyle.Flat;
            btnRent.Font = new Font("Segoe UI", 10F);
            btnRent.ForeColor = Color.White;
            btnRent.Location = new Point(230, 110);
            btnRent.Name = "btnRent";
            btnRent.Size = new Size(120, 35);
            btnRent.TabIndex = 4;
            btnRent.Text = "Wypożycz";
            btnRent.UseVisualStyleBackColor = false;
            btnRent.Click += btnRent_Click;
            // 
            // FormRental
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(364, 161);
            Controls.Add(btnRent);
            Controls.Add(comboBooks);
            Controls.Add(comboCustomers);
            Controls.Add(lblBook);
            Controls.Add(lblCustomer);
            MaximumSize = new Size(380, 200);
            Name = "FormRental";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Nowe wypożyczenie";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCustomer;
        private Label lblBook;
        private ComboBox comboCustomers;
        private ComboBox comboBooks;
        private Button btnRent;
    }
}