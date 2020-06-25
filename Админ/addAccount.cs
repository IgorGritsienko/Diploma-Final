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
using System.Security.Cryptography;

namespace Diploma.Админ
{
    public partial class addAccount : Form
    {
        public addAccount()
        {
            InitializeComponent();
        }

        Dictionary<string, int> map = new Dictionary<string, int>();

        private void addAccount_Load(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            textBox2.Visible = false;

            button1.Visible = false;
            button2.Visible = false;

            using (var conn = new NpgsqlConnection(Options.connectionString))   // заполнить выпадающий список и создать пары <ФИО - id>
            {
                conn.Open();
                var commStr = "SELECT id, name FROM pmib6605.subjects";
                var comm = new NpgsqlCommand(commStr, conn);
                var reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader.GetString(1).Trim());
                    map.Add(reader.GetString(1).Trim(), reader.GetInt32(0));
                }
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = true;

            int subjectId = map[comboBox1.Text.Trim()]; // foreign id
            using (var conn = new NpgsqlConnection(Options.connectionString))   // заполнить выпадающий список и создать пары <ФИО - id>
            {
                conn.Open();
                var commStr = "SELECT login, password FROM pmib6605.login " +
                              "WHERE name = @subjectId";
                var comm = new NpgsqlCommand(commStr, conn);
                comm.Parameters.AddWithValue("subjectId", subjectId);
                var reader = comm.ExecuteReader();
                if (!reader.Read())
                {
                    label1.Text = "У данного пользователя еще нет аккаунта. \n Создайте его:";
                    textBox1.Text = "";
                    
                }
                else
                {
                    label1.Text = "Измените данные:";
                    textBox1.Text = reader.GetString(0).Trim();
                }
                textBox2.Text = "";
                panel3.Visible = true;

            }
            label2.Text = "Логин:";
            label3.Text = "Пароль:";

            textBox1.Visible = true;
            textBox2.Visible = true;

            button1.Visible = true;
            button2.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)      // отмена
        {
            comboBox1.SelectedIndex = -1;
            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            panel3.Visible = false;

            textBox1.Visible = false;
            textBox2.Visible = false;

            button1.Visible = false;
            button2.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)      // сохранение
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Введите логин", "Введите данные", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox2.Text) || textBox2.TextLength < 5)
            {
                MessageBox.Show("Пароль должен быть не меньше 5 символов", "Введите данные", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var conn = new NpgsqlConnection(Options.connectionString))
            {

                var hash = MD5.Create().ComputeHash(Encoding.Default.GetBytes(textBox2.Text.Trim()));

                conn.Open();
                if (label1.Text == "Измените данные:")       // если пользователь существовал, то обновить данные
                {
                    int subjectId = map[comboBox1.Text.Trim()];     // foreign id

                    var commStr = "UPDATE pmib6605.login " +
                                  "SET login = @log, password = @pas " +
                                  "WHERE name = @subjectId";
                    var comm = new NpgsqlCommand(commStr, conn);
                    comm.Parameters.AddWithValue("log", textBox1.Text.Trim());
                    comm.Parameters.AddWithValue("pas", hash);
                    comm.Parameters.AddWithValue("subjectId", subjectId);
                    comm.ExecuteNonQuery();
                    MessageBox.Show("Обновлено", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else                                                 // иначе добавить его в таблицу
                {
                    int subjectId = map[comboBox1.Text.Trim()];     // foreign id

                    var commStr = "SELECT max(id) FROM pmib6605.login";
                    var comm = new NpgsqlCommand(commStr, conn);
                    int result = Convert.ToInt32(comm.ExecuteScalar()) + 1;

                    commStr = "INSERT INTO pmib6605.login " +
                              "VALUES (@id, @subj, @log, @pas)";
                    comm = new NpgsqlCommand(commStr, conn);
                    comm.Parameters.AddWithValue("id", result);
                    comm.Parameters.AddWithValue("subj", subjectId);
                    comm.Parameters.AddWithValue("log", textBox1.Text.Trim());
                    comm.Parameters.AddWithValue("pas", textBox2.Text.Trim());
                    comm.ExecuteNonQuery();
                    MessageBox.Show("Добавлено", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            button2_Click(sender, e);
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
