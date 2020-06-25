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
    public partial class vehicles : Form
    {
        public vehicles(int mark)
        {
            InitializeComponent();
            this.mark = mark;
        }
        int mark;

        private void vehicles_Load(object sender, EventArgs e)
        {
            var connection = new NpgsqlConnection(Options.connectionString);
            connection.Open();

            var commandString = "SELECT b.name, b.type, b.seats, buildings.target, b.ammunition, b.mark " +
                                "FROM pmib6605.buildings " +
                                    "join( " +
                                 "SELECT CV.name, CV.type, VA.seats, VA.places, VA.ammunition, CV.mark " +
                                 "FROM pmib6605.vehicles_attributes VA " +
                                        "join (SELECT id, name, type, mark " +
                                           "FROM pmib6605.combat_vehicles) CV on CV.id = VA.name ) " +
                                 "b on buildings.id = b.places";

            var command = new NpgsqlCommand(commandString, connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                if (Convert.ToInt32(reader["mark"]) <= mark)
                {
                    if (Convert.IsDBNull(reader["ammunition"]))
                    {
                        dataGridView1.Rows.Add(reader["name"].ToString().Trim(),
                          reader["type"].ToString().Trim(),
                          Convert.ToInt32(reader["seats"]),
                          reader["target"].ToString().Trim(),
                          "Отсутствует");
                    }
                    else
                    {
                        dataGridView1.Rows.Add(reader["name"].ToString().Trim(),
                          reader["type"].ToString().Trim(),
                          Convert.ToInt32(reader["seats"]),
                          reader["target"].ToString().Trim(),
                          reader["ammunition"].ToString().Trim());                       
                    }
                }
            }
            reader.Close();
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
            vehicles_Load(sender, e);
        }
    }
}
