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
    public partial class supply : Form
    {
        public supply(int mark)
        {
            InitializeComponent();
            this.mark = mark;
        }
        int mark;

        struct supplies // id и название припасов
        {
            public int id;
            public string name;
        };
        List<supplies> sps = new List<supplies>();

        private void supply_Load(object sender, EventArgs e)    
        {   // заполнение comboBox1
            dataGridView1.Visible = false;

            using (var conn = new NpgsqlConnection(Options.connectionString))
            {
                conn.Open();
                var commStr = "SELECT id, name, mark FROM pmib6605.supplies";
                var comm = new NpgsqlCommand(commStr, conn);
                var reader = comm.ExecuteReader();
                while(reader.Read())
                {
                    if (reader.GetInt32(2) <= mark)
                    {
                        supplies sp = new supplies
                        {
                            id = reader.GetInt32(0),
                            name = reader.GetString(1)
                        };
                        sps.Add(sp);
                        comboBox1.Items.Add(reader.GetString(1));
                    }
                }                              
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {   // заполнение dgv
            dataGridView1.Visible = true;
            dataGridView1.Rows.Clear();

            using (var conn = new NpgsqlConnection(Options.connectionString))
            {
                conn.Open();
                var commStr = "SELECT a.name, a.hname, military_headquarters.name, date_sent, date_came, weight, priority " +
                              "FROM ( " +
                                    "SELECT supply.name, military_headquarters.name as hname, _to, date_sent, date_came, weight, priority " +
                                    "FROM pmib6605.supply " +
                                            "JOIN pmib6605.military_headquarters on military_headquarters.id = supply._from " +
                                            "JOIN pmib6605.supplies on supplies.id = supply.item " +
                                            "WHERE supplies.name = '" + comboBox1.Text.Trim() + "') a " +
                              "JOIN pmib6605.military_headquarters on military_headquarters.id = a._to "; 
                              
                var comm = new NpgsqlCommand(commStr, conn);
                var reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.IsDBNull(4))
                    {
                        dataGridView1.Rows.Add(reader.GetString(0).Trim(), reader.GetString(1).Trim(), reader.GetString(2).Trim(), reader.GetDate(3),
                                  "В процессе доставки", reader.GetInt32(5), reader.GetString(6).Trim());
                    }
                    else
                    {
                        dataGridView1.Rows.Add(reader.GetString(0).Trim(), reader.GetString(1).Trim(), reader.GetString(2).Trim(), reader.GetDate(3),
                                    reader.GetDate(4), reader.GetInt32(5), reader.GetString(6).Trim());
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

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1_SelectionChangeCommitted(sender, e);
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

        private void выйтиИзАккантаToolStripMenuItem_Click(object sender, EventArgs e)
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
