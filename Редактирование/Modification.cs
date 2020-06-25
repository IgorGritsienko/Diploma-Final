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
    public partial class Modification : Form
    {
        public Modification(int mark)
        {
            InitializeComponent();
            this.mark = mark;
        }

        readonly int mark;

        // словарь для сохранения минимальных меток для просмотра (нажатия на кнопку) <ТЕКСТ КНОПКИ - МЕТКА>
        Dictionary<string, int> mapTextMark = new Dictionary<string, int>();

        // словарь для сохранения минимальных меток для просмотра (нажатия на кнопку) <ТЕКСТ КНОПКИ - ОБЪЕКТ>
        Dictionary<string, string> mapTextObject = new Dictionary<string, string>();

        private void Modification_Load(object sender, EventArgs e)
        {
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;

            using (var connection = new NpgsqlConnection(Options.connectionString))
            {
                connection.Open();
                var commandString = "SELECT marks FROM pmib6605.table_marks WHERE table_name = @name";
                var command = new NpgsqlCommand(commandString, connection);
                command.Parameters.AddWithValue("name", "rocket_launcher");
                var res = command.ExecuteScalar();

                if (Convert.ToInt32(res) > mark)
                {
                    button3.ForeColor = Color.Red;
                    toolStripStatusLabel1.Text = "Ваш уровень конфиденциальности не удовлетворяет требованиям для доступа ко всем данных." + "\nДля повышения Вашего уровня конфиденциальности отправьте запрос администратору.";
                    statusStrip1.BackColor = Color.Red;
                    mapTextMark.Add(button3.Text.Trim(), Convert.ToInt32(res));
                    mapTextObject.Add(button3.Text.Trim(), "rocket_launcher");
                }
                connection.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            modWeapon open = new modWeapon(mark);
            open.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            changeAmountWeaponsVehicles open = new changeAmountWeaponsVehicles(mark);
            open.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.ForeColor == Color.Red)
                return;
            transfer open = new transfer(mark);
            open.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Add_main open = new Add_main(mark);
            open.Show();
            this.Close();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Main open = new Main(mark);
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

        private void button1_MouseHover(object sender, EventArgs e)
        {
            label1.Visible = true;
            panel1.Visible = true;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            label1.Visible = false;
            panel1.Visible = false;
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            label2.Visible = true;
            panel2.Visible = true;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            label2.Visible = false;
            panel2.Visible = false;
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            label3.Visible = true;
            panel3.Visible = true;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            label3.Visible = false;
            panel3.Visible = false;
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            label4.Visible = true;
            panel4.Visible = true;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            label4.Visible = false;
            panel4.Visible = false;
        }

        private void connectAdmin_Click(object sender, EventArgs e)
        {
            connectAdmin fr = new connectAdmin(mark, mapTextMark, mapTextObject, 1);
            fr.Show();
            this.Close();
        }
    }
}
