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
    public partial class weapons : Form
    {
        public weapons(int mark)
        {
            InitializeComponent();
            this.mark = mark;
        }
        int mark;

        private void weapons_Load(object sender, EventArgs e)
        {
            var connection = new NpgsqlConnection(Options.connectionString);
            connection.Open();

            var commandString = "SELECT name, type, date_start, date_end, mark FROM pmib6605.weapons";
            var command = new NpgsqlCommand(commandString, connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                if (Convert.ToInt32(reader["mark"]) <= mark)
                {
                    if (Convert.IsDBNull(reader["date_end"]))
                    {
                        dataGridView1.Rows.Add(reader["name"].ToString().Trim(), reader["type"].ToString().Trim(), (reader["date_start"]).ToString().Substring(0, 10), "В применении");
                    }
                    else
                    {
                        dataGridView1.Rows.Add(reader["name"].ToString().Trim(), reader["type"].ToString().Trim(), (reader["date_start"]).ToString().Substring(0, 10), (reader["date_end"]).ToString());
                    }
                }
            }
            if (dataGridView1.Rows.Count == 0)
            {
                dataGridView1.Visible = false;
                MessageBox.Show("Данных еще нет", "Нет информации", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
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
            weapons_Load(sender, e);
        }
    }
}
