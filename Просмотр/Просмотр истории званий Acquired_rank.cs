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
    public partial class Acquired_rank : Form
    {
        public Acquired_rank(int mark)
        {
            InitializeComponent();
            this.mark = mark;
        }
        int mark;

        private void Aquired_rank_Load(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            using (var conn = new NpgsqlConnection(Options.connectionString))
            {
                conn.Open();
                var commStr = "SELECT name, mark, id " +
                              "FROM pmib6605.subjects";
                var comm = new NpgsqlCommand(commStr, conn);

                var reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetInt32(1) <= mark)
                    {
                        comboBox1.Items.Add(reader.GetString(0).Trim());
                    }
                }
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {      
            dataGridView1.Rows.Clear();
            using (var conn = new NpgsqlConnection(Options.connectionString))
            {
                conn.Open();
                var commStr = "SELECT military_rank.rank, obtaining_date " +
                               "FROM pmib6605.acquired_rank " +
                                        "JOIN pmib6605.subjects on subjects.id = acquired_rank.name " +
                                         "JOIN pmib6605.military_rank on military_rank.id = acquired_rank.rank_obtained " + 
                               "WHERE subjects.name = " + "'" + comboBox1.Text.Trim() + "'";

                var comm = new NpgsqlCommand(commStr, conn);

                var reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader.GetString(0).Trim(), reader.GetDate(1));
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
            comboBox1_SelectionChangeCommitted(sender, e);
        }
    }
}
