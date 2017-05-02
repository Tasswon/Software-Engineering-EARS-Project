using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COSC3506_Project
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // TODO: Create a new Connection and test before showing mainForm.
            DBConnection dbConnection = new DBConnection("159.203.38.0", "ears_db", "root", "12345");
            if (dbConnection.OpenConnection())
            {
                dbConnection.CloseConnection();
                SignInForm signInForm = new SignInForm(dbConnection);
                signInForm.Show();
                Application.Run();
            }
            else
            {
                dbConnection.CloseConnection();
                MessageBox.Show(null, "Error connecting to the database. Please try again.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
