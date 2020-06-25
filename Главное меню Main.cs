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
    public partial class Main : Form
    {
        public Main(int mark)
        {
            InitializeComponent();
            this.mark = mark;
        }
        int mark;

        private void button1_Click(object sender, EventArgs e)
        {
            selectRO open = new selectRO(mark);
            open.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Modification open = new Modification(mark);
            open.Show();
            this.Hide();
        }

        private void выйтиИзАккаунтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login open = new Login();
            open.Show();
            this.Close();
        }
    }
}
