using Diploma.Админ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diploma
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void addUser_Click(object sender, EventArgs e)
        {
            addUser open = new addUser();
            open.Show();
            this.Close();
        }

        private void regUser_Click(object sender, EventArgs e)
        {
            addAccount open = new addAccount();
            open.Show();
            this.Close();
        }

        private void changeMark_Click(object sender, EventArgs e)
        {
            changeMark open = new changeMark();
            open.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            requestUser open = new requestUser();
            open.Show();
            this.Close();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            toolStrip1.BackColor = Color.FromArgb(34, 36, 49);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Login open = new Login();
            open.Show();
            this.Close();
        }
    }
}
