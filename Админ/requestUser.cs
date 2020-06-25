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
    public partial class requestUser : Form
    {
        public requestUser()
        {
            InitializeComponent();
        }

        Dictionary<int, string> dict = new Dictionary<int, string>();
        int currIndex = -1;

        private void requestUser_Load(object sender, EventArgs e)
        {
            solved.ReadOnly = true;
            label1.Visible = false;
            description.Visible = false;
            description.ReadOnly = true;

           
            using (var conn = new NpgsqlConnection(Options.connectionString))
            {
                conn.Open();
                var commStr = "SELECT requests.id, subjects.name, description, button, date, current_mark, requested_mark, status, object " +
                              "FROM pmib6605.requests " +
                              "JOIN pmib6605.subjects on subjects.id = requests.name";
                var comm = new NpgsqlCommand(commStr, conn);
                var reader = comm.ExecuteReader();
                while(reader.Read())
                {
                    // в зависимости от статуса поместить в одну из двух таблиц
                    if(reader.GetString(7).Trim() == "В ожидании" || reader.GetString(7).Trim() == "Отложено")
                    {
                        unsolved.Rows.Add(reader.GetInt32(0), reader.GetString(1).Trim(), reader.GetString(3).Trim(), reader.GetString(8).Trim(), reader.GetDate(4),
                                          reader.GetInt32(5), reader.GetInt32(6), reader.GetString(7).Trim());
                    }
                    else if (reader.GetString(7).Trim() == "Отклонено" || reader.GetString(7).Trim() == "Выполнено")
                    {
                        solved.Rows.Add(reader.GetInt32(0), reader.GetString(1).Trim(), reader.GetString(3).Trim(), reader.GetString(8).Trim(), reader.GetDate(4),
                                        reader.GetInt32(5), reader.GetInt32(6), reader.GetString(7).Trim());
                    }
                    dict.Add(reader.GetInt32(0), reader.GetString(2));
                }
            }

            if (unsolved.Rows.Count == 0)
            {
                unsolved.Visible = false;
                MessageBox.Show("Ожидающих заявок нет", "Данных нет", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                unsolved.Visible = true;

            }
            if (solved.Rows.Count == 0) // если выполненных заявок еще нет
            {
                solved.Visible = false;
                MessageBox.Show("Выполненных заявок еще нет", "Данных нет", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                solved.Visible = true;
            }
        }

        private void unsolved_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            // при успешном нажатии появится описание запроса
            label1.Visible = true;
            label1.Text = "Описание запроса:";
            description.Visible = true;
            description.Text = dict[Convert.ToInt32(unsolved.Rows[e.RowIndex].Cells[0].Value)];
            currIndex = e.RowIndex;

            delay.ForeColor = Color.Black;
            decline.ForeColor = Color.Black;
            accept.ForeColor = Color.Black;
        }

        private void solved_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            label1.Visible = true;
            label1.Text = "Описание запроса:";
            description.Visible = true;
            description.Text = dict[Convert.ToInt32(solved.Rows[e.RowIndex].Cells[0].Value)];
            currIndex = e.RowIndex;


            delay.ForeColor = Color.Red;
            decline.ForeColor = Color.Red;
            accept.ForeColor = Color.Red;
        }

        private void delay_Click(object sender, EventArgs e)
        {
            if (currIndex == -1)
                return;
            if (delay.ForeColor == Color.Red)
                return;

            // меняется только одна запись
            unsolved.Rows[currIndex].Cells[7].Value = "Отложено";
            using (var conn = new NpgsqlConnection(Options.connectionString))
            {
                conn.Open();
                var commStr = "UPDATE pmib6605.requests " +
                              "SET status = @state " +
                              "WHERE id = @id";
                var comm = new NpgsqlCommand(commStr, conn);
                comm.Parameters.AddWithValue("state", unsolved.Rows[currIndex].Cells[7].Value);
                comm.Parameters.AddWithValue("id", unsolved.Rows[currIndex].Cells[0].Value);
                comm.ExecuteNonQuery();
            }
            MessageBox.Show("Отложено", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void decline_Click(object sender, EventArgs e)
        {
            if (currIndex == -1)
                return;
            if (decline.ForeColor == Color.Red)
                return;

            unsolved.Rows[currIndex].Cells[7].Value = "Отклонено";
            using (var conn = new NpgsqlConnection(Options.connectionString))
            {
                conn.Open();
                var commStr = "UPDATE pmib6605.requests " +
                              "SET status = @state " +
                              "WHERE id = @id";
                var comm = new NpgsqlCommand(commStr, conn);
                comm.Parameters.AddWithValue("state", unsolved.Rows[currIndex].Cells[7].Value);
                comm.Parameters.AddWithValue("id", unsolved.Rows[currIndex].Cells[0].Value);
                comm.ExecuteNonQuery();
            }
            solved.Rows.Add(unsolved.Rows[currIndex].Cells[0].Value,
                            unsolved.Rows[currIndex].Cells[1].Value,
                            unsolved.Rows[currIndex].Cells[2].Value,
                            unsolved.Rows[currIndex].Cells[3].Value,
                            unsolved.Rows[currIndex].Cells[4].Value,
                            unsolved.Rows[currIndex].Cells[5].Value,
                            unsolved.Rows[currIndex].Cells[6].Value,
                            unsolved.Rows[currIndex].Cells[7].Value);

            unsolved.Rows.Remove(unsolved.Rows[currIndex]);     // удаляем решенный запрос из таблицы нерешенных

            solved.Refresh();
            solved.Visible = true;
            unsolved.Refresh();

            MessageBox.Show("Отклонено", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void accept_Click(object sender, EventArgs e)
        {
            if (currIndex == -1)
                return;
            if (accept.ForeColor == Color.Red)
                return;

            unsolved.Rows[currIndex].Cells[7].Value = "Разрешено";

            // изменяем метку пользователя в соответствии с запросом
            using (var conn = new NpgsqlConnection(Options.connectionString))
            {
                conn.Open();
                var commStr = "UPDATE pmib6605.requests " +
                              "SET status = @state " +
                              "WHERE id = @id";
                var comm = new NpgsqlCommand(commStr, conn);
                comm.Parameters.AddWithValue("state", unsolved.Rows[currIndex].Cells[7].Value);
                comm.Parameters.AddWithValue("id", unsolved.Rows[currIndex].Cells[0].Value);
                comm.ExecuteNonQuery();

                commStr = "UPDATE pmib6605.subjects " +
                          "SET mark = @mark " +
                          "WHERE name = @name";
                comm = new NpgsqlCommand(commStr, conn);
                comm.Parameters.AddWithValue("mark", unsolved.Rows[currIndex].Cells[6].Value);
                comm.Parameters.AddWithValue("name", unsolved.Rows[currIndex].Cells[1].Value);
                comm.ExecuteNonQuery();
            }

            solved.Rows.Add(unsolved.Rows[currIndex].Cells[0].Value,
                            unsolved.Rows[currIndex].Cells[1].Value,
                            unsolved.Rows[currIndex].Cells[2].Value,
                            unsolved.Rows[currIndex].Cells[3].Value,
                            unsolved.Rows[currIndex].Cells[4].Value,
                            unsolved.Rows[currIndex].Cells[5].Value,
                            unsolved.Rows[currIndex].Cells[6].Value,
                            unsolved.Rows[currIndex].Cells[7].Value);

            unsolved.Rows.Remove(unsolved.Rows[currIndex]);

            solved.Refresh();
            solved.Visible = true;
            unsolved.Refresh();

            MessageBox.Show("Разрешено", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
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