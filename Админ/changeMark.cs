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

namespace Diploma.Админ
{
    public partial class changeMark : Form
    {
        public changeMark()
        {
            InitializeComponent();
        }
        Dictionary<string, int> map = new Dictionary<string, int>();
        private void changeMark_Load(object sender, EventArgs e)
        {
            label1.Visible = false;
            numericUpDown1.Visible = false;
            textBox1.Visible = false;

            button1.Visible = false;
            button2.Visible = false;

            using (var conn = new NpgsqlConnection(Options.connectionString))   // заполнить выпадающий список и создать пары <ТАБЛИЦА - МЕТКА>
            {
                conn.Open();
                var commStr = "SELECT  table_name, marks FROM pmib6605.table_marks";
                var comm = new NpgsqlCommand(commStr, conn);
                var reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader.GetString(0).Trim());
                    map.Add(reader.GetString(0).Trim(), reader.GetInt32(1));
                }
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            using(var conn = new NpgsqlConnection(Options.connectionString)) 
            {
                conn.Open();
                var commStr = "SELECT min(mark) FROM pmib6605." + comboBox1.Text.Trim();
                var comm = new NpgsqlCommand(commStr, conn);
                //comm.Parameters.AddWithValue("table", "pmib6605." + comboBox1.Text.Trim());
                var result = Convert.ToInt32(comm.ExecuteScalar());
                label1.Text = "Минимальный допустимый уровень: " + result;
                numericUpDown1.Minimum = result;
            }
            numericUpDown1.Value = map[comboBox1.Text.Trim()];
            numericUpDown1_ValueChanged(sender, e);


            label1.Visible = true;
            numericUpDown1.Visible = true;
            textBox1.Visible = true;

            button1.Visible = true;
            button2.Visible = true;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)        // изменение текста при изменении значения numericUpDown
        {
            using (var conn = new NpgsqlConnection(Options.connectionString))
            {
                conn.Open();
                var commStr = "SELECT name " +
                              "FROM pmib6605.marks " +
                              "WHERE mark = @mk";

                var comm = new NpgsqlCommand(commStr, conn);
                comm.Parameters.AddWithValue("mk", numericUpDown1.Value);
                var result = comm.ExecuteScalar();
                textBox1.Text = result.ToString().Trim();
            }
        }

        private void button1_Click(object sender, EventArgs e)      // сохранение
        {
            using (var conn = new NpgsqlConnection(Options.connectionString))
            {
                conn.Open();
                var commStr = "UPDATE pmib6605.marks " + 
                              "SET mark = @value " +
                              "WHERE name = @name";

                var comm = new NpgsqlCommand(commStr, conn);
                comm.Parameters.AddWithValue("value", numericUpDown1.Value);
                comm.Parameters.AddWithValue("name", comboBox1.Text.Trim());
                comm.ExecuteNonQuery();


                // увеличиваем все метки в таблице до установленного минимального
                commStr = "UPDATE pmib6605." + "name" +
                          "SET mark = @value " +
                          "WHERE mark < @value";
                comm = new NpgsqlCommand(commStr, conn);
                comm.Parameters.AddWithValue("value", numericUpDown1.Value);
                comm.ExecuteNonQuery();

            }
            MessageBox.Show("Обновлено", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
            button2_Click(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)      // отмена
        {
            label1.Visible = false;
            numericUpDown1.Visible = false;
            textBox1.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            comboBox1.SelectedIndex = -1;
        }

        private void back_Click(object sender, EventArgs e)
        {
            Admin open = new Admin();
            open.Show();
            this.Hide();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Login open = new Login();
            open.Show();
            this.Hide();
        }
    }
}
