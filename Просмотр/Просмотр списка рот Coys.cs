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
    public partial class Coys : Form
    {
        public Coys(int mark)
        {
            InitializeComponent();
            this.mark = mark;
        }

        int mark;
        Dictionary <string, int> containerID = new Dictionary <string, int>(); // хранение id роты

        // состояние кнопки показать-скрыть
        bool button1Show = false;
        bool button2Show = false;
        bool button3Show = false;
        private void Coys_Load(object sender, EventArgs e)
        {
            dataGridComposition.Visible = false;
            dataGridComposition.Enabled = false;
            dataGridWeapons.Visible = false;
            dataGridWeapons.Enabled = false;
            dataGridVehicles.Visible = false;
            dataGridVehicles.Enabled = false;

            showComposition.Visible = false;
            showWeapons.Visible = false;
            showVehicles.Visible = false;

            showComposition.Enabled = false;
            showWeapons.Enabled = false;
            showVehicles.Enabled = false;

            using (var connection = new NpgsqlConnection(Options.connectionString)) // считывание основной информации с названиями рот
            {
                connection.Open();
                // для вывода первой таблицы
                var commandString = "SELECT a.name, subjects.name, a.mark, a.id " +
                                    "FROM pmib6605.subjects " +
                                                "join ( " +
                                             "SELECT id, name, commander, mark " +
                                             "FROM pmib6605.coy" +
                                                      ") a on a.commander = subjects.id";

                var command = new NpgsqlCommand(commandString, connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetInt32(2) <= mark)     // если удовлетворяет метки безопасности
                    {
                        dataGridCoys.Rows.Add(reader.GetString(0).Trim(), reader.GetString(1).Trim());
                        containerID.Add(reader.GetString(0).Trim(), reader.GetInt32(3));
                    }
                }
                reader.Close();
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            var index = containerID[dataGridCoys.Rows[e.RowIndex].Cells[0].Value.ToString().Trim()];    // значение id роты по выбранной строке таблицы
            dataGridComposition.Rows.Clear();
            dataGridWeapons.Rows.Clear();
            dataGridVehicles.Rows.Clear();

            using (var connection = new NpgsqlConnection(Options.connectionString))
            {
                connection.Open();

                // для вывода первой дополнительной таблицы
                var commandString = "SELECT subjects.name, birthday, b.rank " +
                                    "FROM pmib6605.subjects " +
                                               "join (SELECT id, rank " +
                                                     "FROM pmib6605.military_rank) b on b.id = subjects.id " +
                                               "join pmib6605.coy_composition on subjects.id = coy_composition.subject " +
                                               "join pmib6605.coy on coy_composition.coy_id = coy.id " +
                                               "WHERE coy_composition.coy_id = @ind";

                var command = new NpgsqlCommand(commandString, connection);
                command.Parameters.AddWithValue("ind", index);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    dataGridComposition.Rows.Add(reader.GetString(0).Trim(), reader.GetDate(1), reader.GetString(2).Trim());
                }
                reader.Close();
                if (dataGridComposition.Rows.Count == 0)
                {
                    showComposition.Visible = false;
                    dataGridComposition.Visible = false;
                    MessageBox.Show("В подразделении нет служащих.", "Нет данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    showComposition.Visible = true;
                    showComposition.Enabled = true;
                }

                // для вывода второй дополнительной таблицы
                commandString = "SELECT weapons.name, weapons.type, a.amount, weapons.mark " +
                                "FROM pmib6605.weapons " +
                                            "join ( " +
                                     "SELECT weapon, amount " +
                                     "FROM pmib6605.coy_weapons " +
                                            "join pmib6605.coy on coy.id = coy_weapons.coy_id " +
                                     "WHERE coy.id = @ind) a on weapons.id = a.weapon";

                command = new NpgsqlCommand(commandString, connection);
                command.Parameters.AddWithValue("ind", index);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetInt32(3) <= mark)
                    {
                        dataGridWeapons.Rows.Add(reader.GetString(0).Trim(), reader.GetString(1).Trim(), reader.GetInt32(2));
                    }
                }
                reader.Close();

                if (dataGridWeapons.Rows.Count == 0)
                {
                    showWeapons.Visible = false;
                    dataGridComposition.Visible = false;
                    MessageBox.Show("Подразделение не обладает оружием.", "Нет данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    showWeapons.Visible = true;
                    showWeapons.Enabled = true;
                }

                // для вывода третьей дополнительной таблицы
                commandString = "SELECT combat_vehicles.name, combat_vehicles.type, a.amount, vehicles_attributes.seats, vehicles_attributes.ammunition, combat_vehicles.mark " +
                                "FROM pmib6605.combat_vehicles " +
                                                "join ( " +
                                        "SELECT vehicle, amount " +
                                         "FROM pmib6605.coy_vehicles " +
                                                        "join pmib6605.coy on coy.id = coy_vehicles.coy_id  " +
                                         "WHERE coy.id = @ind " +
                                                    ") a on combat_vehicles.id = a.vehicle  " +
                                         "join pmib6605.vehicles_attributes on vehicles_attributes.name = combat_vehicles.id";

                command = new NpgsqlCommand(commandString, connection);
                command.Parameters.AddWithValue("ind", index);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetInt32(5) <= mark)
                    {
                        if (Convert.IsDBNull(reader["ammunition"]))
                        {
                            dataGridVehicles.Rows.Add(reader.GetString(0).Trim(), reader.GetString(1).Trim(), reader.GetInt32(2), reader.GetInt32(3), "Отсутствует");
                        }
                        else
                        {
                            dataGridVehicles.Rows.Add(reader.GetString(0).Trim(), reader.GetString(1).Trim(), reader.GetInt32(2), reader.GetInt32(3), reader.GetString(4).Trim());
                        }
                    }
                }
                reader.Close();

                if (dataGridVehicles.Rows.Count == 0)
                {
                    showVehicles.Visible = false;
                    dataGridVehicles.Visible = false;
                    MessageBox.Show("У подразделения отсутствует военная техника.", "Нет данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    showVehicles.Visible = true;
                    showVehicles.Enabled = true;
                }
            }
        }

        private void showComposition_Click(object sender, EventArgs e)
        {
            if (!button1Show)
            {
                showComposition.Text = "Скрыть состав роты";
                dataGridComposition.Visible = true;
                dataGridComposition.Enabled = true;
                button1Show = true;
                showComposition.Visible = true;
                showComposition.Enabled = true;
            }
            else
            {
                showComposition.Text = "Показать состав роты";
                dataGridComposition.Visible = false;
                dataGridComposition.Enabled = false;
                button1Show = false;
            }
        }

        private void showWeapons_Click(object sender, EventArgs e)
        {
            if (!button2Show)
            {
                showWeapons.Text = "Скрыть информацию об оружии роты";
                dataGridWeapons.Visible = true;
                dataGridWeapons.Enabled = true;
                button2Show = true;
                showWeapons.Visible = true;
                showWeapons.Enabled = true;
            }
            else
            {
                showWeapons.Text = "Показать инфомрацию об оружии роты";
                dataGridWeapons.Visible = false;
                dataGridWeapons.Enabled = false;
                button2Show = false;             
            }
        }

        private void showVehicles_Click(object sender, EventArgs e)
        {
            if (!button3Show)
            {
                showVehicles.Text = "Скрыть информацию о военной техники роты";
                dataGridVehicles.Visible = true;
                dataGridVehicles.Enabled = true;
                button3Show = true;
                showVehicles.Visible = true;
                showVehicles.Enabled = true;
            }
            else
            {
                showVehicles.Text = "Показать информацию о военной техники роты";
                dataGridVehicles.Visible = false;
                dataGridVehicles.Enabled = false;
                button3Show = false;
            }
        }
        private void mainMenu_Click(object sender, EventArgs e)
        {
            Main open = new Main(mark);
            open.Show();
            this.Close();
        }

        private void back_Click(object sender, EventArgs e)
        {
            selectRO open = new selectRO(mark);
            open.Show();
            this.Close();
        }

        private void changeUser_Click(object sender, EventArgs e)
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

