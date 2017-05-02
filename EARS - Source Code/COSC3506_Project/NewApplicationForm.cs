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
    public partial class NewApplicationForm : Form
    {
        private DBConnection dbConnection;
        private int jobId;
        private string filePath;
        private string fileName;

        public NewApplicationForm(DBConnection dbConnection, int jobId)
        {
            this.jobId = jobId;
            this.dbConnection = dbConnection;
            InitializeComponent();
        }

        private void btnCancel_OnClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAttach_OnClick(object sender, EventArgs e)
        {
            getFilePath();
        }

        private void getFilePath()
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            filePath = openFileDialog.FileName;
                            fileName = Path.GetFileName(openFileDialog.FileName);
                            fileText.Text = filePath;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void btnCreate_OnClick(object sender, EventArgs e)
        {
            try
            {
                string name = nameBox.Text;
                string phone = phoneBox.Text;
                string email = emailBox.Text;

                if (name == null || phone == null || email == null || filePath == null)
                {
                    throw new Exception();
                }
                string fileFolder = "";
                if (dbConnection.OpenConnection())
                {
                    MySqlCommand command = new MySqlCommand();
                    command.Connection = dbConnection.getConnection();
                    command.CommandText = "SELECT job_path FROM dropboxes WHERE job_id = @job_id";              
                    command.Parameters.AddWithValue("@job_id", jobId);

                    using (MySqlDataReader dr = command.ExecuteReader())
                    {
                        try
                        {
                            while (dr.Read())
                                fileFolder = dr[0].ToString();

                        }
                        catch (Exception ex)
                        { System.Console.WriteLine("Not found..."); }
                    }
                    dbConnection.CloseConnection();
                    command.Dispose();
                }

                string newApplication = fileFolder + "/" + fileName;

                WebRequest request = WebRequest.Create(newApplication);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential("ftpuser", "pickleparty");

                byte[] bytes = System.IO.File.ReadAllBytes(@filePath);
                request.ContentLength = bytes.Length;
                using (Stream request_stream = request.GetRequestStream())
                {
                    request_stream.Write(bytes, 0, bytes.Length);
                    request_stream.Close();
                }

                if (dbConnection.OpenConnection())
                {
                    MySqlCommand command = new MySqlCommand();

                    command.Connection = dbConnection.getConnection();
                    command.CommandText = "INSERT INTO applications (job_id, name, phone, email, app_path, approved) VALUES (@job_id, @name, @phone, @email, @app_path, @approved)";
                    command.Parameters.AddWithValue("@job_id", jobId);
                    Console.WriteLine("JOB ID" + jobId);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@app_path", (fileFolder + "/" + fileName));
                    command.Parameters.AddWithValue("@approved", "No");

                    command.ExecuteNonQuery();
                    dbConnection.CloseConnection();
                    command.Dispose();
                }
                MessageBox.Show("Application successfully created!", "EARS System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception x)
            {
                Console.WriteLine("All fields must be complete...");
                Console.WriteLine(x);
            }
        }
    }
}

