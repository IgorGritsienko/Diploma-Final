namespace Diploma
{
    partial class Login
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.labelLogin = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.buttonEnter = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.textBoxLogin = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelLogin
            // 
            this.labelLogin.AutoSize = true;
            this.labelLogin.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelLogin.ForeColor = System.Drawing.Color.White;
            this.labelLogin.Location = new System.Drawing.Point(60, 61);
            this.labelLogin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(105, 18);
            this.labelLogin.TabIndex = 0;
            this.labelLogin.Text = "Введите логин:";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPassword.ForeColor = System.Drawing.Color.White;
            this.labelPassword.Location = new System.Drawing.Point(60, 132);
            this.labelPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(114, 18);
            this.labelPassword.TabIndex = 1;
            this.labelPassword.Text = "Введите пароль:";
            // 
            // buttonEnter
            // 
            this.buttonEnter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(184)))), ((int)(((byte)(206)))));
            this.buttonEnter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEnter.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonEnter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.buttonEnter.Location = new System.Drawing.Point(168, 216);
            this.buttonEnter.Margin = new System.Windows.Forms.Padding(4);
            this.buttonEnter.Name = "buttonEnter";
            this.buttonEnter.Size = new System.Drawing.Size(166, 50);
            this.buttonEnter.TabIndex = 2;
            this.buttonEnter.Text = "Войти";
            this.buttonEnter.UseVisualStyleBackColor = false;
            this.buttonEnter.Click += new System.EventHandler(this.Authorization);
            // 
            // buttonExit
            // 
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonExit.ForeColor = System.Drawing.Color.White;
            this.buttonExit.Location = new System.Drawing.Point(266, 378);
            this.buttonExit.Margin = new System.Windows.Forms.Padding(4);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(249, 50);
            this.buttonExit.TabIndex = 3;
            this.buttonExit.Text = "Выход из приложения";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // textBoxLogin
            // 
            this.textBoxLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.textBoxLogin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxLogin.ForeColor = System.Drawing.Color.White;
            this.textBoxLogin.Location = new System.Drawing.Point(188, 64);
            this.textBoxLogin.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxLogin.Name = "textBoxLogin";
            this.textBoxLogin.Size = new System.Drawing.Size(232, 17);
            this.textBoxLogin.TabIndex = 4;
            this.textBoxLogin.Text = "login4";
            this.textBoxLogin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxLogin_KeyPress);
            this.textBoxLogin.MouseLeave += new System.EventHandler(this.textBoxLogin_MouseLeave);
            this.textBoxLogin.MouseMove += new System.Windows.Forms.MouseEventHandler(this.textBoxLogin_MouseMove);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.textBoxPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxPassword.ForeColor = System.Drawing.Color.White;
            this.textBoxPassword.Location = new System.Drawing.Point(182, 132);
            this.textBoxPassword.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(240, 17);
            this.textBoxPassword.TabIndex = 5;
            this.textBoxPassword.Text = "password4";
            this.textBoxPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPassword_KeyPress);
            this.textBoxPassword.MouseLeave += new System.EventHandler(this.textBoxPassword_MouseLeave);
            this.textBoxPassword.MouseMove += new System.Windows.Forms.MouseEventHandler(this.textBoxPassword_MouseMove);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.statusStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.statusStrip1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 432);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusStrip1.Size = new System.Drawing.Size(528, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.toolStripStatusLabel1.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            this.toolStripStatusLabel1.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(64, 84);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(356, 1);
            this.panel1.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(66, 154);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(356, 1);
            this.panel2.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(456, 132);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(33, 29);
            this.button1.TabIndex = 10;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(442, 140);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 12;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.ClientSize = new System.Drawing.Size(528, 454);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxLogin);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonEnter);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.labelLogin);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Вход";
            this.Load += new System.EventHandler(this.Вход_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Button buttonEnter;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.TextBox textBoxLogin;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        public System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

