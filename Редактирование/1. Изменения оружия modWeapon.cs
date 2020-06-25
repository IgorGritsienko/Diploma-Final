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
    public partial class modWeapon : Form
    {
        public modWeapon(int mark)
        {
            InitializeComponent();
            this.mark = mark;
        }

        readonly int mark;

        List<int> id = new List<int>();       // id из таблицы

        int index;  // индекс строки датагрида, на которую последний раз кликнули

        int tmp;

        private void modWeapon_Load(object sender, EventArgs e)
        {
            // скрыть вспомогательную таблицу и кнопки
            dataGridEditing.Visible = false;
            button4.Visible = false;
            button5.Visible = false;

            refreshTable();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            index = e.RowIndex;

            // нельзя редактировать архивированные записи (списанное оружие)
            if (dataGridEnabledList.Rows[e.RowIndex].Cells[4].Value.ToString() != "В применении")
            {
                button1.ForeColor = Color.Red;
                button2.ForeColor = Color.Red;
            }
            else
            {
                button1.ForeColor = Color.White;
                button2.ForeColor = Color.White;
            }
        }

        private void button1_Click(object sender, EventArgs e)          // списание
        {
            if (button1.ForeColor == Color.Red)
                return;

            tmp = id[index];

            dataGridEditing.Visible = true;
            dataGridEditing.Rows.Add(
                dataGridEnabledList.Rows[index].Cells[0].Value,
                dataGridEnabledList.Rows[index].Cells[1].Value,
                dataGridEnabledList.Rows[index].Cells[2].Value,
                dataGridEnabledList.Rows[index].Cells[3].Value,
                DateTime.Now.Year);

            // все поля заблокированы
            dataGridEditing.Rows[0].Cells[0].ReadOnly = true;
            dataGridEditing.Rows[0].Cells[1].ReadOnly = true;
            dataGridEditing.Rows[0].Cells[2].ReadOnly = true;
            dataGridEditing.Rows[0].Cells[3].ReadOnly = true;
            dataGridEditing.Rows[0].Cells[4].ReadOnly = true;

            button4.Text = "Подтвердите списание";

            button1.ForeColor = Color.Red;
            button2.ForeColor = Color.Red;
            button3.ForeColor = Color.Red;

            button4.Visible = true;
            button5.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)      // изменение
        {
            if (button2.ForeColor == Color.Red)
                return;

            dataGridEditing.ReadOnly = false;
            tmp = id[index];

            dataGridEditing.Visible = true;
            dataGridEditing.Rows.Add(
                dataGridEnabledList.Rows[index].Cells[0].Value,
                dataGridEnabledList.Rows[index].Cells[1].Value,
                dataGridEnabledList.Rows[index].Cells[2].Value,
                dataGridEnabledList.Rows[index].Cells[3].Value,
                dataGridEnabledList.Rows[index].Cells[4].Value);

            dataGridEditing.Rows[0].Cells[0].ReadOnly = false;
            dataGridEditing.Rows[0].Cells[1].ReadOnly = false;
            dataGridEditing.Rows[0].Cells[2].ReadOnly = false;
            dataGridEditing.Rows[0].Cells[3].ReadOnly = true;
            dataGridEditing.Rows[0].Cells[4].ReadOnly = true;

            button4.Text = "Подтвердите изменения";

            button1.ForeColor = Color.Red;
            button2.ForeColor = Color.Red;
            button3.ForeColor = Color.Red;

            button4.Visible = true;
            button5.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)      // добавление
        {
            if (button3.ForeColor == Color.Red)
                return;

            dataGridEditing.Visible = true;
            dataGridEditing.Rows.Add("", "", "", DateTime.Now.Year, "");

            dataGridEditing.Rows[0].Cells[0].ReadOnly = false;
            dataGridEditing.Rows[0].Cells[1].ReadOnly = false;
            dataGridEditing.Rows[0].Cells[2].ReadOnly = false;
            dataGridEditing.Rows[0].Cells[3].ReadOnly = true;
            dataGridEditing.Rows[0].Cells[4].ReadOnly = true;

            button4.Text = "Добавить оружие";

            button1.ForeColor = Color.Red;
            button2.ForeColor = Color.Red;
            button3.ForeColor = Color.Red;

            button4.Visible = true;
            button5.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)      // сброс
        {
            dataGridEditing.Rows.Clear();
            dataGridEditing.Visible = false;
            button1.ForeColor = Color.White;
            button2.ForeColor = Color.White;
            button3.ForeColor = Color.White;

            button4.Visible = false;
            button5.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)      // подтверждение
        {
            //добавляем новую строку в таблицу
            if (button4.Text == "Добавить оружие")
            {
                for (int i = 0; i < 3; i++)
                {
                    if (string.IsNullOrEmpty(dataGridEditing.Rows[0].Cells[i].Value as string))
                    {
                        dataGridEditing.Rows[0].Cells[i].Value = "";
                    }
                    if (dataGridEditing.Rows[0].Cells[i].Value.ToString() == "")
                    {
                        MessageBox.Show("Заполните все поля, кроме даты списания", "Предупрежедние", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                using (var connection = new NpgsqlConnection(Options.connectionString))
                {
                    connection.Open();
                    var commandString = "SELECT max(id)" +
                                        "FROM pmib6605.weapons";

                    int maxID;  // максимальный занятый id
                    var command = new NpgsqlCommand(commandString, connection);
                    maxID = Convert.ToInt32(command.ExecuteScalar()) + 1;

                    commandString = "SELECT name " +
                                    "FROM pmib6605.weapons";
                    command = new NpgsqlCommand(commandString, connection);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader.GetString(0).Trim() == dataGridEditing.Rows[0].Cells[0].Value.ToString().Trim())
                        {
                            MessageBox.Show("Данное оружие уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    reader.Close();


                    commandString = "INSERT INTO pmib6605.weapons (id, name, type, ammo, date_start, mark) " +
                                    "VALUES (@id, @name, @type, @ammo, @date_start, @mark)";

                    command = new NpgsqlCommand(commandString, connection);

                    command.Parameters.AddWithValue("id", maxID);
                    command.Parameters.AddWithValue("name", dataGridEditing.Rows[0].Cells[0].Value);
                    command.Parameters.AddWithValue("type", dataGridEditing.Rows[0].Cells[1].Value);
                    command.Parameters.AddWithValue("ammo", dataGridEditing.Rows[0].Cells[2].Value);
                    command.Parameters.AddWithValue("date_start", DateTime.ParseExact(dataGridEditing.Rows[0].Cells[3].Value.ToString(), "yyyy", null));
                    command.Parameters.AddWithValue("mark", mark);

                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Добавлено", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);

                refreshTable();
            }

            else if (button4.Text == "Подтвердите списание")
            {
                using (var connection = new NpgsqlConnection(Options.connectionString))
                {
                    connection.Open();
                    var commandString = "UPDATE pmib6605.weapons " +
                                        "SET date_end = @date_end " +
                                        "WHERE id = @id";

                    var command = new NpgsqlCommand(commandString, connection);

                    command.Parameters.AddWithValue("id", tmp);
                    command.Parameters.AddWithValue("date_end", DateTime.ParseExact(dataGridEditing.Rows[0].Cells[4].Value.ToString(), "yyyy", null));

                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Изменено", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refreshTable();
            }

            else if (button4.Text == "Подтвердите изменения")
            {
                for (int i = 0; i < 3; i++)
                {
                    if (string.IsNullOrEmpty(dataGridEditing.Rows[0].Cells[i].Value as string))
                    {
                        MessageBox.Show("Заполните все поля, кроме дат списания и поставки на вооружение", "Предупрежедние", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                using (var connection = new NpgsqlConnection(Options.connectionString))
                {
                    connection.Open();
                    var commandString = "UPDATE pmib6605.weapons " +
                                        "SET name = @name, type = @type, ammo = @ammo, mark = @mark " +
                                        "WHERE id = @id";

                    var command = new NpgsqlCommand(commandString, connection);

                    command.Parameters.AddWithValue("id", tmp);
                    command.Parameters.AddWithValue("name", dataGridEditing.Rows[0].Cells[0].Value);
                    command.Parameters.AddWithValue("type", dataGridEditing.Rows[0].Cells[1].Value);
                    command.Parameters.AddWithValue("ammo", dataGridEditing.Rows[0].Cells[2].Value);
                    command.Parameters.AddWithValue("mark", mark);

                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Изменено", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refreshTable();
            }

            //сделать основные кнопки активными, очистить вспомогательную таблицу
            dataGridEditing.Rows.Clear();
            dataGridEditing.Visible = false;

            button1.ForeColor = Color.White;
            button2.ForeColor = Color.White;
            button3.ForeColor = Color.White;

            button4.Visible = false;
            button5.Visible = false;
        }

        private void refreshTable()
        {   //заполнить основную таблицу          

            dataGridEnabledList.Rows.Clear();
            using (var connection = new NpgsqlConnection(Options.connectionString))
            {
                connection.Open();
                var commandString = "SELECT id, name, type, ammo, date_start, date_end, mark FROM pmib6605.weapons";
                var command = new NpgsqlCommand(commandString, connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if (reader.GetInt32(6) == mark)
                    {
                        if (reader.IsDBNull(5))
                        {
                            dataGridEnabledList.Rows.Add(reader.GetString(1).Trim(), reader.GetString(2).Trim(), reader.GetString(3).Trim(), reader.GetDate(4).Year, "В применении");             //)(reader["date_start"]).ToString().Substring(6,4), "В применении");
                        }
                        else
                        {
                            dataGridEnabledList.Rows.Add(reader.GetString(1).Trim(), reader.GetString(2).Trim(), reader.GetString(3).Trim(), reader.GetDate(4).Year, (reader.GetDate(5).Year));
                        }
                        id.Add(reader.GetInt32(0));
                    }
                }
                reader.Close();
            }

            if (dataGridEnabledList.Rows.Count == 0)
            {
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = true;              
                MessageBox.Show("Доступных для редактирования Вам данных не обнаружено.", "Нет данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                button1.Visible = true;
                button2.Visible = true;
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
