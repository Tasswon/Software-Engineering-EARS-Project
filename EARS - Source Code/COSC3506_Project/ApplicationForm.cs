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
using System.Data.SqlClient;

namespace COSC3506_Project
{
    public partial class ApplicationForm : Form
    {
        int appId;
        int sentApp;
        bool navFromTag = false;
        private DBConnection dbConnection;
        private int jobId;
        int securityStatus;
        private int memberId;
        private Boolean otherWindowOpen = false;

        public ApplicationForm(DBConnection dbConnection, int jobId, int securityStatus, int memberId)
        {
            this.memberId = memberId;
            this.jobId = jobId;
            this.dbConnection = dbConnection;
            this.securityStatus = securityStatus;

            InitializeComponent();

            switch (securityStatus)
            {
                case 2: // Secretary
                    btnAdd.Visible = true;
                    btnRemove.Visible = true;
                    break;

                case 3: // Committee
                    btnComment.Visible = true;
                    btnTag.Visible = true;
                    btnApprove.Visible = true;
                    break;

                case 4: // Chair
                    btnComment.Visible = true;
                    btnFinalize.Visible = true;
                    break;
            }
        }
        public ApplicationForm(DBConnection dbConnection, int jobId, int securityStatus, int memberId, int appID)
        {
            this.securityStatus = securityStatus;
            this.jobId = jobId;
            this.dbConnection = dbConnection;
            this.memberId = memberId;
            sentApp = appID;
            InitializeComponent();
            btnComment.Visible = true;
            btnTag.Visible = true;
            btnApprove.Visible = true;
        }

        private void ApplicationForm_Activated(object sender, EventArgs e)
        {
            Console.WriteLine("Form active, refreshing user list.");
            RefreshApplicationList();
        }

        private void ApplicationForm_Closed(object sender, FormClosedEventArgs e)
        {
            if (!otherWindowOpen)
            {
                Application.Exit();
            }
        }

        private void Application_Load(object sender, EventArgs e)
        {
            applicationList.View = View.Details;
            applicationList.GridLines = true;
            applicationList.FullRowSelect = true;

            applicationList.Columns.Add("Job ID", 150);
            applicationList.Columns.Add("Application ID", 150);
            applicationList.Columns.Add("Name", 150);
            applicationList.Columns.Add("Phone", 150);
            applicationList.Columns.Add("Email", 150);
            applicationList.Columns.Add("Approved", 150);
        }

        public void RefreshApplicationList()
        {
            applicationList.Items.Clear();

            if (dbConnection.OpenConnection())
            {
                MySqlCommand command = new MySqlCommand();

                command.Connection = dbConnection.getConnection();
                if(securityStatus == 4)
                {
                    command.CommandText = "SELECT job_id, application_id, name, phone, email, approved FROM applications WHERE job_id = @job_id AND passOn = 'yes'";
                }
                else if (securityStatus == 3)
                {
                    command.CommandText = "SELECT job_id, application_id, name, phone, email, approved FROM applications WHERE job_id = @job_id AND passOn is NULL";
                }
                else
                {
                    command.CommandText = "SELECT job_id, application_id, name, phone, email, approved FROM applications WHERE job_id = @job_id";
                }
                command.Parameters.AddWithValue("@job_id", jobId);

                using (MySqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ListViewItem item = new ListViewItem();

                        item.Text = dr[0].ToString();
                        item.SubItems.Add(dr[1].ToString());
                        item.SubItems.Add(dr[2].ToString());
                        item.SubItems.Add(dr[3].ToString());
                        item.SubItems.Add(dr[4].ToString());
                        item.SubItems.Add(dr[5].ToString());

                        applicationList.Items.Add(item);
                    }
                }

                dbConnection.CloseConnection();
                command.Dispose();

                foreach (ListViewItem li in applicationList.Items)
                {
                    if (li.SubItems[5].Text == "Yes")
                    {
                        li.BackColor = Color.LightGreen;
                    }
                }
                foreach (ListViewItem li in applicationList.Items)
                {
                    if ((Int32.Parse(li.SubItems[1].Text)) == sentApp)
                    {
                        li.BackColor = Color.LightBlue;
                    }
                }
            }
        }

