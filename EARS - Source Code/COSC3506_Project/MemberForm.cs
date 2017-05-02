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
    public partial class MemberForm : Form
    {
        private DBConnection dbConnection;
        private Boolean otherWindowOpen = false;
        private int securityStatus;
        private int memberId;

        public MemberForm(DBConnection dbConnection, int securityStatus, int memberId)
        {
            this.dbConnection = dbConnection;
            this.securityStatus = securityStatus;
            this.memberId = memberId;
            InitializeComponent();

            switch (securityStatus)
            {
                case 2: // Secretary
                    btnAdd.Visible = true;
                    btnRemove.Visible = true;
                    btnView.Visible = true;
                    break;

                case 3: // Committee
                    btnViewTags.Visible = true;
                    btnView.Visible = true;
                    break;

                case 4: // Chair
                    btnView.Visible = true;
                    break;
            }
        }

        private void MemberForm_Load(object sender, EventArgs e)
        {
            jobList.View = View.Details;
            jobList.GridLines = true;
            jobList.FullRowSelect = true;

            jobList.Columns.Add("Job ID", 150);
            jobList.Columns.Add("Job Title", 150);
            jobList.Columns.Add("Job Status", 150);
        }

        private void jobList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (jobList.SelectedItems.Count > 0)
            {
                btnView.Enabled = true;
            }
            else
            {
                btnView.Enabled = false;
            }
        }

        public void RefreshJobList()
        {
            jobList.Items.Clear();

            if (dbConnection.OpenConnection())
            {
                MySqlCommand command = new MySqlCommand();

                command.Connection = dbConnection.getConnection();
                command.CommandText = "SELECT job_id, job_name, job_status FROM dropboxes";

                using (MySqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ListViewItem item = new ListViewItem();

                        item.Text = dr[0].ToString();
                        item.SubItems.Add(dr[1].ToString());
                        item.SubItems.Add(dr[2].ToString());

                        jobList.Items.Add(item);
                    }
                }

                dbConnection.CloseConnection();
                command.Dispose();

                foreach (ListViewItem li in jobList.Items)
                {
                    if (li.SubItems[2].Text == "Approved")
                    {
                        li.BackColor = Color.LightGreen;
                    }
                }
            }
        }

        private void MemberForm_Activated(object sender, EventArgs e)
        {
            Console.WriteLine("Form active, refreshing user list.");
            RefreshJobList();
        }

        private void MemberForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!otherWindowOpen)
            {
                Application.Exit();
            }
        }

        private void btnAdd_OnClick(object sender, EventArgs e)
        {
            AddJobForm addJobForm = new AddJobForm(dbConnection);
            addJobForm.ShowDialog();
        }

        private void btnRemove_OnClick(object sender, EventArgs e)
        {
            try
            {
                string jobId = jobList.SelectedItems[0].Text;
                string filePath = getFilePath(jobId);
                DeleteFTPDirectory(filePath);

                if (dbConnection.OpenConnection())
                {
                    MySqlCommand command = new MySqlCommand();

                    command.Connection = dbConnection.getConnection();
                    command.CommandText = "DELETE FROM dropboxes WHERE job_id = @job_id";
                    command.Parameters.AddWithValue("@job_id", jobId);

                    command.ExecuteNonQuery();
                    dbConnection.CloseConnection();
                    command.Dispose();
                }

                RefreshJobList();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Didn't select an option...");
            }
        }

        private string getFilePath(string jobId)
        {
            string filePath = "";
            if (dbConnection.OpenConnection())
            {
                MySqlCommand command = new MySqlCommand();

                command.Connection = dbConnection.getConnection();
                command.CommandText = "SELECT job_path FROM dropboxes WHERE job_id=@job_id";

                command.Parameters.AddWithValue("@job_id", jobId);

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

        private List<string> ListDirectories(string filePath)
        {
            WebRequest request = WebRequest.Create(filePath);
            request.Credentials = new NetworkCredential("ftpuser", "pickleparty");

            request.Method = WebRequestMethods.Ftp.ListDirectory;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);

            List<string> result = new List<string>();

            while (!reader.EndOfStream)
            {
                result.Add(reader.ReadLine());
            }

            reader.Close();
            response.Close();

            return result;
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

        private void DeleteFTPDirectory(string filePath)
        {
            WebRequest request = WebRequest.Create(filePath);
            request.Credentials = new NetworkCredential("ftpuser", "pickleparty");

            List<string> filesList = ListDirectories(filePath);

            foreach (string file in filesList)
            {
                DeleteFTPFile("ftp://159.203.38.0/files/" + file);
            }

            request.Method = WebRequestMethods.Ftp.RemoveDirectory;

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

        private void btnView_OnClick(object sender, EventArgs e)
        {
            try
            {
                int jobId = Int32.Parse(jobList.SelectedItems[0].Text);
                ApplicationForm addApplicationForm = new ApplicationForm(dbConnection, jobId, securityStatus, memberId);
                addApplicationForm.Show();
                otherWindowOpen = true;
                this.Close();
            }
            catch(Exception ex)
            { Console.WriteLine("Job not selected..."); }
        }

        private void btnViewTags_onClick(object sender, EventArgs e)
        {
            TaggedInForm form = new TaggedInForm(dbConnection, securityStatus, memberId);
            form.Show();
            otherWindowOpen = true;
            this.Close();
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
