using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using MySql.Data.MySqlClient;
using System.IO;

namespace COSC3506_Project
{
    public partial class TaggedInForm : Form
    {
        private DBConnection dbConnection;
        private Boolean otherWindowOpen = false;
        private int securityStatus;
   
        private int memberId; 

        public TaggedInForm(DBConnection dbConnection, int securityStatus, int memberId)
        {
            this.memberId = memberId;
            this.securityStatus = securityStatus;
            this.dbConnection = dbConnection;
            InitializeComponent();
        }
        private void TaggedInForm_Activated(object sender, EventArgs e)
        {
            Console.WriteLine("Form active, refreshing user list.");
            RefreshTagsList();
        }

        private void TaggedInForm_Closed(object sender, FormClosedEventArgs e)
        {
            if (!otherWindowOpen)
                Application.Exit();
        }

        private void tagsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tagsList.SelectedItems.Count > 0)
            {
                btnGo.Enabled = true;
            }
            else
            {
                btnGo.Enabled = false;
            }
        }
        private void TaggedInForm_Load(object sender, EventArgs e)
        {
            tagsList.View = View.Details;
            tagsList.GridLines = true;
            tagsList.FullRowSelect = true;

            tagsList.Columns.Add("Job ID", 150);
            tagsList.Columns.Add("Application ID", 150);
            tagsList.Columns.Add("Tagee", 150);
        }
        public void RefreshTagsList()
        {
            tagsList.Items.Clear();

            if (dbConnection.OpenConnection())
            {
                MySqlCommand command = new MySqlCommand();

                command.Connection = dbConnection.getConnection();
                command.CommandText = "SELECT job_id, app_id, tagee_id FROM app_tags WHERE member_id = @member_id";
                command.Parameters.AddWithValue("@member_id", memberId);

                using (MySqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ListViewItem item = new ListViewItem();

                        item.Text = dr[0].ToString();
                        item.SubItems.Add(dr[1].ToString());
                        item.SubItems.Add(dr[2].ToString());

                        tagsList.Items.Add(item);
                    }
                }

                dbConnection.CloseConnection();
                command.Dispose();
            }
        }
        private void btnBack_OnClick(object sender, EventArgs e)
        {
            otherWindowOpen = true; 
            this.Close();
        }
        private void btnGo_Click(object sender, EventArgs e)
        {
            ApplicationForm applicationsForm = new ApplicationForm(dbConnection, Int32.Parse(tagsList.SelectedItems[0].SubItems[0].Text), securityStatus, memberId, Int32.Parse(tagsList.SelectedItems[0].SubItems[1].Text));
            applicationsForm.Show();
            otherWindowOpen = true;
            this.Close();
        }

        private void lblUserList_Click(object sender, EventArgs e)
        {

        }
    }
}
