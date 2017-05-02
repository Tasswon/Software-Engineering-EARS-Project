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
    public partial class SignInForm : Form
    {
        private DBConnection dbConnection;
        private bool signIn;

        public SignInForm(DBConnection dbConnection)
        {
            this.dbConnection = dbConnection;
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            // TODO: Check for saved username.
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            if (txtUsername.TextLength > 0)
            {
                if (txtPassword.TextLength > 0)
                {
                    if (dbConnection.OpenConnection())
                    {
                        int memberId = 0;
                        String username = "";
                        String password = "";
                        int securityStatus = 0;

                        MySqlCommand command = new MySqlCommand();

                        command.Connection = dbConnection.getConnection();
                        command.CommandText = "SELECT member_id, username, password, security_status FROM user_login WHERE username=@username";

                        command.Parameters.AddWithValue("@username", this.txtUsername.Text);

                        using (MySqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                memberId = Int32.Parse(dr[0].ToString());
                                username = dr[1].ToString();
                                password = dr[2].ToString();
                                securityStatus = Int32.Parse(dr[3].ToString());
                            }
                        }

                        dbConnection.CloseConnection();
                        command.Dispose();

                        if (username.Equals(txtUsername.Text) && password.Equals(txtPassword.Text))
                        {
                            switch (securityStatus)
                            {
                                case 1: // System administrator
                                    SystemAdminForm systemAdminForm = new SystemAdminForm(dbConnection);
                                    systemAdminForm.Show();
                                    signIn = true;
                                    this.Close();
                                    break;
                                case 2: // Secretary
                                case 3: // member
                                case 4: // chair
                                    MemberForm memberForm = new MemberForm(dbConnection, securityStatus, memberId);
                                    memberForm.Show();
                                    signIn = true;
                                    this.Close();
                                    break;
                            }
                        }
                        else
                        {
                            MessageBox.Show(this, "Invalid username or password.", "Sign In", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                    MessageBox.Show(this, "Please enter a valid password.", "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show(this, "Please enter a valid username.", "Invalid Username", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void chkRememberUsername_CheckedChanged(object sender, EventArgs e)
        {
            // TODO: Store/remove the username value in configurationn if not already.
        }

        private void SignInForm_Closed(object sender, FormClosedEventArgs e)
        {
            if (!signIn)
                Application.Exit();
        }
    }
}
