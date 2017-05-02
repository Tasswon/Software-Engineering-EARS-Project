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
    public partial class createCommentsForm : Form
    {
        private DBConnection dbConnection;
        private int appId;
        private int memberId;
        private string filePath;
        private string fileName;

        public createCommentsForm(DBConnection dbConnection, int memberId, int appId)
        {
            this.appId = appId; //somehow get the application id of the application we currently have selected
            this.memberId = memberId; //get member ID of he who is currently logged in somehow
            this.dbConnection = dbConnection;
            InitializeComponent();
        }
        private void submitBtn_Click(object sender, EventArgs e)
        {
            string comment = commentBox.Text;

            if (comment == null)
                throw new Exception();
                
            if (dbConnection.OpenConnection())
            {
                MySqlCommand command = new MySqlCommand();
                command.Connection = dbConnection.getConnection();
                command.CommandText = "INSERT INTO comments (application_id, comment_text, member_id) VALUES (@application_id, @comment_text, @member_id)";
               // command.Parameters.AddWithValue("@comment_id", commentId);
                command.Parameters.AddWithValue("@application_id", appId);
                command.Parameters.AddWithValue("@@comment_text", comment);
                command.Parameters.AddWithValue("@member_id", memberId);
                Console.WriteLine("Congrats you made it this far");
                command.ExecuteNonQuery();
                dbConnection.CloseConnection();
                command.Dispose();
                    
            }
                
            MessageBox.Show("Comment posted!", "EARS System Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
