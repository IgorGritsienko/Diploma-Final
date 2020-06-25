using Npgsql;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Diploma
{
    public partial class Add_main : Form
    {
        public Add_main(int mark)
        {
            InitializeComponent();
            this.mark = mark;
        }
        int mark;

        int button;
        bool button1Block = false;
        bool button2Block = false;
        bool button3Block = false;

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.ForeColor == Color.Red)
                return;
            button = 1;

            button2.ForeColor = Color.Red;
            button1.ForeColor = Color.Red;
            button3.ForeColor = Color.Red;

            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            panel5.Visible = true;

            name.Visible = true;
            type.Visible = true;
            ammo.Visible = true;
            Class.Visible = true;

            name.Enabled = true;
            type.Enabled = true;
            ammo.Enabled = true;
            Class.Enabled = true;

            label1.Text = "Введите наименование оружия";
            label2.Text = "Введите тип оружия";
            label3.Text = "Введите боеприпасы";
            label4.Text = "Введите дальность поражения";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.ForeColor == Color.Red)
                return;
            button = 2;

            button2.ForeColor = Color.Red;
            button1.ForeColor = Color.Red;
            button3.ForeColor = Color.Red;

            name.Visible = true;
            type.Visible = true;
            ammo.Visible = true;
            seats.Visible = true;
            places.Visible = true;

            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            panel4.Visible = true;

            name.Enabled = true;
            type.Enabled = true;
            ammo.Enabled = true;
            seats.Enabled = true;
            places.Enabled = true;

            label1.Text = "Введите наименование техники";
            label2.Text = "Введите тип";
            label3.Text = "Введите вооружение (опционально)";
            label4.Text = "Выберите место обслуживания";
            label5.Text = "Введите вместительность";

            using (var conn = new NpgsqlConnection(Options.connectionString))
            {
                conn.Open();
                var commStr = "SELECT distinct target, buildings.mark FROM pmib6605.buildings " +
                              "JOIN pmib6605.vehicles_attributes on vehicles_attributes.places = buildings.id ";
                var comm = new NpgsqlCommand(commStr, conn);
                var reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetInt32(1) <= mark)
                    {
                        places.Items.Add(reader.GetString(0));
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.ForeColor == Color.Red)
                return;
            button = 3;

            button2.ForeColor = Color.Red;
            button1.ForeColor = Color.Red;
            button3.ForeColor = Color.Red;

            name.Visible = true;
            headquarters.Visible = true;
            target.Visible = true;
            dateStart.Visible = true;

            panel1.Visible = true;

            name.Enabled = true;
            headquarters.Enabled = true;
            target.Enabled = true;
            dateStart.Enabled = true;

            label1.Text = "Введите название проекта";
            label2.Text = "Выберите штаб, в котором он выполнен";
            label3.Text = "Дополнительное описание";
            label4.Text = "Выберите дату окончания проекта";

            using (var conn = new NpgsqlConnection(Options.connectionString))
            {
                conn.Open();
                var commStr = "SELECT name, mark FROM pmib6605.military_headquarters";
                var comm = new NpgsqlCommand(commStr, conn);
                var reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetInt32(1) <= mark)
                    {
                        headquarters.Items.Add(reader.GetString(0));
                    }
                }
            }
        }


        private void cancel_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
            label5.Text = "";

            name.Clear();
            type.Clear();
            ammo.Clear();
            Class.Clear();
            seats.Clear();
            target.Clear();

            if (!button1Block)
                button1.ForeColor = Color.White;
            if (!button2Block)
                button2.ForeColor = Color.White;
            if (!button3Block)
                button3.ForeColor = Color.White;

            name.Visible = false;
            type.Visible = false;
            ammo.Visible = false;
            dateStart.Visible = false;
            Class.Visible = false;

            seats.Visible = false;
            places.Visible = false;

            headquarters.Visible = false;
            target.Visible = false;

            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;

            name.Enabled = false;
            type.Enabled = false;
            ammo.Enabled = false;
            dateStart.Enabled = false;
            Class.Enabled = false;

            seats.Enabled = false;
            places.Enabled = false;

            headquarters.Enabled = false;
            target.Enabled = false;

            places.Items.Clear();
            headquarters.Items.Clear();
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

        private void Add_main_Load(object sender, EventArgs e)
        {
            using (var conn = new NpgsqlConnection(Options.connectionString))
            {
                conn.Open();
                var commStr = "SELECT table_name, marks FROM pmib6605.table_marks";
                var comm = new NpgsqlCommand(commStr, conn);
                var reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetString(0) == "rockets" && reader.GetInt32(1) > mark)
                    {
                        button1.ForeColor = Color.Red;
                        button1Block = true;
                    }
                    if (reader.GetString(0) == "military_inventions" && reader.GetInt32(1) > mark)
                    {
                        button2.ForeColor = Color.Red;
                        button2Block = true;
                    }
                    if (reader.GetString(0) == "combat_vehicles" && reader.GetInt32(1) > mark)
                    {
                        button3.ForeColor = Color.Red;
                        button3Block = true;
                    }
                }
            }
            cancel_Click(sender, e);
            //name.Visible = false;
            //type.Visible = false;
            //ammo.Visible = false;
            //dateStart.Visible = false;
            //Class.Visible = false;

            //seats.Visible = false;
            //places.Visible = false;

            //headquarters.Visible = false;
            //target.Visible = false;

            //name.Enabled = false;
            //type.Enabled = false;
            //ammo.Enabled = false;
            //dateStart.Enabled = false;
            //Class.Enabled = false;

            //seats.Enabled = false;
            //places.Enabled = false;

            //headquarters.Enabled = false;
            //target.Enabled = false;

            //panel1.Visible = false;
            //panel2.Visible = false;
            //panel3.Visible = false;
            //panel4.Visible = false;
            //panel5.Visible = false;
        }

        private void save_Click(object sender, EventArgs e)
        {
            using (var conn = new NpgsqlConnection(Options.connectionString))
            {
                conn.Open();
                if (button == 1)
                {
                    if (string.IsNullOrWhiteSpace(name.Text))
                    {
                        MessageBox.Show("Введите название", "Введите данные", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (string.IsNullOrWhiteSpace(type.Text))
                    {
                        MessageBox.Show("Введите тип", "Введите данные", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (string.IsNullOrWhiteSpace(ammo.Text))
                    {
                        MessageBox.Show("Введите класс", "Введите данные", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (string.IsNullOrWhiteSpace(Class.Text))
                    {
                        MessageBox.Show("Введите дальность поражения", "Введите данные", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var commStr = "SELECT name FROM pmib6605.rockets";
                    var comm = new NpgsqlCommand(commStr, conn);
                    var reader = comm.ExecuteReader();
                    while (reader.Read())
                        if (reader.GetString(0).Trim() == name.Text.Trim())
                        {
                            MessageBox.Show("Данная модель ракет уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                    commStr = "SELECT max(id) FROM pmib6605.rockets";

                    comm = new NpgsqlCommand(commStr, conn);
                    reader = comm.ExecuteReader();
                    reader.Read();
                    int id = reader.GetInt32(0) + 1;
                    reader.Close();

                    commStr = "INSERT INTO pmib6605.rockets (id, name, type, class, mark) " +
                              "VALUES (@id, @name, @type, @class, @mark)";
                    comm = new NpgsqlCommand(commStr, conn);
                    comm.Parameters.AddWithValue("id", id);
                    comm.Parameters.AddWithValue("name", name.Text.Trim());
                    comm.Parameters.AddWithValue("type", type.Text.Trim());
                    comm.Parameters.AddWithValue("class", ammo.Text.Trim());
                    comm.Parameters.AddWithValue("mark", mark);
                    comm.ExecuteNonQuery();

                    MessageBox.Show("Добавлено", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (button == 2)
                {
                    if (string.IsNullOrWhiteSpace(name.Text))
                    {
                        MessageBox.Show("Введите название", "Введите данные", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (string.IsNullOrWhiteSpace(type.Text))
                    {
                        MessageBox.Show("Введите тип", "Введите данные", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (string.IsNullOrWhiteSpace(seats.Text) || !seats.Text.All(char.IsDigit))
                    {
                        MessageBox.Show("Введите количество мест", "Введите данные", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(places.Text))
                    {
                        MessageBox.Show("Выберите место", "Введите данные", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var commStr = "SELECT name FROM pmib6605.combat_vehicles";
                    var comm = new NpgsqlCommand(commStr, conn);
                    var reader = comm.ExecuteReader();
                    while (reader.Read())
                        if (reader.GetString(0).Trim() == name.Text.Trim())
                        {
                            MessageBox.Show("Данная техника ракет уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                    commStr = "SELECT max(id) FROM pmib6605.combat_vehicles";
                    comm = new NpgsqlCommand(commStr, conn);
                    reader = comm.ExecuteReader();
                    reader.Read();
                    int id = reader.GetInt32(0) + 1;
                    reader.Close();

                    commStr = "INSERT INTO pmib6605.combat_vehicles (id, name, type, mark) " +
                              "VALUES (@id, @name, @type, @mark)";
                    comm = new NpgsqlCommand(commStr, conn);
                    comm.Parameters.AddWithValue("id", id);
                    comm.Parameters.AddWithValue("name", name.Text.Trim());
                    comm.Parameters.AddWithValue("type", type.Text.Trim());
                    comm.Parameters.AddWithValue("mark", mark);

                    comm.ExecuteNonQuery();

                    commStr = "INSERT INTO pmib6605.vehicles_attributes " +
                               "VALUES (@id, @name, @seats, @places, @ammunition)";

                    comm = new NpgsqlCommand(commStr, conn);
                    comm.Parameters.AddWithValue("id", id);
                    comm.Parameters.AddWithValue("name", id);
                    comm.Parameters.AddWithValue("seats", Convert.ToInt32(seats.Text.Trim()));
                    comm.Parameters.AddWithValue("places", places.Text.Trim());
                    comm.Parameters.AddWithValue("ammunition", ammo.Text.Trim());

                    comm.ExecuteNonQuery();

                    MessageBox.Show("Добавлено", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (button == 3)
                {
                    if (string.IsNullOrWhiteSpace(name.Text))
                    {
                        MessageBox.Show("Введите название", "Введите данные", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (string.IsNullOrWhiteSpace(headquarters.Text))
                    {
                        MessageBox.Show("Выберите штаб", "Введите данные", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var commStr = "SELECT name FROM pmib6605.military_inventions";
                    var comm = new NpgsqlCommand(commStr, conn);
                    var reader = comm.ExecuteReader();
                    while (reader.Read())
                        if (reader.GetString(0).Trim() == name.Text.Trim())
                        {
                            MessageBox.Show("Данное изобретения уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                    commStr = "SELECT max(id) FROM pmib6605.military_inventions";
                    comm = new NpgsqlCommand(commStr, conn);
                    reader = comm.ExecuteReader();
                    reader.Read();
                    int id = reader.GetInt32(0) + 1;
                    reader.Close();

                    commStr = "SELECT id FROM pmib6605.military_headquarters WHERE name = @name";
                    comm = new NpgsqlCommand(commStr, conn);
                    comm.Parameters.AddWithValue("name", headquarters.Text.Trim());
                    reader = comm.ExecuteReader();
                    reader.Read();
                    int headId = reader.GetInt32(0);
                    reader.Close();

                    commStr = "INSERT INTO pmib6605.military_inventions " +
                              "VALUES (@id, @name, @date_start, @target, @headquarter @mark)";
                    comm = new NpgsqlCommand(commStr, conn);
                    comm.Parameters.AddWithValue("id", id);
                    comm.Parameters.AddWithValue("name", name.Text.Trim());
                    comm.Parameters.AddWithValue("date_start", dateStart.Value.Date);
                    comm.Parameters.AddWithValue("target", target.Text.Trim());
                    comm.Parameters.AddWithValue("headquarter", headId);
                    comm.Parameters.AddWithValue("mark", mark);
                    comm.ExecuteNonQuery();

                    MessageBox.Show("Добавлено", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            cancel_Click(sender, e);

            //label1.Text = "";
            //label2.Text = "";
            //label3.Text = "";
            //label4.Text = "";
            //label5.Text = "";

            //name.Clear();
            //type.Clear();
            //ammo.Clear();
            //Class.Clear();
            //seats.Clear();
            //target.Clear();

            //button2.ForeColor = Color.White;
            //button1.ForeColor = Color.White;
            //button3.ForeColor = Color.White;

            //name.Visible = false;
            //type.Visible = false;
            //ammo.Visible = false;
            //dateStart.Visible = false;
            //Class.Visible = false;

            //seats.Visible = false;
            //places.Visible = false;

            //headquarters.Visible = false;
            //target.Visible = false;

            //name.Enabled = false;
            //type.Enabled = false;
            //ammo.Enabled = false;
            //dateStart.Enabled = false;
            //Class.Enabled = false;

            //seats.Enabled = false;
            //places.Enabled = false;

            //headquarters.Enabled = false;
            //target.Enabled = false;

            //places.Items.Clear();
            //headquarters.Items.Clear();

            //panel1.Visible = false;
            //panel2.Visible = false;
            //panel3.Visible = false;
            //panel4.Visible = false;
            //panel5.Visible = false;
        }
    }
}