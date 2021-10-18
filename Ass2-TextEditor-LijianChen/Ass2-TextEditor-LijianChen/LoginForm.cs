using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ass2_TextEditor_LijianChen
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        /*
         * @brief Exit button to terminate the running application (Simple Text Editor) with code 0x0
         * 
         * @return void
         */
        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Exit from the application
        }

        /*
         * @brief Login button to login to the Simple Text Editor System to use the text editor based on user type
         * 
         * @return void
         */
        private void loginButton_Click(object sender, EventArgs e)
        {
            SimpleTextEditorSystem STES = new SimpleTextEditorSystem();
            Users users = new Users();

            // Get the username and password input from the user
            string usernameInput = usernameTextBox.Text;
            string passwordInput = passwordTextBox.Text;
            // Store the user name (username, first name, last name) of the logged-in user
            string loggedInUserName = "";
            // Store the user type of the logged-in user
            string loggedInUserType = "";
            // Dictionary to store the login credentials
            Dictionary<string, string> loginCredentials = new Dictionary<string, string>();
            // List to store user information (Users object)
            List<Users> userInformation = new List<Users>();

            // Load/Update login credentials
            loginCredentials = STES.GetLoginCredentials();

            // Login verification
            if ((loginCredentials.ContainsKey(usernameInput)) && (passwordInput == loginCredentials[usernameInput]))
            {
                // Get the user information based on the username
                userInformation = users.GetUserInformation(usernameInput);

                // Loop over the List<Users> for the matched user's information
                foreach (Users user in userInformation)
                {
                    // Logged-in user name include the username, first name and last name
                    loggedInUserName = user.Username + " | " + user.FirstName + " " + user.LastName;
                    loggedInUserType = user.UserType; // User type of the logged-in user
                }

                TextEditorForm textEditorForm = new TextEditorForm(loggedInUserName, loggedInUserType);
                textEditorForm.Show(); // Show the TextEditorForm
                this.Hide(); // Hide the current form (LoginForm)
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Invalid credentials!", "Login Verification", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);

                if (dialogResult == DialogResult.Retry)
                {
                    // Clear the previous input
                    usernameTextBox.Clear();
                    passwordTextBox.Clear();
                }
                else
                {
                    Application.Exit(); // Exit from the application if the user does not want to retry the login
                }
            }
        }

        /*
         * @brief New User button to do the new user registration
         * 
         * @return void
         */
        private void newUserButton_Click(object sender, EventArgs e)
        {
            NewUserForm newUserForm = new NewUserForm();
            newUserForm.Show(); // Show the NewUserForm
            this.Hide(); // Hide the current form (LoginForm)
        }
    }
}