        private void btnAdd_OnClick(object sender, EventArgs e)
        {
            NewApplicationForm addApplicationForm = new NewApplicationForm(dbConnection, jobId);
            addApplicationForm.ShowDialog();
        }

        private void btnBack_OnClick(object sender, EventArgs e)
        {
            if (navFromTag)
            {
                TaggedInForm taggedInForm = new TaggedInForm(dbConnection, securityStatus, memberId);
            }
            else
            {
                MemberForm secretaryForm = new MemberForm(dbConnection, securityStatus, memberId);
                secretaryForm.Show();
            }
            otherWindowOpen = true;
            this.Close();
        }

        private void btnRemove_OnClick(object sender, EventArgs e)
        {
            try
            {
                string applicationId = applicationList.SelectedItems[0].SubItems[1].Text;
                string filePath = getFilePath(applicationId);
                DeleteFTPFile(filePath);

                if (dbConnection.OpenConnection())
                {
                    MySqlCommand command = new MySqlCommand();

                    command.Connection = dbConnection.getConnection();
                    command.CommandText = "DELETE FROM applications WHERE application_id = @application_id";
                    command.Parameters.AddWithValue("@application_id", applicationId);

                    command.ExecuteNonQuery();
                    dbConnection.CloseConnection();
                    command.Dispose();

                    RefreshApplicationList();
                }
            }
            catch (Exception ex)
            { Console.WriteLine("Didn't select an option..."); }
        }

        private string getFilePath(string applicationId)
        {
            string filePath = "";
            if (dbConnection.OpenConnection())
            {
                MySqlCommand command = new MySqlCommand();

                command.Connection = dbConnection.getConnection();
                command.CommandText = "SELECT app_path FROM applications WHERE application_id = @application_id";

                command.Parameters.AddWithValue("@application_id", applicationId);

                using (MySqlDataReader dr = command.ExecuteReader())
                {
                    try
                    {
                        while (dr.Read())
                            filePath = dr[0].ToString();
                    }
                    catch (Exception e)
                    { System.Console.WriteLine("Nothing selected..."); }
                }

                dbConnection.CloseConnection();
                command.Dispose();
            }
            return filePath;
        }

        public void DeleteFTPFile(string filePath)
        {
            WebRequest request = WebRequest.Create(filePath);
            request.Credentials = new NetworkCredential("ftpuser", "pickleparty");

            request.Method = WebRequestMethods.Ftp.DeleteFile;

            string result = string.Empty;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            long size = response.ContentLength;
            Stream datastream = response.GetResponseStream();
            StreamReader sr = new StreamReader(datastream);
            result = sr.ReadToEnd();
            sr.Close();

            datastream.Close();
            response.Close();
        }

        private void btnDownload_OnClick(object sender, EventArgs e)
        {
            try
            {
                string applicationId = applicationList.SelectedItems[0].SubItems[1].Text;
                DownloadResumeForm addJobForm = new DownloadResumeForm(dbConnection, applicationId);
                addJobForm.ShowDialog();
            }
            catch
            { MessageBox.Show("You must select an application"); }
            
        }

        private void btnComment_OnClick(object sender, EventArgs e)
        {
            try {
                appId = Int32.Parse(applicationList.SelectedItems[0].SubItems[1].Text);
                CommentsForm form = new CommentsForm(dbConnection, jobId, securityStatus, memberId, appId);
                form.Show();
                otherWindowOpen = true;
                this.Close();
            }
            catch (Exception) {
                MessageBox.Show("You must select an application");
            };

        }

