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
    public partial class CommentsForm : Form
    {
        private DBConnection dbConnection;
        private int memberId;
        private int appId;
        private int securityStatus;
        private int jobId;
        private Boolean otherWindowOpen = false;

        public CommentsForm(DBConnection dbConnection, int jobId, int securityStatus, int memberId, int appId)
        {
            this.memberId = memberId;
            this.jobId = jobId;
            this.appId = appId; 
            this.securityStatus = securityStatus;
            this.dbConnection = dbConnection;
            InitializeComponent();
        }

        private void CommentsForm_Load(object sender, EventArgs e)
        {
            commentList.View = View.Details;
            commentList.GridLines = true;
            commentList.FullRowSelect = true;

            commentList.Columns.Add("Application", 150);
            commentList.Columns.Add("Commenter", 150);
            commentList.Columns.Add("Comment Text", 250);
        }

        public void RefreshCommentList()
        {
            commentList.Items.Clear();

           if (dbConnection.OpenConnection())
            {
                MySqlCommand command = new MySqlCommand();
                
                command.Connection = dbConnection.getConnection();
                command.CommandText = "SELECT application_id, member_id, comment_text FROM comments WHERE application_id = @app_id";
                command.Parameters.AddWithValue("@app_id", appId);
                
                using (MySqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ListViewItem item = new ListViewItem();

                        item.Text = dr[0].ToString();
                        item.SubItems.Add(dr[1].ToString());
                        item.SubItems.Add(dr[2].ToString());

                        commentList.Items.Add(item);
                    }
                }

                dbConnection.CloseConnection();
                command.Dispose();
           }
        }

        private void CommentsForm_Activated(object sender, EventArgs e)
        {
            Console.WriteLine("Form active, refreshing user list.");
            RefreshCommentList();
        }

        private void CommentsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (otherWindowOpen == false)
                Application.Exit();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            createCommentsForm createComment = new createCommentsForm(dbConnection, memberId, appId);
            createComment.ShowDialog();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            ApplicationForm form = new ApplicationForm(dbConnection, jobId, securityStatus, memberId);
            form.Show();
            otherWindowOpen = true;
            this.Close();
        }
    }
}

