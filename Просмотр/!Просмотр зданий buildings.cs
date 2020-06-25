using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Diploma
{
    public partial class buildings : Form
    {
        public buildings(int mark)
        {
            InitializeComponent();
            this.mark = mark;
        }
        int mark;

        private void buildings_Load(object sender, EventArgs e)
        {
            var connection = new NpgsqlConnection(Options.connectionString);
            connection.Open();

            var commandString = "SELECT type, target, mark FROM pmib6605.buildings";
            var command = new NpgsqlCommand(commandString, connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                if (reader.GetInt32(2) <= mark)
                {
                    dataGridView1.Rows.Add(reader.GetString(0).Trim(), reader.GetString(1).Trim());
                }
            }
            if (dataGridView1.Rows.Count == 0)
            {
                dataGridView1.Visible = false;
                MessageBox.Show("Данных, доступных для Вас еще нет", "Нет информации", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else
            {
                dataGridView1.Visible = true;
            }
        }

        private void главноеМенюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main open = new Main(mark);
            open.Show();
            this.Close();
        }
        private void назадToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectRO open = new selectRO(mark);
            open.Show();
            this.Close();
        }
        private void выйтиИзАккаунтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены?", "Выход из аккаунта", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Login open = new Login();
                open.Show();
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            buildings_Load(sender, e);
        }
    }
}
