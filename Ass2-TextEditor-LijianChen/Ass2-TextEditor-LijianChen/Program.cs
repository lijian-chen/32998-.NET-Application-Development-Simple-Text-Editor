using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ass2_TextEditor_LijianChen
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

            SimpleTextEditorSystem STES = new SimpleTextEditorSystem();

            // Check if login text file is exist
            if (STES.CheckLoginFilePath() == true)
            {
                Application.Run(new LoginForm());
            }
            else
            {
                // Show a message box to report to the user
                DialogResult dialogResult = MessageBox.Show("Login credentials data not found!", "Login Credentials Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (dialogResult == DialogResult.OK)
                {
                    Application.Exit(); // Exit from the application as the login text file is not found
                }
            }
        }
    }
}
