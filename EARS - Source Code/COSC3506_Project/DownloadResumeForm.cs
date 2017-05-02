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
    public partial class DownloadResumeForm : Form
    {
        private DBConnection dbConnection;
        private string appId;
        private string downloadFile;
        private string downloadLocation;
        private string fileName;

        public DownloadResumeForm(DBConnection dbConnection, string appId)
        {
            this.appId = appId;
            this.dbConnection = dbConnection;
            InitializeComponent();
        }

        private void btnCancel_OnClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            string filePath = "";
            using (var selectDirectory = new FolderBrowserDialog())
            {
                DialogResult result = selectDirectory.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(selectDirectory.SelectedPath))
                {
                    filePath = selectDirectory.SelectedPath;
                }
            }
            fileText.Text = filePath;
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                downloadFile = getDownloadFile();
                downloadLocation = @fileText.Text;

                for (int i = downloadFile.Length - 1; i > 0; i--)
                {
                    if (downloadFile[i - 1] == '/')
                    {
                        fileName = downloadFile.Substring(i);
                        break;
                    }
                }

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(downloadFile);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential("ftpuser", "pickleparty");
                request.UseBinary = true;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();

                FileStream ws = new FileStream(downloadLocation + @"\" + fileName, FileMode.Create);

                using (ws)
                {
                    byte[] buffer = new byte[2048];
                    int bytesRead = responseStream.Read(buffer, 0, buffer.Length);

                    while (bytesRead > 0)
                    {
                        ws.Write(buffer, 0, bytesRead);
                        bytesRead = responseStream.Read(buffer, 0, buffer.Length);                     
                    }
                }
                response.Close();
                MessageBox.Show("File Download Complete", "EARS System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("File download has run into an error! If this persists contact system administrator.", 
                    "EARS System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private string getDownloadFile()
        {
            string downloadFile = "";
            if (dbConnection.OpenConnection())
            {
                MySqlCommand command = new MySqlCommand();

                command.Connection = dbConnection.getConnection();
                command.CommandText = "SELECT app_path FROM applications WHERE application_id = @application_id";
                command.Parameters.AddWithValue("@application_id", appId);

                using (MySqlDataReader dr = command.ExecuteReader())
                {
                    try
                    {
                        while (dr.Read())
                            downloadFile = dr[0].ToString();
                    }
                    catch (Exception ex)
                    { System.Console.WriteLine("Not found..."); }
                }
                dbConnection.CloseConnection();
                command.Dispose();
            }
            return downloadFile;
        }
    }
}
