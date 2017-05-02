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

namespace COSC3506_Project
{
    public partial class AddJobForm : Form
    {
        private DBConnection dbConnection;

        public AddJobForm(DBConnection dbConnection)
        {
            this.dbConnection = dbConnection;
            InitializeComponent();
        }

        private void btnCancel_OnClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFinish_OnClick(object sender, EventArgs e)
        {
            try
            {
                string newJob = "ftp://159.203.38.0/files/" + jobText.Text.Replace(" ", "_");
                WebRequest request = WebRequest.Create(newJob);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.Credentials = new NetworkCredential("ftpuser", "pickleparty");
                using (var resp = (FtpWebResponse)request.GetResponse())
                {
                    Console.WriteLine(resp.StatusCode);
                }

                if (dbConnection.OpenConnection())
                {
                    MySqlCommand command = new MySqlCommand();

                    command.Connection = dbConnection.getConnection();
                    command.CommandText = "INSERT INTO dropboxes (job_name, job_path, job_status) VALUES (@job_name, @job_path, @job_status)";
                    command.Parameters.AddWithValue("@job_name", jobText.Text.Replace(" ", "_"));
                    command.Parameters.AddWithValue("@job_path", newJob.Replace(" ", "_"));
                    command.Parameters.AddWithValue("@job_status", "Not_Approved");

                    command.ExecuteNonQuery();
                    dbConnection.CloseConnection();
                    command.Dispose();
                }

                this.Close();
            }
            catch (Exception ex) { Console.WriteLine("Text not entered..."); }
        }
    }
}
