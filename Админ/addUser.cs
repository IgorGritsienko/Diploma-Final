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
    public partial class addUser : Form
    {
        public addUser()
        {
            InitializeComponent();
        }

        List<string> rank = new List<string>();     // хранение id ранга
        Dictionary<string, int> map = new Dictionary<string, int>(); // соответствие ранга и звания

        private void addUser_Load(object sender, EventArgs e)
        {
            // заполняем выпадающий список
            using (var conn = new NpgsqlConnection(Options.connectionString))
            {
                conn.Open();
                var commStr = "SELECT rank FROM pmib6605.ranks";
                var comm = new NpgsqlCommand(commStr, conn);
                var reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    comboBoxGeneralRank.Items.Add(reader.GetString(0).Trim());
                    rank.Add(reader.GetString(0).Trim());
                }
            }

            // убираем ненужные на данном этапе элементы
            labelRank.Visible = false;
            comboBoxRank.Visible = false;

            labelAccess.Visible = false;
            textBox2.Visible = false;
            labelReccomendation.Visible = false;
            numericUpDown1.Visible = false;

            Apply.Visible = false;
            Cancel.Visible = false;
        }

        private void comboBoxGeneralRank_SelectionChangeCommitted(object sender, EventArgs e)
        {
            comboBoxRank.Items.Clear();
            map.Clear();
            using (var conn = new NpgsqlConnection(Options.connectionString))
            {
                conn.Open();
                var commStr = "SELECT rank, type " +
                              "FROM pmib6605.military_rank " +
                              "WHERE type = @rank";

                var comm = new NpgsqlCommand(commStr, conn);
                comm.Parameters.AddWithValue("rank", rank.IndexOf(comboBoxGeneralRank.Text.Trim()) + 1);
                var reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    comboBoxRank.Items.Add(reader.GetString(0).Trim());
                    map.Add(reader.GetString(0).Trim(), reader.GetInt32(1));    // ключ: ранг(строка), значение: тип(int)

                }
            }

            // показываем элементы для следующего шага
            labelRank.Visible = true;
            comboBoxRank.Visible = true;

            Cancel.Visible = true;
        }

        private void comboBoxRank_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string key = comboBoxRank.Text.Trim();
            using (var conn = new NpgsqlConnection(Options.connectionString))
            {
                int suggestedMark = 0;

                // определение по рангу (всего 6) уровня доступа (4)
                if (map[key] == 1)
                    suggestedMark = 1;
                else if (map[key] == 2 || map[key] == 3)
                    suggestedMark = 2;
                else if (map[key] == 4 || map[key] == 5)
                    suggestedMark = 3;
                else if (map[key] == 6)
                    suggestedMark = 4;

                conn.Open();

                // получаем словесное описание метки
                var commStr = "SELECT name " +
                              "FROM pmib6605.marks " +
                              "WHERE mark = @mk";

                var comm = new NpgsqlCommand(commStr, conn);
                comm.Parameters.AddWithValue("mk", suggestedMark);
                var result = comm.ExecuteScalar();
                textBox2.Text = result.ToString().Trim();
                labelReccomendation.Text = "Рекомендуемый доступ: " + suggestedMark + ": " + result.ToString().Trim();
                numericUpDown1.Value = suggestedMark;

            }
            // показываем дальнейшие элементы
            labelAccess.Visible = true;
            labelReccomendation.Visible = true;
            textBox2.Visible = true;
            numericUpDown1.Visible = true;

            Apply.Visible = true;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            // изменение словесного описание метки в соответствии с изменяемым значением
            using (var conn = new NpgsqlConnection(Options.connectionString))
            {
                conn.Open();
                var commStr = "SELECT name " +
                              "FROM pmib6605.marks " +
                              "WHERE mark = @mk";

                var comm = new NpgsqlCommand(commStr, conn);
                comm.Parameters.AddWithValue("mk", numericUpDown1.Value);
                var reader = comm.ExecuteReader();
                reader.Read();
                textBox2.Text = reader.GetString(0).Trim();
            }
        }

        private void cancel_Click(object sender, EventArgs e)      // отмена
        {
            name.Text = "";
            dateTimePicker1.Value = System.DateTime.Now;

            labelRank.Visible = false;         // для второго выпадающего списка
            comboBoxRank.Visible = false;
            comboBoxRank.Items.Clear();

            textBox2.Visible = false;       // для словесного описания метки
            numericUpDown1.Visible = false;
            labelAccess.Visible = false;
            labelReccomendation.Visible = false;

            Cancel.Visible = false;
            Apply.Visible = false;
            comboBoxGeneralRank.SelectedIndex = -1;
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            // проверка на заполнение поля ФИО
            if (string.IsNullOrWhiteSpace(name.Text))
            {
                MessageBox.Show("ФИО", "Введите данные", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var conn = new NpgsqlConnection(Options.connectionString))
            {
                conn.Open();

                var commStr = "SELECT name FROM pmib6605.subjects " +               // проверка на дубликаты
                              "WHERE name = @name";

                var comm = new NpgsqlCommand(commStr, conn);
                comm.Parameters.AddWithValue("name", name.Text.Trim());
                var result = comm.ExecuteScalar();
                if (result != null)
                {
                    MessageBox.Show("Данный сотрудник уже находится в базе", "Ошшибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                commStr = "SELECT max(id) FROM pmib6605.subjects";              // получение порядкового id
                comm = new NpgsqlCommand(commStr, conn);
                var id = Convert.ToInt32(comm.ExecuteScalar()) + 1;

                commStr = "SELECT id FROM pmib6605.military_rank WHERE rank = @rank";       // получение type
                comm = new NpgsqlCommand(commStr, conn);
                comm.Parameters.AddWithValue("rank", comboBoxRank.Text.Trim());
                var idType = comm.ExecuteScalar().ToString();


                commStr = "INSERT INTO pmib6605.subjects (id, name, birthday, rank, type, mark) " +             // вставка
                              "VALUES (@id, @name, @birthday, @rank, @idType, @mark) ";
                comm = new NpgsqlCommand(commStr, conn);
                comm.Parameters.AddWithValue("id", Convert.ToInt32(id));
                comm.Parameters.AddWithValue("name", name.Text);
                comm.Parameters.AddWithValue("birthday", dateTimePicker1.Value);
                comm.Parameters.AddWithValue("idType", Convert.ToInt32(idType));
                comm.Parameters.AddWithValue("rank", rank.IndexOf(comboBoxGeneralRank.Text.Trim()) + 1);
                comm.Parameters.AddWithValue("mark", numericUpDown1.Value);

                comm.ExecuteNonQuery();
            }
            MessageBox.Show("Добавлено", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
            cancel_Click(sender, e);
        }

        private void name_KeyPress(object sender, KeyPressEventArgs e)
        {
            // возможны следующие клавиши: буквы, bakcspace, space, point
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space || e.KeyChar == '.');
        }

        private void back_Click(object sender, EventArgs e)
        {
            Admin open = new Admin();
            open.Show();
            this.Hide();
        }

        private void changeAccount_Click(object sender, EventArgs e)
        {
            Login open = new Login();
            open.Show();
            this.Hide();
        }
    }
}