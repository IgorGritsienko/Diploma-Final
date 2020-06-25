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
    public partial class connectAdmin : Form
    {
        public connectAdmin(int mark, Dictionary<string, int> mapTextMark, Dictionary<string, string> mapTextObject, int from)
        {
            InitializeComponent();
            this.mark = mark;
            this.mapTextMark = mapTextMark;
            this.mapTextObject = mapTextObject;
            this.from = from;
        }
        int from;
        int mark;
        Dictionary<string, int> mapTextMark;
        Dictionary<string, string> mapTextObject;

        private void connectAdmin_Load(object sender, EventArgs e)  // заполнение comboBox ключами словаря (название кнопок)
        {
            if (from == 1)  // если пришли из редактирования, то добавляем еще один выпадающий список
            {
                using (var conn = new NpgsqlConnection(Options.connectionString))
                {
                    conn.Open();
                    var commStr = "SELECT name FROM pmib6605.marks";
                    var comm = new NpgsqlCommand(commStr, conn);
                    var reader = comm.ExecuteReader();
                    while (reader.Read())
                        comboBox2.Items.Add(reader.GetString(0).Trim());
                }
                if (mapTextMark.Count == 0)
                {
                    MessageBox.Show("Вы уже имеете доступ на редактирование ко всем элементам.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    comboBox1.Enabled = false;
                }
            }
            else
            {
                label4.Visible = false;
                panel3.Visible = false;
                comboBox2.Visible = false;
            }

            List<string> keyList = new List<string>(mapTextMark.Keys); // список ключей (текст кнопки на предыдущей форме) - недоступные пользователю кнопки помещаются в выпадающий список

            foreach (string item in keyList)
            {
                comboBox1.Items.Add(item);
            }

            richTextBox1.Visible = false;
            button1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            panel2.Visible = false;
            panel2.Visible = false;
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox2.Enabled)
                comboBox2.Enabled = false;
            if (button1.Visible)
                button1.Visible = true;

            richTextBox1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            panel2.Visible = true;
            button1.Visible = true;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            label2.ForeColor = Color.FromArgb(255, 255, 255);
            label2.Text = "Осталось " + (500 - richTextBox1.TextLength) + " знаков";
        }

        private void button1_Click(object sender, EventArgs e)      // отправить запрос администратору
        {
            if (comboBox2.Enabled)
                if (comboBox2.SelectedIndex + 1 == mark)
                    MessageBox.Show("Вы уже обладаете данным уровнем доступа.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            using (var conn = new NpgsqlConnection(Options.connectionString))
            {

                conn.Open();
                var commStr = "SELECT max(id) FROM pmib6605.requests";
                var comm = new NpgsqlCommand(commStr, conn);
                var result = comm.ExecuteScalar();

                int initialId;  // порядковый номер запроса
                if (Convert.IsDBNull(result))
                    initialId = 1;
                else
                    initialId = Convert.ToInt32(result) + 1;

                commStr = "SELECT id FROM pmib6605.subjects WHERE name = @name";
                comm = new NpgsqlCommand(commStr, conn);
                comm.Parameters.AddWithValue("name", Options.fio);
                var idName = comm.ExecuteScalar();  // id субъекта

                // заполняем всю информацию о запросе
                commStr = "INSERT INTO pmib6605.requests " +
                          "VALUES (@id, @name, @text, @table, @object, @date, @subjMk, @needMk, @status)";

                comm = new NpgsqlCommand(commStr, conn);
                comm.Parameters.AddWithValue("id", initialId);
                comm.Parameters.AddWithValue("name", idName);
                comm.Parameters.AddWithValue("text", richTextBox1.Text.Trim());
              
                string tableMarkTemp;
                string tableNameTemp;
                int neededMarkTemp;
   
                if (comboBox1.Enabled)
                {                 
                    tableMarkTemp = mapTextObject[comboBox1.Text.Trim()];
                    neededMarkTemp = mapTextMark[comboBox1.Text.Trim()];
                    tableNameTemp = comboBox1.Text.Trim();
                }
                else
                {
                    tableMarkTemp = "";
                    neededMarkTemp = comboBox2.SelectedIndex + 1;
                    tableNameTemp = "";
                }

                comm.Parameters.AddWithValue("table", tableNameTemp);
                comm.Parameters.AddWithValue("object", tableMarkTemp);
                comm.Parameters.AddWithValue("date", DateTime.Today);
                comm.Parameters.AddWithValue("subjMk", mark);
                comm.Parameters.AddWithValue("needMk", neededMarkTemp);
                comm.Parameters.AddWithValue("status", "В ожидании");
                comm.ExecuteNonQuery();
            }

            comboBox1.SelectedIndex = -1;
            richTextBox1.Visible = false;
            button1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            panel2.Visible = false;

            MessageBox.Show("Ваш запрос отправлен администратору на рассмотрение.", "Выполнено", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            selectRO open = new selectRO(mark);
            open.Show();
            this.Close();
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox1.Enabled)
                comboBox1.Enabled = false;
            if (button1.Visible)
                button1.Visible = true;

            richTextBox1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            panel2.Visible = true;
            button1.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.Enabled = true;
            comboBox1.Text = "";
            label2.Visible = false;
            label3.Visible = false;
            panel2.Visible = false;
            richTextBox1.Text = "";
            richTextBox1.Visible = false;


            comboBox2.Enabled = true;
            comboBox2.Text = "";
        }


        ToolTip buttonToolTip = new ToolTip();

        private void comboBox1_MouseHover(object sender, EventArgs e)
        {
            buttonToolTip.ToolTipTitle = "Значение";
            buttonToolTip.UseFading = true;
            buttonToolTip.UseAnimation = true;
            buttonToolTip.IsBalloon = true;
            buttonToolTip.ShowAlways = true;
            buttonToolTip.AutoPopDelay = 5000;
            buttonToolTip.InitialDelay = 500;
            buttonToolTip.ReshowDelay = 0;
            buttonToolTip.SetToolTip(comboBox1, comboBox1.Text);
        }
    }
}
