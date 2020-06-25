namespace Diploma
{
    partial class addUser
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
            this.labelName = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.labelBirthDate = new System.Windows.Forms.Label();
            this.comboBoxGeneralRank = new System.Windows.Forms.ComboBox();
            this.comboBoxRank = new System.Windows.Forms.ComboBox();
            this.labelGeneralRank = new System.Windows.Forms.Label();
            this.labelRank = new System.Windows.Forms.Label();
            this.Apply = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.labelAccess = new System.Windows.Forms.Label();
            this.labelReccomendation = new System.Windows.Forms.Label();
            this.Cancel = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.back = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.ForeColor = System.Drawing.Color.White;
            this.labelName.Location = new System.Drawing.Point(16, 79);
            this.labelName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(242, 36);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Введите ФИО сотрудника в формате:\r\n\"Фамилия И. О.\"";
            // 
            // name
            // 
            this.name.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.name.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.name.ForeColor = System.Drawing.Color.White;
            this.name.Location = new System.Drawing.Point(16, 133);
            this.name.Margin = new System.Windows.Forms.Padding(4);
            this.name.Name = "name";
            this.name.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.name.Size = new System.Drawing.Size(327, 19);
            this.name.TabIndex = 1;
            this.name.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.name_KeyPress);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.dateTimePicker1.CalendarMonthBackground = System.Drawing.Color.White;
            this.dateTimePicker1.Location = new System.Drawing.Point(16, 220);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(265, 26);
            this.dateTimePicker1.TabIndex = 3;
            // 
            // labelBirthDate
            // 
            this.labelBirthDate.AutoSize = true;
            this.labelBirthDate.ForeColor = System.Drawing.Color.White;
            this.labelBirthDate.Location = new System.Drawing.Point(16, 198);
            this.labelBirthDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelBirthDate.Name = "labelBirthDate";
            this.labelBirthDate.Size = new System.Drawing.Size(246, 18);
            this.labelBirthDate.TabIndex = 4;
            this.labelBirthDate.Text = "Выберите дату рождения сотрудника";
            // 
            // comboBoxGeneralRank
            // 
            this.comboBoxGeneralRank.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.comboBoxGeneralRank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGeneralRank.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxGeneralRank.ForeColor = System.Drawing.Color.White;
            this.comboBoxGeneralRank.FormattingEnabled = true;
            this.comboBoxGeneralRank.Location = new System.Drawing.Point(16, 320);
            this.comboBoxGeneralRank.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxGeneralRank.Name = "comboBoxGeneralRank";
            this.comboBoxGeneralRank.Size = new System.Drawing.Size(327, 26);
            this.comboBoxGeneralRank.TabIndex = 5;
            this.comboBoxGeneralRank.SelectionChangeCommitted += new System.EventHandler(this.comboBoxGeneralRank_SelectionChangeCommitted);
            // 
            // comboBoxRank
            // 
            this.comboBoxRank.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.comboBoxRank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRank.ForeColor = System.Drawing.Color.White;
            this.comboBoxRank.FormattingEnabled = true;
            this.comboBoxRank.Location = new System.Drawing.Point(16, 417);
            this.comboBoxRank.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxRank.Name = "comboBoxRank";
            this.comboBoxRank.Size = new System.Drawing.Size(327, 26);
            this.comboBoxRank.TabIndex = 6;
            this.comboBoxRank.SelectionChangeCommitted += new System.EventHandler(this.comboBoxRank_SelectionChangeCommitted);
            // 
            // labelGeneralRank
            // 
            this.labelGeneralRank.AutoSize = true;
            this.labelGeneralRank.ForeColor = System.Drawing.Color.White;
            this.labelGeneralRank.Location = new System.Drawing.Point(21, 298);
            this.labelGeneralRank.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelGeneralRank.Name = "labelGeneralRank";
            this.labelGeneralRank.Size = new System.Drawing.Size(223, 18);
            this.labelGeneralRank.TabIndex = 7;
            this.labelGeneralRank.Text = "Выберите ранг военнослужащего";
            // 
            // labelRank
            // 
            this.labelRank.AutoSize = true;
            this.labelRank.ForeColor = System.Drawing.Color.White;
            this.labelRank.Location = new System.Drawing.Point(16, 395);
            this.labelRank.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelRank.Name = "labelRank";
            this.labelRank.Size = new System.Drawing.Size(182, 18);
            this.labelRank.TabIndex = 8;
            this.labelRank.Text = "Выберите воинское звание";
            // 
            // Apply
            // 
            this.Apply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(184)))), ((int)(((byte)(206)))));
            this.Apply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Apply.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.Apply.Location = new System.Drawing.Point(583, 278);
            this.Apply.Margin = new System.Windows.Forms.Padding(4);
            this.Apply.Name = "Apply";
            this.Apply.Size = new System.Drawing.Size(232, 68);
            this.Apply.TabIndex = 9;
            this.Apply.Text = "Подтвердить";
            this.Apply.UseVisualStyleBackColor = false;
            this.Apply.Click += new System.EventHandler(this.Apply_Click);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.ForeColor = System.Drawing.Color.White;
            this.textBox2.Location = new System.Drawing.Point(701, 133);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox2.Size = new System.Drawing.Size(191, 26);
            this.textBox2.TabIndex = 10;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.BackColor = System.Drawing.Color.White;
            this.numericUpDown1.Location = new System.Drawing.Point(584, 134);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.ReadOnly = true;
            this.numericUpDown1.Size = new System.Drawing.Size(81, 26);
            this.numericUpDown1.TabIndex = 11;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // labelAccess
            // 
            this.labelAccess.AutoSize = true;
            this.labelAccess.ForeColor = System.Drawing.Color.White;
            this.labelAccess.Location = new System.Drawing.Point(580, 97);
            this.labelAccess.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAccess.Name = "labelAccess";
            this.labelAccess.Size = new System.Drawing.Size(181, 18);
            this.labelAccess.TabIndex = 12;
            this.labelAccess.Text = "Выберите уровень доступа";
            // 
            // labelReccomendation
            // 
            this.labelReccomendation.AutoSize = true;
            this.labelReccomendation.ForeColor = System.Drawing.Color.White;
            this.labelReccomendation.Location = new System.Drawing.Point(580, 220);
            this.labelReccomendation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelReccomendation.Name = "labelReccomendation";
            this.labelReccomendation.Size = new System.Drawing.Size(169, 18);
            this.labelReccomendation.TabIndex = 13;
            this.labelReccomendation.Text = "Рекомендуемый доступ: ";
            // 
            // Cancel
            // 
            this.Cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(184)))), ((int)(((byte)(206)))));
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Cancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.Cancel.Location = new System.Drawing.Point(16, 485);
            this.Cancel.Margin = new System.Windows.Forms.Padding(4);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(232, 68);
            this.Cancel.TabIndex = 14;
            this.Cancel.Text = "Отмена";
            this.Cancel.UseVisualStyleBackColor = false;
            this.Cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.toolStrip1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.back,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(1040, 25);
            this.toolStrip1.TabIndex = 15;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // back
            // 
            this.back.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.back.ForeColor = System.Drawing.Color.White;
            this.back.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(45, 22);
            this.back.Text = "Назад";
            this.back.Click += new System.EventHandler(this.back_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.ForeColor = System.Drawing.Color.White;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(109, 22);
            this.toolStripButton2.Text = "Выйти из аккаунта";
            this.toolStripButton2.Click += new System.EventHandler(this.changeAccount_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(19, 159);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(324, 1);
            this.panel1.TabIndex = 16;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(701, 164);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(194, 1);
            this.panel2.TabIndex = 17;
            this.panel2.Visible = false;
            // 
            // addUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.ClientSize = new System.Drawing.Size(1040, 587);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.labelReccomendation);
            this.Controls.Add(this.labelAccess);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.Apply);
            this.Controls.Add(this.labelRank);
            this.Controls.Add(this.labelGeneralRank);
            this.Controls.Add(this.comboBoxRank);
            this.Controls.Add(this.comboBoxGeneralRank);
            this.Controls.Add(this.labelBirthDate);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.name);
            this.Controls.Add(this.labelName);
            this.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "addUser";
            this.Text = "Добавить пользователя";
            this.Load += new System.EventHandler(this.addUser_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label labelBirthDate;
        private System.Windows.Forms.ComboBox comboBoxGeneralRank;
        private System.Windows.Forms.ComboBox comboBoxRank;
        private System.Windows.Forms.Label labelGeneralRank;
        private System.Windows.Forms.Label labelRank;
        private System.Windows.Forms.Button Apply;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label labelAccess;
        private System.Windows.Forms.Label labelReccomendation;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton back;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}