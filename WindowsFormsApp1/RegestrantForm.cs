using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class RegestrantForm : Form
    {
        public RegestrantForm()
        {
            InitializeComponent();
            LoginText.Text = "Введите имя";
            LoginText.ForeColor = Color.Gray;
            PassField.Text = "Введите пароль";
            PassField.ForeColor = Color.Gray;
            PassField.UseSystemPasswordChar = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        Point lastPoint;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void userNameField_Enter(object sender, EventArgs e)
        {
            if (LoginText.Text == "Введите имя")
            {
                LoginText.Text = "";
                LoginText.ForeColor = Color.Black;
            }
        }

        private void userNameField_Leave(object sender, EventArgs e)
        {
            if (LoginText.Text == "")
            {
                LoginText.Text = "Введите имя";
                LoginText.ForeColor = Color.Gray;
            }
        }

        private void userSurNameField_Enter(object sender, EventArgs e)
        {
            if (PassField.Text == "Введите пароль")
            {
                PassField.Text = "";
                PassField.ForeColor = Color.Black;
                PassField.UseSystemPasswordChar = true;
            }
        }

        private void userSurNameField_Leave(object sender, EventArgs e)
        {
            if (PassField.Text == "")
            {
                PassField.Text = "Введите пароль";
                PassField.ForeColor = Color.Gray;
                PassField.UseSystemPasswordChar = false;
            }
        }

        private void buttonregestrat_Click(object sender, EventArgs e)
        {
            if (LoginText.Text == "Введите имя")
            {
                MessageBox.Show("Введите имя");
                return;
            }    


            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`id`, `login`, `password`, `Name`, `SurName`) VALUES (@login, @password, @name, @surname)", db.getConnection());
            
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = LoginText.Text;
            command.Parameters.Add("@password", MySqlDbType.VarChar).Value = PassField.Text;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = textName.Text;
            command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = textSurName.Text;

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
                MessageBox.Show("Аккаунт создан");
            else
                MessageBox.Show("Аккаунт не был создан");

            db.closeConnection();
        }


        public Boolean checkUser()
        {
            DB db = new DB();
            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login`=@uL", db.getConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = LoginText.Text;
            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Такой пользователь уже есть в системе?, введите другой логин");
                return true;
            }
            else
                return false;
        }

        private void RegesterLable_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }
    }
}
