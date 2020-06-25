namespace Diploma
{
    partial class Admin
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
            this.addUser = new System.Windows.Forms.Button();
            this.changeMark = new System.Windows.Forms.Button();
            this.regUser = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // addUser
            // 
            this.addUser.Cursor = System.Windows.Forms.Cursors.Default;
            this.addUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addUser.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addUser.ForeColor = System.Drawing.Color.White;
            this.addUser.Location = new System.Drawing.Point(60, 40);
            this.addUser.Margin = new System.Windows.Forms.Padding(4);
            this.addUser.Name = "addUser";
            this.addUser.Size = new System.Drawing.Size(251, 60);
            this.addUser.TabIndex = 0;
            this.addUser.Text = "Добавить пользователя";
            this.addUser.UseVisualStyleBackColor = true;
            this.addUser.Click += new System.EventHandler(this.addUser_Click);
            // 
            // changeMark
            // 
            this.changeMark.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.changeMark.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.changeMark.ForeColor = System.Drawing.Color.White;
            this.changeMark.Location = new System.Drawing.Point(60, 176);
            this.changeMark.Margin = new System.Windows.Forms.Padding(4);
            this.changeMark.Name = "changeMark";
            this.changeMark.Size = new System.Drawing.Size(251, 60);
            this.changeMark.TabIndex = 1;
            this.changeMark.Text = "Изменить уровень доступа к таблице";
            this.changeMark.UseVisualStyleBackColor = true;
            this.changeMark.Click += new System.EventHandler(this.changeMark_Click);
            // 
            // regUser
            // 
            this.regUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.regUser.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.regUser.ForeColor = System.Drawing.Color.White;
            this.regUser.Location = new System.Drawing.Point(60, 108);
            this.regUser.Margin = new System.Windows.Forms.Padding(4);
            this.regUser.Name = "regUser";
            this.regUser.Size = new System.Drawing.Size(251, 60);
            this.regUser.TabIndex = 2;
            this.regUser.Text = "Зарегистрировать пользователя";
            this.regUser.UseVisualStyleBackColor = true;
            this.regUser.Click += new System.EventHandler(this.regUser_Click);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(60, 244);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(251, 60);
            this.button1.TabIndex = 3;
            this.button1.Text = "Заявки от пользователей";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStrip1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(380, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.AutoToolTip = false;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripButton1.ForeColor = System.Drawing.Color.White;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(109, 22);
            this.toolStripButton1.Text = "Выйти из аккаунта";
            this.toolStripButton1.ToolTipText = "\r\n\r\n";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // Admin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.ClientSize = new System.Drawing.Size(380, 342);
            this.ControlBox = false;
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.regUser);
            this.Controls.Add(this.changeMark);
            this.Controls.Add(this.addUser);
            this.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Admin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Меню администратора";
            this.Load += new System.EventHandler(this.Admin_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addUser;
        private System.Windows.Forms.Button changeMark;
        private System.Windows.Forms.Button regUser;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}