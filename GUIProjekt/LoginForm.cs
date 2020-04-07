using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace GUIProjekt
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CONNECTION connection = new CONNECTION();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand();
            String query = "SELECT * FROM `users` WHERE `username`=@user AND `password` = @password";

            command.CommandText = query;
            command.Connection = connection.GetConnection();

            command.Parameters.Add("@user", MySqlDbType.VarChar).Value = textBoxUsr.Text;
            command.Parameters.Add("@password", MySqlDbType.VarChar).Value = textBoxPass.Text;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if(table.Rows.Count > 0)
            {
         
                this.Hide();
                MenuForm menuform = new MenuForm();
                menuform.Show();
            }
            else
            {
                if(textBoxUsr.Text.Trim().Equals(""))
                {
                    MessageBox.Show("Please enter username", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else if (textBoxPass.Text.Trim().Equals(""))
                {
                    MessageBox.Show("Please enter password", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("wrong password or login", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    }
}
