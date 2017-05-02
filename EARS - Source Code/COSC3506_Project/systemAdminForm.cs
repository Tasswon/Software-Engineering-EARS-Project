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
    public partial class SystemAdminForm : Form
    {
        private DBConnection dbConnection;
        private Boolean otherWindowOpen = false;

        public SystemAdminForm(DBConnection dbConnection)
        {
            this.dbConnection = dbConnection;
            InitializeComponent();
        }

        private void systemAdminForm_Load(object sender, EventArgs e)
        {
            usersListView.View = View.Details;
            usersListView.GridLines = true;
            usersListView.FullRowSelect = true;

            usersListView.Columns.Add("Member ID", 150);
            usersListView.Columns.Add("First Name", 150);
            usersListView.Columns.Add("Last Name", 150);
            usersListView.Columns.Add("Position", 150);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddUserForm addUserForm = new AddUserForm(dbConnection);
            addUserForm.ShowDialog();
        }

        public void RefreshUserList()
        {
            usersListView.Items.Clear();

            if (dbConnection.OpenConnection())
            {
                MySqlCommand command = new MySqlCommand();

                command.Connection = dbConnection.getConnection();
                command.CommandText = "SELECT member_id, f_name, l_name, position FROM member_info";

                using (MySqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ListViewItem item = new ListViewItem();

                        item.Text = dr[0].ToString();
                        item.SubItems.Add(dr[1].ToString());
                        item.SubItems.Add(dr[2].ToString());
                        item.SubItems.Add(dr[3].ToString());

                        usersListView.Items.Add(item);
                    }
                }

                dbConnection.CloseConnection();
                command.Dispose();
            }
        }

        private void SystemAdminForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!otherWindowOpen)
            {
                Application.Exit();
            }
        }

        private void usersListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (usersListView.SelectedItems.Count > 0)
            {
                btnModifyUser.Enabled = true;
                btnDeleteUser.Enabled = true;
                btnChangePassword.Enabled = true;
            } else
            {
                btnModifyUser.Enabled = false;
                btnDeleteUser.Enabled = false;
                btnChangePassword.Enabled = false;
            }
        }

        private void SystemAdminForm_Activated(object sender, EventArgs e)
        {
            Console.WriteLine("Form active, refreshing user list.");
            RefreshUserList();
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(usersListView.SelectedItems[0].Text);

            var result = MessageBox.Show(this, "Are you sure you would like to delete this user? This can't be undone.", "Delete User", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

            switch (result)
            {
                case DialogResult.Yes:
                    if (dbConnection.OpenConnection())
                    {
                        MySqlCommand command = new MySqlCommand();

                        command.Connection = dbConnection.getConnection();
                        command.CommandText = "DELETE FROM member_info WHERE member_id=@id";

                        command.Parameters.AddWithValue("@id", id);

                        command.ExecuteNonQuery();

                        command.Connection = dbConnection.getConnection();
                        command.CommandText = "DELETE FROM user_login WHERE member_id=@id";

                        command.ExecuteNonQuery();

                        dbConnection.CloseConnection();
                        command.Dispose();

                        MessageBox.Show(this, "User successfully deleted.", "User Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        btnDeleteUser.Enabled = false;
                        btnModifyUser.Enabled = false;
                        btnChangePassword.Enabled = false;
                    }
                    break;
                case DialogResult.No:
                    break;
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(usersListView.SelectedItems[0].Text);

            PasswordResetForm passwordResetForm = new PasswordResetForm(dbConnection, id);
            passwordResetForm.ShowDialog();

            btnDeleteUser.Enabled = false;
            btnModifyUser.Enabled = false;
            btnChangePassword.Enabled = false;
        }

        private void btnModifyUser_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(usersListView.SelectedItems[0].Text);

            ModifyUserForm modifyUserForm = new ModifyUserForm(dbConnection, id);
            modifyUserForm.ShowDialog();

            btnDeleteUser.Enabled = false;
            btnModifyUser.Enabled = false;
            btnChangePassword.Enabled = false;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            SignInForm form = new SignInForm(dbConnection);
            form.Show();
            otherWindowOpen = true;
            this.Close();
        }
    }
}
