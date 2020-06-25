using Npgsql;
using System;
using System.Windows.Forms;

namespace Diploma
{
    public partial class transfer : Form
    {
        public transfer(int mark)
        {
            InitializeComponent();
            this.mark = mark;
        }

        readonly int mark;

        // индекс выбранной строки
        int index1 = -1;
        int index2 = -1;

        private void transfer_Load(object sender, EventArgs e)
        {
            comboBoxRight.Visible = false;
            dataGridLeft.Visible = false;
            dataGridRight.Visible = false;
            leftToRight.Visible = false;
            rightToLeft.Visible = false;

            // заполнение первого списка
            using (var connection = new NpgsqlConnection(Options.connectionString))
            {
                connection.Open();

                var commandString = "SELECT id, name, mark " +
                    "FROM pmib6605.military_headquarters";

                var command = new NpgsqlCommand(commandString, connection);
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader.GetInt32(2) <= mark)
                        {
                            comboBoxLeft.Items.Add(reader.GetString(1).Trim());
                        }
                    }
                }
            }
        }

        private void comboBoxLeft_SelectionChangeCommitted(object sender, EventArgs e)
        {
            comboBoxRight.Visible = true;
            dataGridLeft.Visible = true;
            dataGridLeft.Rows.Clear();
            dataGridRight.Rows.Clear();
            comboBoxRight.Items.Clear();

            using (var connection = new NpgsqlConnection(Options.connectionString))
            {
                connection.Open();

                var commandString = "SELECT rocket_launcher.id, rocket_launcher.name, rockets.name, rocket_launcher.mark " +
                                    "FROM pmib6605.rocket_launcher " +
                                    "JOIN pmib6605.military_headquarters on rocket_launcher.headquarters = military_headquarters.id " +
                                    "JOIN pmib6605.rockets on rockets.id = rocket_launcher.rockets_type " +
                                    "WHERE military_headquarters.name = @name";

                var command = new NpgsqlCommand(commandString, connection);
                command.Parameters.AddWithValue("name", comboBoxLeft.Text.Trim());

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetInt32(3) <= mark)
                    {
                        dataGridLeft.Rows.Add(reader.GetString(1).Trim(), reader.GetString(2).Trim());
                    }
                }
            }

            foreach (var item in comboBoxLeft.Items)       // заполняем второй выпадающий список всеми элементами,                                                        
            {                                           // кроме выбранного в первом списке
                comboBoxRight.Items.Add(item);
            }
            comboBoxRight.Items.Remove(comboBoxLeft.Text);
            // устанавливаем, что нет выбранных строк
            index1 = -1;
            index2 = -1;
        }

        private void comboBoxRight_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dataGridRight.Visible = true;
            dataGridRight.Rows.Clear();
            using (var connection = new NpgsqlConnection(Options.connectionString))
            {
                connection.Open();
                var commandString = "SELECT rocket_launcher.id, rocket_launcher.name, rockets.name, rocket_launcher.mark " +
                                    "FROM pmib6605.rocket_launcher " +
                                    "JOIN pmib6605.military_headquarters on rocket_launcher.headquarters = military_headquarters.id " +
                                    "JOIN pmib6605.rockets on rockets.id = rocket_launcher.rockets_type " +
                                    "WHERE military_headquarters.name = @name";

                var command = new NpgsqlCommand(commandString, connection);
                command.Parameters.AddWithValue("name", comboBoxRight.Text);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetInt32(3) <= mark)   
                    {
                        dataGridRight.Rows.Add(reader.GetString(1).Trim(), reader.GetString(2).Trim());
                    }
                }
            }
            leftToRight.Visible = true;
            rightToLeft.Visible = true;
            index1 = -1;
            index2 = -1;
        }

        private void leftToRight_Click(object sender, EventArgs e)
        {
            if (index1 == -1)
                return;

            // добавить выбранную запись в противоположную таблицу
            dataGridRight.Rows.Add(dataGridLeft.Rows[index1].Cells[0].Value, dataGridLeft.Rows[index1].Cells[1].Value);
            using (var connection = new NpgsqlConnection(Options.connectionString))
            {
                connection.Open();
                var commandString = "UPDATE pmib6605.rocket_launcher " +
                                    "SET headquarters = (SELECT distinct rocket_launcher.headquarters " +
                                                        "FROM pmib6605.military_headquarters " +
                                                        "JOIN pmib6605.rocket_launcher on rocket_launcher.headquarters = military_headquarters.id " +
                                                        "WHERE military_headquarters.name = @headq_name) " +
                                    "WHERE rocket_launcher.name = @rocketlaucner_name";

                var command = new NpgsqlCommand(commandString, connection);
                command.Parameters.AddWithValue("headq_name", comboBoxRight.Text.Trim());
                command.Parameters.AddWithValue("rocketlaucner_name", dataGridLeft.Rows[index1].Cells[0].Value.ToString().Trim());
                command.ExecuteNonQuery();

                // удалить выбранную запись из текущей таблицы
                dataGridLeft.Rows.RemoveAt(index1);
                dataGridLeft.Refresh();
                index1 = -1;
            }
        }

        private void rightToLeft_Click(object sender, EventArgs e)
        {
            if (index2 == -1)
                return;

            dataGridLeft.Rows.Add(dataGridRight.Rows[index2].Cells[0].Value, dataGridRight.Rows[index2].Cells[1].Value);

            using (var connection = new NpgsqlConnection(Options.connectionString))
            {
                connection.Open();

                var commandString = "UPDATE pmib6605.rocket_launcher " +
                                    "SET headquarters = (SELECT distinct rocket_launcher.headquarters " +
                                                        "FROM pmib6605.military_headquarters " +
                                                        "JOIN pmib6605.rocket_launcher on rocket_launcher.headquarters = military_headquarters.id " +
                                                        "WHERE military_headquarters.name = @headq_name) " +
                                    "WHERE rocket_launcher.name = @rocketlaucner_name";

                var command = new NpgsqlCommand(commandString, connection);
                command.Parameters.AddWithValue("headq_name", comboBoxLeft.Text.Trim());
                command.Parameters.AddWithValue("rocketlaucner_name", dataGridRight.Rows[index2].Cells[0].Value.ToString().Trim());
                command.ExecuteNonQuery();

                dataGridRight.Rows.RemoveAt(index2);
                dataGridRight.Refresh();
                index2 = -1;
            }
        }

        private void dataGridViewLeft_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index1 = e.RowIndex;    // при нажатии на ячейку таблицы запоминается номер строки
        }

        private void dataGridViewRight_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index2 = e.RowIndex;
        }

        private void mainMenu_Click(object sender, EventArgs e)
        {
            Main open = new Main(mark);
            open.Show();
            this.Close();
        }

        private void back_Click(object sender, EventArgs e)
        {
            Modification open = new Modification(mark);
            open.Show();
            this.Close();
        }

        private void changeAccount_Click(object sender, EventArgs e)
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
