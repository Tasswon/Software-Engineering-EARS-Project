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
    public partial class TagPersonForm : Form
    {
        private DBConnection dbConnection;
        bool search = false;
        bool otherWindowOpen = true;
        int jobId;
        int appId;
        int memId;
        int securityStatus;
        public TagPersonForm(DBConnection dbConnection, int jobid, int appid, int memid, int securityStatus)
        {
            this.securityStatus = securityStatus;
            this.jobId = jobid;
            this.appId = appid;
            this.memId = memid;
            this.dbConnection = dbConnection;
            InitializeComponent();
        }

        private void TagPersonForm_Load(object sender, EventArgs e)
        {
            positionLevel.Items.Add("Administrator");
            positionLevel.Items.Add("Secretary");
            positionLevel.Items.Add("Committee Member");
            positionLevel.Items.Add("Chairperson");

            positionLevel.SelectedIndex = 0;

            usersListView.View = View.Details;
            usersListView.GridLines = true;
            usersListView.FullRowSelect = true;

            usersListView.Columns.Add("Member ID", 150);
            usersListView.Columns.Add("First Name", 150);
            usersListView.Columns.Add("Last Name", 150);
            usersListView.Columns.Add("Position", 150);
        }

        public void RefreshUserList()
        {
            usersListView.Items.Clear();

            if (dbConnection.OpenConnection())
            {
                MySqlCommand command = new MySqlCommand();

                command.Connection = dbConnection.getConnection();
                
                if(search)
                {
                    command.CommandText = "SELECT member_id, f_name, l_name, position FROM member_info WHERE f_name LIKE @fname AND l_name LIKE @lname AND position LIKE @position;";
                    command.Parameters.Add("@fname", MySqlDbType.VarChar, 200).Value = "%" + txtFirstName.Text + "%";
                    command.Parameters.Add("@lname", MySqlDbType.VarChar, 200).Value = "%" + txtLastName.Text + "%";
                    command.Parameters.Add("@position", MySqlDbType.VarChar, 200).Value = "%" + positionLevel.Text + "%";
                }
                else
                {
                    command.CommandText = "SELECT member_id, f_name, l_name, position FROM member_info";
                }

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

        private void TagPersonForm_FormClosed(object sender, FormClosedEventArgs e)
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
                btnSelect.Enabled = true;
            }
            else
            {
                btnSelect.Enabled = false;
            }
        }

        private void TagPersonForm_Activated(object sender, EventArgs e)
        {
            Console.WriteLine("Form active, refreshing user list.");
            RefreshUserList();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (dbConnection.OpenConnection())
            {
                int taged = Int32.Parse(usersListView.SelectedItems[0].SubItems[0].Text);
                MySqlCommand command = new MySqlCommand();

                command.Connection = dbConnection.getConnection();
                command.CommandText = "INSERT INTO app_tags (app_id, job_id, member_id, tagee_id) VALUES (@appid, @jobid, @taged, @tager)";

                command.Parameters.AddWithValue("@appid", appId);
                command.Parameters.AddWithValue("@jobid", jobId);
                command.Parameters.AddWithValue("@taged", taged);
                command.Parameters.AddWithValue("@tager", memId);

                command.ExecuteNonQuery();

                dbConnection.CloseConnection();
                command.Dispose();

                MessageBox.Show(this, "User tagged successfully", "User Tagged", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            otherWindowOpen = true;
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            search = true;
            RefreshUserList();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            search = false;
            RefreshUserList();
        }
    }
}