        private void btnApprove_OnClick(object sender, EventArgs e)
        {

            try
            {
                appId = Int32.Parse(applicationList.SelectedItems[0].SubItems[1].Text);
                int numberOfRepeats;
                if (dbConnection.OpenConnection())
                {
                    MySqlCommand command = new MySqlCommand();
                    command.Connection = dbConnection.getConnection();
                    command.CommandText = "INSERT INTO app_passes (application_id, member_id) VALUES (@application_id, @member_id)";
                    command.Parameters.AddWithValue("@application_id", appId);
                    command.Parameters.AddWithValue("@member_id", memberId);
                    
                    try
                    {
                        command.ExecuteNonQuery();
                        command.Dispose();
                        MessageBox.Show("Application Approved!", "EARS System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MySqlCommand query = new MySqlCommand("SELECT COUNT(application_id) as NumOfApprovals from app_passes where application_id = @application_id", dbConnection.getConnection());
                        query.Parameters.AddWithValue("@application_id", appId);
                        dbConnection.OpenConnection();
                        query.Connection = dbConnection.getConnection();
              

                        Console.WriteLine("HELLO HERE: " + appId + " " + query.CommandText + "CONNECTION STATUS: " + dbConnection.getConnection().State);
                        MySqlDataReader rd = query.ExecuteReader();
                        
                        if (rd.HasRows)
                            {
                                rd.Read(); // read first row
                                numberOfRepeats = rd.GetInt32(0);
                                query.Dispose();
                                rd.Dispose();
                                Console.WriteLine(numberOfRepeats);
                                if (numberOfRepeats >= 3)
                                {
                                    Console.WriteLine("HELLO IM HERE!" + appId);
                                    MySqlCommand threeApproved = new MySqlCommand();
                                    threeApproved.Connection = dbConnection.getConnection();
                                    threeApproved.CommandText = "UPDATE applications SET passOn = 'Yes' where application_id = @application_id";
                                   
                                    threeApproved.Parameters.AddWithValue("@application_id", appId);
                                    threeApproved.ExecuteNonQuery();
                                    threeApproved.Dispose();
                                }
                            }
                        dbConnection.CloseConnection();
                        RefreshApplicationList();
                    }
                    catch (Exception)
                    {
                         MessageBox.Show("You cannot approve an application multiple times");
                        
                         dbConnection.CloseConnection();
                         RefreshApplicationList();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("You must select an application");
            };
        }

        private void btnTag_OnClick(object sender, EventArgs e)
        {
            try
            {
                appId = Int32.Parse(applicationList.SelectedItems[0].SubItems[1].Text);
                TagPersonForm tagPersonForm = new TagPersonForm(dbConnection, jobId, appId, memberId, securityStatus);
                tagPersonForm.Show();
                otherWindowOpen = true;

            }
            catch
            {
                MessageBox.Show("Please select an application");
            }
        }

        private void applicationList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (applicationList.SelectedItems.Count > 0)
            {
                btnComment.Enabled = true;
                btnFinalize.Enabled = true;
                btnApprove.Enabled = true;
                btnRemove.Enabled = true;
                btnTag.Enabled = true;
                btnDownload.Enabled = true;
            }
            else
            {
                btnComment.Enabled = false;
                btnFinalize.Enabled = false;
                btnApprove.Enabled = false;
                btnRemove.Enabled = false;
                btnTag.Enabled = false;
                btnDownload.Enabled = false;
            }
        }

        private void btnFinalize_Click(object sender, EventArgs e)
        {
            try {
                appId = Int32.Parse(applicationList.SelectedItems[0].SubItems[1].Text);
                if (dbConnection.OpenConnection())
                {
                    MySqlCommand command = new MySqlCommand();
                    command.Connection = dbConnection.getConnection();
                    command.CommandText = "UPDATE applications SET approved = 'Yes' where application_id = @application_id";
                    command.Parameters.AddWithValue("@application_id", appId);
                    command.ExecuteNonQuery();
                    command.Dispose();

                    command.Connection = dbConnection.getConnection();
                    command.CommandText = "UPDATE dropboxes SET job_status = 'Approved' where job_id = @job_id";
                    command.Parameters.AddWithValue("@job_id", jobId);
                    command.ExecuteNonQuery();
                    command.Dispose();
                    MessageBox.Show("Application Finalized!", "EARS System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("You cannot approve an application multiple times");
            }
            dbConnection.CloseConnection();
            RefreshApplicationList();
        }
    }
}
