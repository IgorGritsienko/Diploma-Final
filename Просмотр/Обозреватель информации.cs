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
    public partial class selectRO : Form
    {
        public selectRO(int mark)
        {
            InitializeComponent();
            this.mark = mark;
        }
        readonly int mark;

        // словарь для сохранения минимальных меток для просмотра (нажатия на кнопку) <ТЕКСТ КНОПКИ - МЕТКА>
        Dictionary<string, int> mapTextMark = new Dictionary<string, int>();

        // словарь для сохранения минимальных меток для просмотра (нажатия на кнопку) <ТЕКСТ КНОПКИ - ОБЪЕКТ>
        Dictionary<string, string> mapTextObject = new Dictionary<string, string>();

        private void selectRO_Load(object sender, EventArgs e)
        {
            var connection = new NpgsqlConnection(Options.connectionString);
            connection.Open();
            bool tip = false;
            var commandString = "SELECT table_name, marks FROM pmib6605.table_marks";
            var command = new NpgsqlCommand(commandString, connection);
            var reader = command.ExecuteReader();
           
            while (reader.Read())
            {
                if (reader.GetString(0).Trim() == "buildings" && reader.GetInt32(1) > mark)
                {
                    button1.ForeColor = Color.Red;
                    tip = true;                   
                    mapTextMark.Add(button1.Text.Trim(), reader.GetInt32(1));
                    mapTextObject.Add(button1.Text.Trim(), reader.GetString(0).Trim());
                }

                if (reader.GetString(0).Trim() == "combat_vehicles" && reader.GetInt32(1) > mark)
                {
                    button2.ForeColor = Color.Red;                 
                    tip = true;
                    mapTextMark.Add(button2.Text.Trim(), reader.GetInt32(1));
                    mapTextObject.Add(button2.Text.Trim(), reader.GetString(0).Trim());
                }

                if (reader.GetString(0).Trim() == "coys" && reader.GetInt32(1) > mark)
                {
                    button3.ForeColor = Color.Red;
                    tip = true;
                    mapTextMark.Add(button3.Text.Trim(), reader.GetInt32(1));
                    mapTextObject.Add(button3.Text.Trim(), reader.GetString(0).Trim());
                }

                if (reader.GetString(0).Trim() == "rocket_launcher" && reader.GetInt32(1) > mark)
                {
                    button5.ForeColor = Color.Red;
                    tip = true;
                    mapTextMark.Add(button5.Text.Trim(), reader.GetInt32(1));
                    mapTextObject.Add(button5.Text.Trim(), reader.GetString(0).Trim());
                }

                if (reader.GetString(0).Trim() == "weapons" && reader.GetInt32(1) > mark)
                {
                    button6.ForeColor = Color.Red;
                    tip = true;
                    mapTextMark.Add(button6.Text.Trim(), reader.GetInt32(1));
                    mapTextObject.Add(button6.Text.Trim(), reader.GetString(0).Trim());
                }

                if (reader.GetString(0).Trim() == "military_inventions" && reader.GetInt32(1) > mark)
                {
                    button7.ForeColor = Color.Red;
                    tip = true;
                    mapTextMark.Add(button7.Text.Trim(), reader.GetInt32(1));
                    mapTextObject.Add(button7.Text.Trim(), reader.GetString(0).Trim());
                }
                if (reader.GetString(0).Trim() == "acquired_rank" && reader.GetInt32(1) > mark)
                {
                    button8.ForeColor = Color.Red;
                    tip = true;
                    mapTextMark.Add(button8.Text.Trim(), reader.GetInt32(1));
                    mapTextObject.Add(button8.Text.Trim(), reader.GetString(0).Trim());
                }

                if (reader.GetString(0).Trim() == "supplies" && reader.GetInt32(1) > mark)
                {
                    button9.ForeColor = Color.Red;
                    tip = true;
                    mapTextMark.Add(button9.Text.Trim(), reader.GetInt32(1));
                    mapTextObject.Add(button9.Text.Trim(), reader.GetString(0).Trim());
                }
                if (tip)
                {
                    toolStripStatusLabel1.Text = "Ваш уровень конфиденциальности не удовлетворяет требованиям для просмотра всех данных." + "\nДля повышения Вашего уровня конфиденциальности отправьте запрос администратору.";
                    statusStrip1.BackColor = Color.Red;
                }
            }
            reader.Close();
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.ForeColor == Color.Red)
            {
                return;
            }
            buildings open = new buildings(mark);
            open.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.ForeColor == Color.Red)
            {
                return;
            }
            vehicles open = new vehicles(mark);
            open.Show();
            this.Close();
        }


        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.ForeColor == Color.Red)
            {
                return;
            }
            showRanks open = new showRanks(mark);
            open.Show();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (button6.ForeColor == Color.Red)
            {
                return;
            }
            weapons open = new weapons(mark);
            open.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.ForeColor == Color.Red)
            {
                return;
            }
            Coys open = new Coys(mark);
            open.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (button5.ForeColor == Color.Red)
            {
                return;
            }
            rocket_launcher open = new rocket_launcher(mark);
            open.Show();
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (button7.ForeColor == Color.Red)
            {
                return;
            }
            inventions open = new inventions(mark);
            open.Show();
            this.Close();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (button8.ForeColor == Color.Red)
            {
                return;
            }
            Acquired_rank open = new Acquired_rank(mark);
            open.Show();
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (button9.ForeColor == Color.Red)
            {
                return;
            }
            supply open = new supply(mark);
            open.Show();
            this.Close();
        }

        private void отправитьЗапросАдминистраторуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapTextMark.Count == 0)
            {
                MessageBox.Show("Вы уже имеете права на чтение всей доступной информации.", "Назад", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            connectAdmin fr = new connectAdmin(mark, mapTextMark, mapTextObject, 0);
            fr.Show();
            this.Close();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Main open = new Main(mark);
            open.Show();
            this.Close();
        }

        private void ChangeAccount_Click(object sender, EventArgs e)
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
