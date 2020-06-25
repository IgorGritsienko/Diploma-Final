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


namespace Diploma
{
    public partial class Login : Form
    {    
        public Login()
        {
            InitializeComponent();
        }

        bool pswd = false;

        // проверка на пустоту полей и правильность заполнения
        private void Authorization(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            bool error = false;
            if (String.IsNullOrEmpty(textBoxLogin.Text))
            {
                errorProvider1.SetError(textBoxLogin, "Необходимо ввести логин!");
                error = true;
            }
            if (String.IsNullOrEmpty(textBoxPassword.Text) && !error)
            {
                errorProvider1.SetError(textBoxPassword, "Необходимо ввести пароль!");
                error = true;
            }
            if (textBoxPassword.Text.Length < 5 && !error)
            {
                errorProvider1.SetError(textBoxPassword, "Слишком короткий пароль!");
                error = true;
            }

            if (!error)
            {
                var hash = MD5.Create().ComputeHash(Encoding.Default.GetBytes(textBoxPassword.Text.Trim()));
                using (var connection = new NpgsqlConnection(Options.connectionString))
                {
                    connection.Open();

                    var commandString = "SELECT name, login, password FROM pmib6605.login";
                    var command = new NpgsqlCommand(commandString, connection);

                    int idName = 0; // запомнить id субъекта
                    bool found = false;

                    // проверка на соответствие логина и пароля
                    var reader = command.ExecuteReader();

                    while (reader.Read() && !found)
                    {
                        if (reader.IsDBNull(0) && reader.GetString(1).Trim() == textBoxLogin.Text)
                        {
                            var trueHash = reader.GetString(2).Trim();
                            var trueHashBytes = Utility.ConvertHexToBytes(trueHash);
                            bool check = true;
                            for (int i = 0; i < trueHashBytes.Length; i++)
                            {
                                if (trueHashBytes[i] != hash[i])
                                    check = false;
                            }
                            if (check)
                            {
                                MessageBox.Show("Вы вошли в аккаунт администратора.", "Вход выполнен ", MessageBoxButtons.OK);
                                Admin open = new Admin();
                                open.Show();
                                this.Hide();
                                return;
                            }
                        }

                        if (reader.GetString(1).Trim() == textBoxLogin.Text)
                        {
                            var trueHash = reader.GetString(2).Trim();
                            var trueHashBytes = Utility.ConvertHexToBytes(trueHash);

                            bool check = true;
                            for (int i = 0; i < trueHashBytes.Length; i++)
                            {
                                if (trueHashBytes[i] != hash[i])
                                {
                                    check = false;
                                    break;
                                }
                            }
                            if (check)
                            {
                                idName = reader.GetInt32(0);
                                found = true;
                            }
                        }
                    }
                    reader.Close();

                    if (found)
                    {
                        commandString = "SELECT id, name, mark FROM pmib6605.subjects WHERE id = @idName";
                        command = new NpgsqlCommand(commandString, connection);
                        command.Parameters.AddWithValue("idName", idName);
                        int mark = 0;
                        string name = "";

                        // поиск ФИО и метки по id
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            name = reader.GetString(1).Trim();
                            mark = reader.GetInt32(2);
                        }
                        reader.Close();

                        Options.fio = name;
                        MessageBox.Show("Добро пожаловать, " + name + "!", "Вход выполнен ", MessageBoxButtons.OK);

                        Main open = new Main(mark);
                        open.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Кнопка выхода из приложения
        private void buttonExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены?", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
                Application.Exit();
            }
        }

        // подсказки
        private void textBoxLogin_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = "Введите Ваш логин, выданный администратором.";
            statusStrip1.BackColor = Color.FromArgb(78, 184, 206);
        }

        private void textBoxLogin_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "";
            statusStrip1.BackColor = Color.FromArgb(34, 36, 49);
        }

        private void textBoxPassword_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = "Пароль должен состоять не менее, чем из 6 символов.";
            statusStrip1.BackColor = Color.PaleGreen;
            statusStrip1.BackColor = Color.FromArgb(78, 184, 206);
        }

        private void textBoxPassword_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "";
            statusStrip1.BackColor = Color.FromArgb(34, 36, 49);
        }

        private void Вход_Load(object sender, EventArgs e)
        {
            statusStrip1.BackColor = Color.FromArgb(34, 36, 49);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(pswd)
            {
                pswd = false;
                textBoxPassword.PasswordChar = '*';
            }
            else
            {
                pswd = true;
                textBoxPassword.PasswordChar = '\0';
            }
        }

        private void textBoxLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetterOrDigit(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBoxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetterOrDigit(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}