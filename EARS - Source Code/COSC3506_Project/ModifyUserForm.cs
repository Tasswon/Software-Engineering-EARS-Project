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

namespace COSC3506_Project
{
    public partial class ModifyUserForm : Form
    {
        private int memberID;
        private DBConnection dbConnection;

        public ModifyUserForm(DBConnection dbConnection, int memberID)
        {
            this.dbConnection = dbConnection;
            this.memberID = memberID;

            InitializeComponent();
        }

        private void ModifyUserForm_Load(object sender, EventArgs e)
        {
            securityComboBox.Items.Add("Administrator");
            securityComboBox.Items.Add("Secretary");
            securityComboBox.Items.Add("Committee Member");
            securityComboBox.Items.Add("Chairperson");

            bool available = false;

            if (dbConnection.OpenConnection())
            {
                MySqlCommand command = new MySqlCommand();

                command.Connection = dbConnection.getConnection();
                command.CommandText = "SELECT f_name, l_name, available FROM member_info WHERE member_id=@id";

                command.Parameters.AddWithValue("@id", memberID);

                using (MySqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        txtFirstName.Text = dr[0].ToString();
                        txtLastName.Text = dr[1].ToString();

                        if (!DBNull.Value.Equals(dr[2]))
                            available = (Boolean)dr[2];
                    }
                }

                if (available)
                    chkAvailable.Checked = true;
                else
                    chkAvailable.Checked = false;

                command.CommandText = "SELECT username, security_status FROM user_login WHERE member_id=@id";

                using (MySqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        txtUsername.Text = dr[0].ToString();
                        securityComboBox.SelectedIndex = Int32.Parse(dr[1].ToString()) - 1;
                    }
                }

                dbConnection.CloseConnection();
                command.Dispose();
            }
        }

        private void securityComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (securityComboBox.SelectedIndex != 2)
                chkAvailable.Enabled = false;
            else
                chkAvailable.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int available;
            int rowCount = 0;

            if (txtFirstName.TextLength < 1 || txtLastName.TextLength < 1 || txtUsername.TextLength < 1)
            {
                MessageBox.Show(this, "You must fill in all fields.", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dbConnection.OpenConnection())
            {
                MySqlCommand command = new MySqlCommand();

                command.Connection = dbConnection.getConnection();
                command.CommandText = "SELECT username FROM user_login WHERE username = @username AND member_id != @id";

                command.Parameters.AddWithValue("@id", memberID);
                command.Parameters.AddWithValue("@username", txtUsername.Text);

                using (MySqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                        rowCount++;
                }

                if (rowCount > 0)
                {
                    dbConnection.CloseConnection();
                    command.Dispose();

                    MessageBox.Show(this, "The username you entered already exists.", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                if (chkAvailable.Checked == true)
                    available = 1;
                else
                    available = 0;

                command.CommandText = "UPDATE member_info SET f_name = @firstName, l_name = @lastName, available = @available, position = @position WHERE member_id = @id";

                command.Parameters.AddWithValue("@firstName", txtFirstName.Text);
                command.Parameters.AddWithValue("@lastName", txtLastName.Text);
                command.Parameters.AddWithValue("@available", available);
                command.Parameters.AddWithValue("@position", securityComboBox.Text);

                command.ExecuteNonQuery();

                command.CommandText = "UPDATE user_login SET username = @username, security_status = @securityStatus WHERE member_id = @id";

                command.Parameters.AddWithValue("@securityStatus", securityComboBox.SelectedIndex + 1);

                command.ExecuteNonQuery();

                dbConnection.CloseConnection();
                command.Dispose();

                MessageBox.Show(this, "User successfuly updated.", "Modify User", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
        }
    }
}
