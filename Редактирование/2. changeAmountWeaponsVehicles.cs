using Npgsql;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Diploma
{
    public partial class changeAmountWeaponsVehicles : Form
    {
        public changeAmountWeaponsVehicles(int mark)
        {
            InitializeComponent();
            this.mark = mark;
        }
        bool change = false;
        readonly int mark;
        private readonly List<string> containerID = new List<string>();
        private readonly Dictionary<string, int> weapons = new Dictionary<string, int>();
        private readonly Dictionary<string, int> vehicles = new Dictionary<string, int>();
        int index1;
        int index2;



        private void changeAmountWeaponsVehicles_Load(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            numericUpDown1.Visible = false;
            numericUpDown2.Visible = false;
            index1 = 0;
            index2 = 0;

            save.Visible = false;
            cancel.Visible = false;

            var connection = new NpgsqlConnection(Options.connectionString);
            connection.Open();

            var commandString = "SELECT id, name, mark " +
                                "FROM pmib6605.coy";


            using (var command = new NpgsqlCommand(commandString, connection))
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetInt32(2) == mark)
                    {
                        comboBox1.Items.Add(reader.GetString(1).Trim());
                        containerID.Add(reader.GetString(1).Trim());
                    }
                }
                reader.Close();
            }
            connection.Close();

            if (comboBox1.Items.Count == 0)
            {
                MessageBox.Show("В настоящее время у Вас нет доступных для редактирования рот", "Предупрежедние", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                назадToolStripMenuItem_Click(sender, e);
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {       
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            numericUpDown1.Visible = false;
            numericUpDown2.Visible = false;

            save.Visible = true;
            cancel.Visible = true;

            using (var connection = new NpgsqlConnection(Options.connectionString))
            { connection.Open();
                var commandString = "SELECT weapons.id, weapons.name, coy_weapons.amount, weapons.mark " +
                                    "FROM pmib6605.coy_weapons " +
                                            "join pmib6605.weapons on weapons.id = coy_weapons.weapon " +
                                    "WHERE coy_weapons.coy_id = " +
                                                                "(SELECT id FROM pmib6605.coy WHERE name = @name)";

                var command = new NpgsqlCommand(commandString, connection);
                command.Parameters.AddWithValue("name", comboBox1.Text);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetInt32(3) <= mark)
                    {
                        dataGridView1.Rows.Add(reader.GetString(1).Trim(), reader.GetInt32(2));
                        if (!weapons.ContainsKey(reader.GetString(1).Trim()))
                            weapons.Add(reader.GetString(1).Trim(), reader.GetInt32(0));
                    }
                }
                reader.Close();


                commandString = "SELECT combat_vehicles.id, combat_vehicles.name, coy_vehicles.amount, combat_vehicles.mark " +
                                    "FROM pmib6605.coy_vehicles " +
                                            "join pmib6605.combat_vehicles on combat_vehicles.id = coy_vehicles.vehicle " +
                                    "WHERE coy_vehicles.coy_id = " +
                                                                "(SELECT id FROM pmib6605.coy WHERE name = @name)";

                command = new NpgsqlCommand(commandString, connection);
                command.Parameters.AddWithValue("name", comboBox1.Text);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetInt32(3) <= mark)
                    {
                        dataGridView2.Rows.Add(reader.GetString(1).Trim(), reader.GetInt32(2));
                        if (!vehicles.ContainsKey(reader.GetString(1).Trim()))
                            vehicles.Add(reader.GetString(1).Trim(), reader.GetInt32(0));
                    }
                }
                reader.Close();
            }

            bool warning = false;

            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Некоторые данные Вам недоступны или отсутствуют", "Предупрежедние", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                warning = true;
            }
            else
            {
                dataGridView1.Visible = true;
                numericUpDown1.Visible = true;
                numericUpDown1.Value = Convert.ToDecimal(dataGridView1.Rows[0].Cells[1].Value);
            }

            if (dataGridView2.Rows.Count == 0)
            {
                if (!warning)
                {
                    MessageBox.Show("Некоторые данные Вам недоступны или отсутствуют", "Предупрежедние", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                dataGridView2.Visible = true;
                numericUpDown2.Visible = true;
                numericUpDown2.Value = Convert.ToDecimal(dataGridView2.Rows[0].Cells[1].Value);
            }
            change = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            index1 = e.RowIndex;
            numericUpDown1.Value = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
            change = true;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            index2 = e.RowIndex;
            numericUpDown2.Value = Convert.ToDecimal(dataGridView2.Rows[e.RowIndex].Cells[1].Value);
            change = true;
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Сохранятся только данные на этой выкладке. Вы хотите продолжить?", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (var connection = new NpgsqlConnection(Options.connectionString))
                {
                    connection.Open();
                    var commandString = "UPDATE pmib6605.coy_weapons " +
                                        "SET amount = @amount " +
                                        "WHERE coy_id = @cont_ID and weapon = @weapon";

                    var command = new NpgsqlCommand(commandString, connection);
                    command.Parameters.AddWithValue("amount", dataGridView1.Rows[index1].Cells[1].Value);
                    command.Parameters.AddWithValue("cont_ID", containerID.IndexOf(comboBox1.Text.Trim()) + 1);
                    command.Parameters.AddWithValue("weapon", weapons[dataGridView1.Rows[index1].Cells[0].Value.ToString().Trim()]);
                    command.ExecuteNonQuery();

                    commandString = "UPDATE pmib6605.coy_vehicles " +
                                       "SET amount = @amount " +
                                       "WHERE coy_id = @cont_ID and vehicle = @vehicle";

                    command = new NpgsqlCommand(commandString, connection);
                    command.Parameters.AddWithValue("amount", dataGridView2.Rows[index2].Cells[1].Value);
                    command.Parameters.AddWithValue("cont_ID", containerID.IndexOf(comboBox1.Text.Trim()) + 1);
                    command.Parameters.AddWithValue("vehicle", vehicles[dataGridView2.Rows[index2].Cells[0].Value.ToString().Trim()]);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            comboBox1_SelectionChangeCommitted(sender, e);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (index1 > dataGridView1.Rows.Count - 1)
            {
                dataGridView1.Rows[0].Cells[1].Value = numericUpDown1.Value;
                return;
            }
            dataGridView1.Rows[index1].Cells[1].Value = numericUpDown1.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (index2 > dataGridView1.Rows.Count - 1)
            {
                dataGridView2.Rows[0].Cells[1].Value = numericUpDown2.Value;
                return;
            }
            dataGridView2.Rows[index2].Cells[1].Value = numericUpDown2.Value;
        }

        private void главноеМенюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main open = new Main(mark);
            open.Show();
            this.Close();
        }

        private void назадToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Modification open = new Modification(mark);
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
    }
}