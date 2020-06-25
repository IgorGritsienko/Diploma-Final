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
    public partial class showRanks : Form
    {
        public showRanks(int mark)
        {
            InitializeComponent();
            this.mark = mark;
        }
        int mark;

        private void showRanks_Load(object sender, EventArgs e)
        {
            var connection = new NpgsqlConnection(Options.connectionString);
            connection.Open();

            var commandString = "SELECT rank, type FROM pmib6605.military_rank";
            var command = new NpgsqlCommand(commandString, connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                dataGridView1.Rows.Add(Convert.ToInt32(reader["type"]), reader["rank"].ToString().Trim());
            }

            reader.Close();

            commandString = "SELECT id, rank FROM pmib6605.ranks";
            command = new NpgsqlCommand(commandString, connection);
            reader = command.ExecuteReader();

            int i = 1;
            int type;

            // добавить ранги в таблицу, 1 ранг на несколько званий
            reader.Read();
            type = Convert.ToInt32(dataGridView1.Rows[0].Cells[0].Value);

            dataGridView1.Rows[0].Cells[0].Value = reader["rank"].ToString().Trim();
            while (reader.Read())
            {
                for (; i < dataGridView1.RowCount; i++)
                {
                    if (type == Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value))
                    {
                        dataGridView1.Rows[i].Cells[0].Value = "";
                        continue;
                    }
                    else
                    {
                        type = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                        dataGridView1.Rows[i].Cells[0].Value = reader["rank"].ToString().Trim();
                        i++;
                        break;
                    }
                }
            }
            reader.Close();
            connection.Close();
            // обнулить последние строки после последнего ранга
            for (; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = "";
            }
            if (dataGridView1.Rows.Count == 0)
            {
                dataGridView1.Visible = false;
                MessageBox.Show("Данных, доступных для Вас, еще нет", "Нет информации", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else
            {
                dataGridView1.Visible = true;
            }
        }

        // в Main
        private void главноеМенюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main open = new Main(mark);
            open.Show();
            this.Close();
        }

        // в selectRO
        private void назадToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectRO open = new selectRO(mark);
            open.Show();
            this.Close();
        }

        // во Вход
        private void выйтиИзАккаунтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены?", "Выход из аккаунта", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Login open = new Login();
                open.Show();
                this.Close();
            }           
        }
    }
}
