namespace BookRentalApp
{
    partial class FormBook
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
            components = new System.ComponentModel.Container();
            errorProvider1 = new ErrorProvider(components);
            tableLayoutPanel1 = new TableLayoutPanel();
            label1 = new Label();
            txtTitle = new TextBox();
            label2 = new Label();
            label3 = new Label();
            txtAuthor = new TextBox();
            txtGenre = new TextBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            btnSave = new Button();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(txtTitle, 1, 0);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(label3, 0, 2);
            tableLayoutPanel1.Controls.Add(txtAuthor, 1, 1);
            tableLayoutPanel1.Controls.Add(txtGenre, 1, 2);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 1, 4);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.Padding = new Padding(15);
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 0F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(364, 231);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Segoe UI", 10F);
            label1.ImageAlign = ContentAlignment.MiddleLeft;
            label1.Location = new Point(18, 15);
            label1.Name = "label1";
            label1.Size = new Size(110, 35);
            label1.TabIndex = 0;
            label1.Text = "Tytuł";
            // 
            // txtTitle
            // 
            txtTitle.Dock = DockStyle.Fill;
            txtTitle.Font = new Font("Segoe UI", 10F);
            txtTitle.Location = new Point(134, 18);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(212, 25);
            txtTitle.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Font = new Font("Segoe UI", 10F);
            label2.ImageAlign = ContentAlignment.MiddleLeft;
            label2.Location = new Point(18, 50);
            label2.Name = "label2";
            label2.Size = new Size(110, 35);
            label2.TabIndex = 2;
            label2.Text = "Autor";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("Segoe UI", 10F);
            label3.ImageAlign = ContentAlignment.MiddleLeft;
            label3.Location = new Point(18, 85);
            label3.Name = "label3";
            label3.Size = new Size(110, 35);
            label3.TabIndex = 3;
            label3.Text = "Gatunek";
            // 
            // txtAuthor
            // 
            txtAuthor.Dock = DockStyle.Fill;
            txtAuthor.Font = new Font("Segoe UI", 10F);
            txtAuthor.Location = new Point(134, 53);
            txtAuthor.Name = "txtAuthor";
            txtAuthor.Size = new Size(212, 25);
            txtAuthor.TabIndex = 4;
            // 
            // txtGenre
            // 
            txtGenre.Dock = DockStyle.Fill;
            txtGenre.Font = new Font("Segoe UI", 10F);
            txtGenre.Location = new Point(134, 88);
            txtGenre.Name = "txtGenre";
            txtGenre.Size = new Size(212, 25);
            txtGenre.TabIndex = 5;
            // 
            // flowLayoutPanel1
            // 
            tableLayoutPanel1.SetColumnSpan(flowLayoutPanel1, 2);
            flowLayoutPanel1.Controls.Add(btnSave);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel1.Location = new Point(18, 168);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(328, 45);
            flowLayoutPanel1.TabIndex = 6;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.Teal;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10F);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(205, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 35);
            btnSave.TabIndex = 0;
            btnSave.Text = "Zapisz";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // FormBook
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(364, 231);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximumSize = new Size(380, 270);
            Name = "FormBook";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Dodaj książkę";
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ErrorProvider errorProvider1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private TextBox txtTitle;
        private Label label2;
        private Label label3;
        private TextBox txtAuthor;
        private TextBox txtGenre;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button btnSave;
    }
}